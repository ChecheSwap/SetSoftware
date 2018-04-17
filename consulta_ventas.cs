using MundoMusical.XBASE;
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
using static MundoMusical.DB.dbop;
using MundoMusical.VENTA;
using MundoMusical.XML_SCHEMA_WRITER;
using MundoMusical.CONSULTAS.VENTAS.VENTAS_GENERAL;
using MundoMusical.TICKET.CRYSTAL_RPT_TICKET;

namespace MundoMusical.CONSULTAS.VENTAS
{
    public partial class consulta_ventas : baseConsultas
    {

        private dbop db;
        public cliente_idnombre [] arrclientes;
        private string[] arrnombreclientes;
        private dgvtoxml_dataset xmlwriter;
        private venta ventatmp;


        private printTicket ticketBase;
        private frmrptticket ticketViewer;

        private int idventa;
        public consulta_ventas(consultas_central central)
        {
            this.InitializeComponent();
            this.configs();
            this.FormClosed += (sender, args) => { central.Close(); };
        }
        public consulta_ventas()
        {
            InitializeComponent();
            this.configs();
        }
        private void loadall() //CARGA TODAS LAS VENTAS EN GRID
        {
            this.db.getventasdgvconsultas(ref this.dgv);
        }    

        public void getclientes()
        {            

            this.arrclientes = this.db.getallclientesnameidasarray(); //OBTENGO TODOS LOS CLIENTES {ID,NOMBRE COMPLETO}

            this.loadall();

            if (this.arrclientes != null) //SI HAY CLIENTES PROCEDO A LLENAR COMBO BOX ID CLIENTE
            {
                this.arrnombreclientes = new string[this.arrclientes.Length]; //LLENO ARREGLO CON LOS NOMBRES DE LOS CLIENTES

                for (int x = 0; x < this.arrclientes.Length; ++x)
                {
                    this.arrnombreclientes[x] = this.arrclientes[x].nombre;
                }

                this.cbidcliente.DataSource = this.arrnombreclientes;
                this.cbidcliente.SelectedIndex = -1;
            }
        }
        private void configs() //INICIALIZA TODOS LOS DATOS DE LA FORMA
        {
            this.db = new dbop();

            this.dgv.DataSourceChanged += (sender, args) => {

                this.txtnumero.Text = this.dgv.RowCount.ToString() + " Ventas";

                double sum = 0;
                string tmp;

                try
                {
                    foreach (DataGridViewRow r in this.dgv.Rows)
                    {
                        tmp = r.Cells[4].Value.ToString();

                        sum += double.Parse(tmp.Replace("$", "").Trim());
                    }
                }
                catch (Exception ex)
                {
                    genericDefinitions.error(ex.ToString());
                }

                this.txtmonto.Text = "$" + sum.ToString("F2");
            };

            this.getclientes();                                   

            behaviorDefinitions.txtOnlyNumbers(ref this.txtnumeroventa);
            behaviorDefinitions.txtOnlyNumbers(ref this.txtidventa2);

            foreach (String modo in statusVenta.getallstatus())
            {
                this.cbstatus.Items.Add(modo);
            }

            this.txtnumeroventa.KeyUp += (sender, args) => { this.db.getventasdgvconsultas(ref this.dgv, "idventa", this.txtnumeroventa.Text.Trim()); };
            this.dtpfecha.ValueChanged += (sender, args) => { this.db.getventasdgvconsultas(ref this.dgv, "fecha", this.dtpfecha.Text.Trim()); };
            this.dtpfecha.TextChanged += (sender, args) => { this.db.getventasdgvconsultas(ref this.dgv, "fecha", this.dtpfecha.Text.Trim()); };
            this.cbstatus.TextChanged += (sender, args) => { this.db.getventasdgvconsultas(ref this.dgv, "status", this.cbstatus.Text.Trim()); };

            this.cbidcliente.TextChanged += (sender, args) => {
                if (this.cbidcliente.Text != string.Empty)
                {
                    this.db.getventasdgvconsultas(ref this.dgv, "idcliente", this.arrclientes[this.cbidcliente.SelectedIndex].id.ToString());
                }
            };

            this.txtnumeroventa.GotFocus += (sender, args) => {
                this.clearvals();
            };
            this.dtpfecha.GotFocus += (sender, args) => {
                this.clearvals();
            };
            this.cbstatus.GotFocus += (sender, args) => {
                this.clearvals();
            };
            this.cbidcliente.GotFocus += (sender, args) => {
                this.clearvals();
            };

            this.SizeChanged += (sender, args) => {
                this.centrarelementos();
            };

            this.tabconsultas.SelectedIndexChanged += (sender, args) => { this.centercruds(); this.centrarelementos(); };

            this.txtidventa2.KeyUp += (sender, args) => {

                if(this.txtidventa2.Text.Trim() == string.Empty)
                {
                    this.clearValsVenta();
                }
                    
                if(args.KeyCode == Keys.Enter && this.txtidventa2.Text.Trim() != string.Empty)
                {
                    this.maskidventa();
                }
            };

            this.txtnumticket.KeyUp += (sender, args) =>
            {
                if (this.txtnumticket.Text.Trim() == string.Empty)
                {
                    this.clearValsVenta();
                }
                else if (args.KeyCode == Keys.Enter)
                {
                    this.maskidticket();
                }

            };

            this.txtnumticket.GotFocus += (sender, args) =>
            {
                this.cleartxts();
            };

            this.txtidventa2.GotFocus += (sender, args) =>
            {
                this.cleartxts();
            };

            this.dgvdesglose.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void cleartxts()
        {
            this.txtnumticket.Text = string.Empty;
            this.txtidventa2.Text = string.Empty;
            this.clearValsVenta();
        }

        private void maskidventa()
        {
            if (this.txtidventa2.Text.Trim() != "")
            {
                this.makedesglose(int.Parse(this.txtidventa2.Text.Trim()), "idventa");
            }
            else
            {
                genericDefinitions.dangerInfo("Ingrese Id Venta.");
                this.txtidventa2.Focus();
            }
        }

        private void maskidticket()
        {
            if (this.txtnumticket.Text.Trim() != "")
            {
                this.makedesglose(int.Parse(this.txtnumticket.Text.Trim()), "ticket");
            }
            else
            {
                genericDefinitions.dangerInfo("Ingrese Numero de Ticket.");
                this.txtnumticket.Focus();
            }
        }

        private void clearValsVenta()
        {
            this.dgvdesglose.DataSource = null;
            this.txtnventa.Text = string.Empty;
            this.txtsubtotal.Text = string.Empty;
            this.txtiva.Text = string.Empty;
            this.txttotal.Text = string.Empty;
            this.txtdescextra.Text = string.Empty;
        }

        private void centrarelementos()
        {
            this.gboxconsulta2.Location = new Point(this.tabconsultas.Width / 2 - this.gboxconsulta2.Width / 2, this.gboxconsulta2.Location.Y);
            this.pbox2.Location = new Point(this.tabconsultas.Width / 2 - this.pbox2.Width / 2, this.pbox2.Location.Y);
            this.gboxdatosventa.Location = new Point(this.tabdesglose.Width / 2 - this.gboxdatosventa.Width / 2, this.gboxdatosventa.Location.Y);
            this.btnprintDesglose.Location = new Point(this.gboxconsulta2.Location.X + this.gboxconsulta2.Width - this.btnprintDesglose.Width , this.btnprintDesglose.Location.Y);
            this.btnprevia.Location = new Point(this.gboxconsulta2.Location.X + this.gboxconsulta2.Width - this.btnprintDesglose.Width - this.btnprevia.Width, this.btnprevia.Location.Y);

            this.txtmonto.Location = new Point(this.gb1.Location.X - this.txtmonto.Width - 5, this.txtmonto.Location.Y);
            this.lbluno.Location = new Point(this.txtmonto.Location.X , this.lbluno.Location.Y);

            this.txtnumero.Location = new Point(this.gb1.Location.X + this.gb1.Width + 5, this.txtnumero.Location.Y);
            this.lbl2.Location = new Point(this.txtnumero.Location.X, this.lbl2.Location.Y);
        }
        private void makedesglose(int id, string tipo)
        {          
            
            if(tipo == "ticket")
            {
                this.idventa = this.db.getIdventa(id);               
            }
            else
            {
                this.idventa = id;
            }
            
            if (this.db.existventa(idventa))
            {
                this.ventatmp = this.db.getventa(idventa);

                this.dgvdesglose.DataSource = this.db.getdetallesticket(idventa);

                this.txtnventa.Text = idventa.ToString();
                this.txtsubtotal.Text = "$" + this.ventatmp.subtotal.ToString();
                this.txtiva.Text = "$" + this.ventatmp.iva.ToString();
                this.txttotal.Text = "$" + this.ventatmp.total.ToString();
                this.txtdescextra.Text = "$" + this.ventatmp.descuentoextra.ToString();

                this.dgvdesglose.Columns[0].HeaderText = "Cantidad";
                this.dgvdesglose.Columns[1].HeaderText = "Descripción";
                this.dgvdesglose.Columns[2].HeaderText = "Precio";
                this.dgvdesglose.Columns[3].HeaderText = "Importe";
            }
            else
            {
                genericDefinitions.dangerInfo("Esta venta no existe!");

                if (tipo == "idventa")
                {                    
                    this.txtidventa2.Focus();
                }
                else
                {                    
                    this.txtnumticket.Focus();
                }
            }            
        }
       
        private void clearvals()
        {
            this.txtnumeroventa.Text = string.Empty;
            this.cbstatus.SelectedIndex = -1;
            this.cbidcliente.SelectedIndex = -1;
            this.dtpfecha.Value = DateTime.Parse(genericDefinitions.getDate());

            this.loadall();
        }
        private void consulta_ventas_Load(object sender, EventArgs e)
        {
            if (!DesignMode) { 
                this.panel2.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right);
                this.panellinea.Anchor = (AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right);
            }               
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.cargabuscarcliente();
        }

        private void cargabuscarcliente()
        {
            this.getclientes();
            using (seleccionarCliente sc = new seleccionarCliente(this))
            {
                sc.ShowDialog();
            }
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            this.imprime();
        }

        private void imprime()
        {
            this.xmlwriter = new dgvtoxml_dataset(this.dgv, this.Name + "CONSULTAVENTAGENERAL");
            this.xmlwriter.create();
            frmrptventa_general vista = new frmrptventa_general(this.xmlwriter.getdatatable(), this.txtmonto.Text.Trim());           
            vista.ShowDialog();            
        }

        private void imprimeTicket(string caso)
        {
            if (this.txtnventa.Text.Trim() != string.Empty)
            {
                switch (caso)
                {
                    case ("vistaprevia"):
                        {
                            this.ticketBase = new printTicket(this.db.getventa(int.Parse(this.txtnventa.Text.Trim())), this.db.getTienda());
                            this.ticketBase.setparams();

                            this.ticketViewer = new frmrptticket(this.ticketBase.getreporte());
                            this.ticketViewer.ShowDialog();
                            break;
                        }
                    case ("imprime"):
                        {
                            this.ticketBase = new printTicket(this.db.getventa(int.Parse(this.txtnventa.Text.Trim())), this.db.getTienda());
                            this.ticketBase.setparams();

                            this.ticketBase.print();
                            break;
                        }
                }

            }
            else
            {
                genericDefinitions.dangerInfo("Ingrese Numero de Venta!");
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.maskidticket();
        }

        private void btnprintDesglose_Click(object sender, EventArgs e)
        {
            this.imprimeTicket("imprime");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.imprimeTicket("vistaprevia");
        }

        private void txtidventa2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.maskidventa();
        }
    }
}
