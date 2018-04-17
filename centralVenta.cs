using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MundoMusical.CUSTOM_CONTROLS;
using MundoMusical.DB;
using System.Globalization;
using MundoMusical.XBASE;
using MundoMusical.TICKET;
using MundoMusical.IMAGE_OPERATIONS;
using MundoMusical.TICKET.CRYSTAL_RPT_TICKET;
using MundoMusical.PRODUCT;
using MundoMusical.CONFIGURACION_DE_IMPRESION;
using MundoMusical.EFFECTS;

namespace MundoMusical.VENTA
{
    public partial class centralVenta : BaseForm
    {
        public struct vals
        {
            public int totalarticulos { get; set; }
            public double subtotal{ get; set; }
            public double iva{ get; set; }
            public double descuentoextra { get; set;}
            public double total { get; set; }
            
            public vals(bool x):this()
            {
                this.freevals();
            }

            public void freevals()
            {
                this.totalarticulos = 0;
                this.subtotal = 0;
                this.iva = 0;
                this.descuentoextra = 0;
                this.total = 0;               
            }
        }

        public venta ventatmp;
        public int modopago; //ATRIBUTO A ESPERAR DESPUES DE PAGO
        public seleccionarCliente seeclient;

        private DataTable tabla;
        private dbop db;        
                
        private List<int> descuentos;//DESCUENTOS PERMITIDOS
        private string[] tmp; //ARREGLO TEMPORAL PARA AGREGAR DETALLE AL DGV
        private Double dtotal;        
        public vals atributos;//VARIABLE QUE CONTIENE LOS DATOS CALCULADOS DE COMPRA
        
        private Point firstlocation;
        private Size firstsize;
        private bool normalstate = true;
        private Central2 central;
        public ventaCobro frmcobrar;

        private usuario usuarioactual;
        private cliente clienteactual;

        private bool flag;

        private List<producto> inventario;

        private printTicket ticket;
        private cancelarVenta cancelventa;

        private seeproductsinternal buscaProducto;
        
        public centralVenta()
        {
            this.initialconfigs();            
        }
        public centralVenta(Central2 central,ref usuario usuarioactual) 
        {
            
            this.central = central;
            this.usuarioactual = usuarioactual;

            this.initialconfigs();
        }

        private void initialconfigs()
        {
            InitializeComponent();
                        
            this.DoubleBuffered = true;
            this.Text = "Venta de Mostrador";
            this.gb1.BorderColor = Color.DarkGray;
            this.gb2.BorderColor = Color.DarkGray;
            this.gb3.BorderColor = Color.DarkGray;
            this.gb4.BorderColor = Color.DarkGray;
            this.gb5.BorderColor = Color.DarkGray;
            this.gb6.BorderColor = Color.DarkGray;
            this.gb7.BorderColor = Color.DarkGray;
            this.txtcliente.ReadOnly = true;
            this.txtid.ReadOnly = true;
            this.txtarticulos.ReadOnly = true;
            this.txtsubtotal.ReadOnly = true;
            this.txtiva.ReadOnly = true;
            this.db = new dbop();
            behaviorDefinitions.txtPrice(this.txtdescuentoextra);
            behaviorDefinitions.txtPrice(this.txtidproducto);
            this.descuentos = new List<int>() { 0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50 };
            this.cbdescuento.DataSource = this.descuentos;
            this.tabla = new DataTable();
            this.tabla.Columns.Add("Codigo Producto", typeof(string));
            this.tabla.Columns.Add("Descripción", typeof(string));
            this.tabla.Columns.Add("Cantidad", typeof(string));
            this.tabla.Columns.Add("Precio", typeof(string));
            this.tabla.Columns.Add("Descuento", typeof(string));
            this.tabla.Columns.Add("Importe", typeof(string));
            this.dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv.DataSource = this.tabla;
            this.cbcantidad.Minimum = 0;
            this.tmp = new string[6];
            this.atributos = new vals(true);
            this.txttotal.ReadOnly = true;
            this.MaximizeBox = false;
            this.checkboxCP.Checked = true;
           
            this.ventatmp = new venta();
            
            this.txtvendedor.Text = (this.usuarioactual.nombre).ToUpper(); //CARGA DATOS DEL USUARIO DEL SISTEMA ACTUAL

            this.inventario = this.db.getinventarioproductos(); //LLENA INVENTARIO TEMPORAL

            this.FormClosed += (s, g) =>
            {
                this.central.xventa = null;
            };

            this.txtidproducto.KeyUp += (sender, args) =>
            {
                if (args.KeyCode == Keys.Enter && this.txtidproducto.Text.Trim() != "")
                {
                    if (this.rtbCodigos.Checked)
                    {
                        this.addcodeDirect();
                    }
                    else
                    {
                        this.codetxtproducto();
                    }
                }

            };

            this.cbdescuento.KeyUp += (sender, args) =>
            {
                if (args.KeyCode == Keys.Enter)
                {
                    this.additem(this.tmp);
                }
            };

            this.cbcantidad.KeyUp += (sender, args) =>
            {
                if (args.KeyCode == Keys.Enter)
                {
                    this.cbdescuento.Focus();
                }
            };

            this.cbcantidad.ValueChanged += (sender, args) =>
            {
                if (this.cbcantidad.Value > this.cbcantidad.Maximum)
                {
                    this.cbcantidad.Value = this.cbcantidad.Maximum;
                }
            };

            this.dgv.KeyUp += (sender, args) => {

                if (args.KeyCode == Keys.Delete)
                {
                    this.inventario.Find(x => x.idproducto.ToString() == this.dgv.Rows[dgv.CurrentRow.Index].Cells[0].Value.ToString()).stock += int.Parse(this.dgv.Rows[dgv.CurrentRow.Index].Cells[2].Value.ToString());
                    this.tabla.Rows.RemoveAt(this.dgv.CurrentRow.Index);
                    this.updatevalues();
                }
                this.txtidproducto.Focus();
            };

            this.txtdescuentoextra.KeyUp += (sender, args) =>
            {
                if (this.txtdescuentoextra.Text.Trim() != "" && this.txtsubtotal.Text != "0" && args.KeyCode == Keys.Enter)
                {
                    if (Double.Parse(this.txtdescuentoextra.Text, CultureInfo.InvariantCulture) >= Double.Parse(this.txtsubtotal.Text, CultureInfo.InvariantCulture))
                    {
                        genericDefinitions.error("Descuento extra invalido", "Aviso");
                        this.txtdescuentoextra.Focus();
                    }
                    else
                    {
                        this.updatevalues();
                    }
                }

            };
            this.txtdescuentoextra.KeyPress += (x, y) =>
            {
                if (this.txtsubtotal.Text == "0")
                    y.Handled = true;
            };
            this.Resize += (sender, args) =>
            {
                this.pb.Location = new Point(this.Width / 2 - this.pb.Width / 2, this.pb.Location.Y);
            };

            this.btnaccept.GotFocus += (x, y) =>
            {
                this.txtidproducto.Focus();
            };

            this.txtid.TextChanged += (sender, args) =>
            {
                this.clienteactual = this.db.getclientAsobject(int.Parse(this.txtid.Text.Trim()));
            };

            this.blapse.GotFocus += (sender, args) =>
            {
                this.txtidproducto.Focus();
            };

            this.btnsc.GotFocus += (sender, args) =>
            {
                this.txtidproducto.Focus();
            };
            this.stopBounds();
            this.setimage();

            this.LostFocus += (a, b) => { this.Update(); };
            this.GotFocus += (a, b) => { this.Update(); };

            this.txtidproducto.KeyUp += (sender, args) => { if (args.KeyCode == Keys.F12) this.cobrar(); };

            this.btnBuscaproducto.GotFocus += (a, b) => { this.txtidproducto.Focus(); };
        }


        public void updateForm()
        {
            if (genericDefinitions.yesno("Se perderan los datos de venta actual, ¿Desea continuar?", "Actualizar Forma")) 
            {
                this.Close();
                this.central.openxventa();
            }
            else
            {
                this.txtidproducto.Focus();
            }
        }

        public void setimage() //CARGAR LOGO DE TIENDA DE BASE DE DATOS
        {
            if (this.db.getTienda().logotienda != null)
            {
                this.picbox.BackgroundImage = imageOperations.getImageFromBytes(this.db.getTienda().logotienda);
            }
        }
        public void initializeAll()
        {
            this.txtdescuentoextra.Text = "0";
            this.tabla.Clear();
            this.updatevalues();            
            this.loadclientegenerico();
            this.clearTriger();
            this.ActiveControl = this.txtidproducto;
        }

        private void onclickResize()
        {
            if (normalstate)
            {
                normalstate = false;
                this.blapse.Image = global::MundoMusical.Properties.Resources.minimize;
                this.toolTip1.SetToolTip(this.blapse, "Minimizar");

                this.Height = (this.central.Height - this.central.menuStrip1.Height - this.central.panel1.Height) - 20;
                this.Width = this.central.Width - 40;

                this.Location = new Point((this.central.Width / 2) - this.Width / 2, this.central.Location.Y);
            }
            else
            {
                normalstate = true;
                this.blapse.Image = global::MundoMusical.Properties.Resources.maximize;
                this.toolTip1.SetToolTip(this.blapse, "Maximizar");
                this.Size = this.firstsize;
                this.Location = this.firstlocation;
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            this.addcodeDirect();
        }

        private void addcodeDirect()
        {
            if (this.codetxtproducto())
            {
                this.additem(this.tmp);
            }
        }

        private void opcionesAvanzadasToolStripMenuItem_Click(object sender, EventArgs e)
        { }

        private void blapse_Click(object sender, EventArgs e)
        {
            this.onclickResize();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.cobrar();

        }

        private void cobrar()
        {
            if (this.atributos.totalarticulos == 0)
            {
                genericDefinitions.dangerInfo("No existen articulos en Venta Actual", "Atencion");
                this.txtidproducto.Focus();
            }
            else
            {
                this.fillventatmp();
                this.frmcobrar = new ventaCobro(this);
                this.frmcobrar.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.seeclient = new seleccionarCliente(this);
            this.seeclient.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.initializeAll();
        }

        private void checkboxCP_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkboxCP.Checked == true)
            {
                this.loadclientegenerico();
                this.txtidproducto.Focus();
            }

        }
        private void centralVenta_Load(object sender, EventArgs e)
        {
            this.firstsize = this.Size;
            this.firstlocation = this.Location;

            if(this.db.getTienda() != null)
            {
                this.txtnombretienda.Text = this.db.getTienda().nombre;
            }

            if (!this.loadclientegenerico())//CARGA CLIENTE PUBLICO   
            {
                genericDefinitions.error("No se ha podido cargar cliente generico, contacte al desarrollador del sistema.","->loadclientegenerico()");
                this.Shown += (a, b) =>
                {                    
                    this.Close();
                };
            }
                     
            this.pb.Location = new Point(this.Width / 2 - this.pb.Width / 2, this.pb.Location.Y);
            this.ActiveControl = this.txtidproducto;
        }

        //INSTRUCCIONES DE MANEJO DE DATOS DE VENTA

        private bool fillventatmp()//LLENO DATOS DE VENTA TEMPORAL CON VALORES PROPIOS
        {
            this.ventatmp.idventa = this.db.getnumerodeventaactual();
            this.ventatmp.idusuario = this.usuarioactual.id_usuario;
            this.ventatmp.idmodopago = this.modopago;
            this.ventatmp.idcliente = this.clienteactual.id_cliente;
            try
            {
                this.ventatmp.fecha = DateTime.Parse(genericDefinitions.getDate());
                this.ventatmp.hora = TimeSpan.Parse(genericDefinitions.getTimeExact());
            }
            catch (Exception ex)
            {
                genericDefinitions.error(ex.ToString());
                return false;
            }
            this.ventatmp.articulos = this.atributos.totalarticulos;
            this.ventatmp.subtotal = Math.Round(this.atributos.subtotal, 2);            

            if (this.cbiva.Checked)
            {
                this.ventatmp.iva = Math.Round(this.atributos.iva, 2);
                this.ventatmp.total = this.atributos.total;
            }
            else
            {
                this.ventatmp.iva = 0;
                this.ventatmp.total = Math.Round(this.atributos.total, 2) - this.atributos.iva;
            }
            
            this.ventatmp.status = statusVenta.ACTIVA;
            this.ventatmp.descuentoextra = this.atributos.descuentoextra;
            this.ventatmp.idtienda = this.db.getIdTienda();

            return true;
        }
        private bool InsertVenta() //PROCESAR LA VENTA Y DETALLES DE LA MISMA
        {
            if (this.fillventatmp())
            {

                this.flag = true;

                if (this.db.insertarVenta(this.ventatmp))
                {
                    if (!this.db.insertarDetalles(this.dgv, this.ventatmp.idventa))
                    {
                        if (this.db.getventa(this.ventatmp.idventa).status == statusVenta.ACTIVA)
                        {
                            this.db.cancelarventa(this.ventatmp.idventa);
                            this.db.restablecerdetalles(this.ventatmp.idventa);
                        }
                        this.flag = false;
                    }
                    else
                    {
                        this.db.addticket(this.ventatmp.idventa);
                    }
                }
                else
                {
                    this.flag = false;
                }
            }
            else
            {
                genericDefinitions.error("Error al generar Venta Temporal");
                flag = false;
            }
            return this.flag;
        }

        public async Task<bool> VentaFinal() //FUNCION INICIO PARA CREAR VENTA
        {
            bool res = false;
            Task<bool> tarea = new Task<bool>(this.InsertVenta);
            tarea.Start();
            res = await tarea;

            if (res)
            {
                genericDefinitions.ok("Se ha procesado correctamente la venta!", "Venta Realizada");

                if (this.frmcobrar.cbticket.Checked)
                {                                       
                        this.ticket = new printTicket(this.db.getventa(this.ventatmp.idventa), this.db.getTienda());
                        this.ticket.setparams();
                        this.ticket.print();                  
                }
                this.inventario = this.db.getinventarioproductos();
            }
            else
            {
                genericDefinitions.error("No se ha procesado correctamente la venta!", "Error de procesamiento.");
            }
            return res;
        }
        
        private void updatevalues()//CALCULO VALORES EN VARIABLE ATRIBUTOS Y LOS MUESTRO EN PANTALLA
        {
            this.atributos.freevals();
            this.atributos.descuentoextra = Double.Parse(this.txtdescuentoextra.Text.Trim(), CultureInfo.InvariantCulture);
            for (int x = 0; x < this.tabla.Rows.Count; ++x)
            {
                this.atributos.totalarticulos += Int32.Parse(this.dgv.Rows[x].Cells[2].Value.ToString());
                this.atributos.subtotal += Double.Parse(this.dgv.Rows[x].Cells[5].Value.ToString(), CultureInfo.InvariantCulture);
            }
            this.atributos.iva = (this.atributos.subtotal / 100) * 16;
            this.atributos.total = (this.atributos.subtotal + this.atributos.iva)- this.atributos.descuentoextra;

            this.atributos.iva = Math.Round(this.atributos.iva, 2);
            this.atributos.total = Math.Round(this.atributos.total, 2);

            this.txtarticulos.Text = this.atributos.totalarticulos.ToString();
            this.txtsubtotal.Text = this.atributos.subtotal.ToString("F2").Replace(",", ".");
            this.txtiva.Text = this.atributos.iva.ToString("F2").Replace(",", ".");
            this.txttotal.Text = this.atributos.total.ToString("F2").Replace(",", ".");
        }

        public bool loadclientegenerico()//CARGO CLIENTE GENERICO Y INCORPORA NUMERO DE VENTA ACTUAL
        {
            this.flag = false;

            this.clienteactual = this.db.getclienteGenerico();

            if (this.clienteactual != null)
            {
                this.txtcliente.Text = this.clienteactual.nombre;
                this.txtid.Text = this.clienteactual.id_cliente.ToString();
                this.checkboxCP.Checked = true;
                this.txtnumerodeventa.Text = this.db.getnumerodeventaactual().ToString();
                this.flag = true;
            }
            return this.flag;            
        }

        private void additem(string[] point) //COMPLEMENTO DETALLE EN VARIABLE POINT Y SE AGREGA A LA DGV
        {
            //tmp -> POINT

            //POINT[0] = IDPRODUCTO==CODIGO PRODUCTO
            //POINT[1] = DESCRIPCION == NOMBRE
            //POINT[2] = CANTIDAD
            //POINT[3] = PRECIO
            //POINT[4] = DESCUENTO
            //POINT[5] = IMPORTE

            try
            {

                point[1] = this.inventario.Find(x => x.idproducto.ToString() == point[0]).nombre;
                point[2] = this.cbcantidad.Value.ToString();
                point[3] = this.inventario.Find(x => x.idproducto.ToString() == point[0]).precio.ToString();
                point[4] = this.descuentos[this.cbdescuento.SelectedIndex].ToString();

                //(CANTIDAD * PRECIO)-DESCUENTO
                this.dtotal = double.Parse(point[2], CultureInfo.InvariantCulture) * double.Parse(point[3], CultureInfo.InvariantCulture);
                this.dtotal = dtotal - (dtotal / 100) * double.Parse(point[4], CultureInfo.InvariantCulture);


                point[5] = dtotal.ToString().Replace(",", ".");

                foreach (string g in point)
                {
                    if (g == null)
                    {
                        genericDefinitions.error("Datos de detalle invalidos", "Error");
                        return;
                    }
                }
                this.tabla.Rows.Add(point);
                this.inventario.Find(x => x.idproducto.ToString() == point[0]).stock -= int.Parse(this.cbcantidad.Value.ToString());
                this.clearTriger();
                this.updatevalues();
            }
            catch(Exception ex)
            {
                genericDefinitions.error(ex.ToString());
            }   
        }
        private bool codetxtproducto() //VERIFICA LA INTEGRIDAD DE LOS DATOS DE ITEM ANTES DE AGREGARLOS AL DGV (DESPUES DE ENTER EN TXTIDPRODUCTO Y CLICK EN BTN AGREGAR PRODUCTO)
        {
            if(this.txtidproducto.Text.Trim()=="")
            {
                genericDefinitions.dangerInfo("Ingrese ID producto!", "Aviso");
                this.txtidproducto.Focus();
                return false;
            }
            if (!this.db.existproduct(int.Parse(this.txtidproducto.Text)))
            {
                genericDefinitions.dangerInfo("Clave de Producto Invalida", "Error");
                this.clearTriger();
                return false;
            }

           /* if (this.isOntable(this.txtidproducto.Text.Trim()))
            {
                genericDefinitions.dangerInfo("Este producto ya se ha seleccionado", "Información");
                this.clearTriger();
                return false;
            }
            */

               //TMP ARREGLO AUXILIAR DE DETALLE PARA INGRESAR A DGV (AGREGO ID PRODUCTO A ARREGLO [0])
               this.tmp[0] = this.txtidproducto.Text.Trim();                
               this.cbcantidad.Maximum = (this.inventario.Find(x=>x.idproducto.ToString()==this.tmp[0])).stock; //OBTIENE CANTIDAD MAXIMA EN EXISTENCIA INVENTARIO TEMPORAL Y LLENA CBCANTIDAD
               this.cbcantidad.Focus();
            

               if(this.cbcantidad.Maximum == 0)
               {
                   genericDefinitions.error("No existe existencia de este producto en la base de datos Temporal", "Error");
                    this.txtidproducto.Text = null;
                    this.txtidproducto.Focus();              
                   return false;
               }
            this.cbcantidad.Value = 1; //PONE POR DEFAULT LA CANTIDAD DE 1 (LOGICAMENTE AL HACER LA LECTURA DEL CODIGO DE BARRAS SOLO SE TOMA 1)
            return true;                           
       }

       private void clearTriger() //LIMPIO VARIABLE DE DETALLE TMP, TXTIDPRODUCTO, CBCANTIDAD, CDDESCUENTO Y PONGO EL FOCO EN TXTIDPRODUCTO
       {
           for(int x=0; x<this.tmp.Length; ++x)
           {
               this.tmp[x] = null;
           }
           this.txtidproducto.Text = null;
           this.cbcantidad.Value = 0;
           this.cbdescuento.SelectedIndex = 0;
           this.txtidproducto.Focus();
       }
       
        private bool isOntable(string id) //VERIFICO SI UN PRODUCTO SE ENCUENTRA EN LA DGV
        {            
            for(int x=0; x<this.dgv.RowCount; ++x)
            {
                if(this.dgv.Rows[x].Cells[0].Value.Equals(id))
                    return true;
                
            }
            return false;
        }

        private void cancelarVentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.cancelventa = new cancelarVenta();
            this.cancelventa.ShowDialog();
            this.txtidproducto.Focus();
        }

        private void actualizarInventarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.updateForm();
        }

        private void txtidproducto_TextChanged(object sender, EventArgs e)
        {}

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.buscaProducto = new seeproductsinternal(this);
            this.buscaProducto.ShowDialog();
        }
    }
}
