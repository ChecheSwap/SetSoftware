using MundoMusical;
using MundoMusical.CUSTOM_CONTROLS;
using MundoMusical.DB;
using MundoMusical.IMAGE_OPERATIONS;
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

namespace MundoMusical.TIENDA
{
    public partial class centraltienda : customerBase
    {
        private dbop db;
        private tienda xtienda;
        private string leyenda;
        private bool flag;
        private OpenFileDialog ofd;
        private Central2 source;

        public centraltienda()
        {            
            this.global_settings();

        }

        public centraltienda(Central2 source)
        {            
            this.source = source;

            this.global_settings();

            this.ShowInTaskbar = false;
        }

        private void global_settings()
        {
            InitializeComponent();
            this.FormClosed += (sender, args) => { this.source.tienda = null; };
            this.txtrfc.MaxLength = 13;
            this.txtcp.MaxLength = 5;

            behaviorDefinitions.txtOnlyNumbers(ref this.txtnumero);
            behaviorDefinitions.txtOnlyNumbers(ref this.txtcp);
            behaviorDefinitions.txtOnlyNumbers(ref this.txttelefono);

            this.Text = "Tienda";

            this.txtnombre.TextChanged += (sender, args) => { this.flag = true; };
            this.btnloadimage.TextChanged += (sender, args) => { this.flag = true; };
            this.txtcalle.TextChanged += (sender, args) => { this.flag = true; };
            this.txtnumero.TextChanged += (sender, args) => { this.flag = true; };
            this.txtcolonia.TextChanged += (sender, args) => { this.flag = true; };
            this.txtcp.TextChanged += (sender, args) => { this.flag = true; };
            this.txtciudad.TextChanged += (sender, args) => { this.flag = true; };
            this.txtestado.TextChanged += (sender, args) => { this.flag = true; };
            this.txtpais.TextChanged += (sender, args) => { this.flag = true; };
            this.txttelefono.TextChanged += (sender, args) => { this.flag = true; };
            this.txtrazonsocial.TextChanged += (sender, args) => { this.flag = true; };
            this.txtrfc.TextChanged += (sender, args) => { this.flag = true; };
            this.pboximagen.BackgroundImageChanged += (sender, args) => { this.flag = true; };


            this.txtnombre.KeyPress += (a, b) => { if (b.KeyChar == (char)Keys.Enter) this.btnloadimage.Focus(); };
            this.btnloadimage.PreviewKeyDown += (a, b) => { this.txtcalle.Focus(); };
            this.txtcalle.KeyPress += (a, b) => { if (b.KeyChar == (char)Keys.Enter) this.txtnumero.Focus(); };
            this.txtnumero.KeyPress += (a, b) => { if (b.KeyChar == (char)Keys.Enter) this.txtcolonia.Focus(); };
            this.txtcolonia.KeyPress += (a, b) => { if (b.KeyChar == (char)Keys.Enter) this.txtcp.Focus(); };
            this.txtcp.KeyPress += (a, b) => { if (b.KeyChar == (char)Keys.Enter) this.txtciudad.Focus(); };
            this.txtciudad.KeyPress += (a, b) => { if (b.KeyChar == (char)Keys.Enter) this.txtestado.Focus(); };
            this.txtestado.KeyPress += (a, b) => { if (b.KeyChar == (char)Keys.Enter) this.txtpais.Focus(); };
            this.txtpais.KeyPress += (a, b) => { if (b.KeyChar == (char)Keys.Enter) this.txttelefono.Focus(); };
            this.txttelefono.KeyPress += (a, b) => { if (b.KeyChar == (char)Keys.Enter) this.txtrazonsocial.Focus(); };
            this.txtrazonsocial.KeyPress += (a, b) => { if (b.KeyChar == (char)Keys.Enter) this.txtrfc.Focus(); };
            this.txtrfc.KeyPress += (a, b) => { if (b.KeyChar == (char)Keys.Enter) this.txtnombre.Focus(); };

            this.db = new dbop();

            this.xtienda = this.db.getTienda();


            this.leyenda = "Sin Asignar";

            this.ofd = new OpenFileDialog()
            {
                Title = "Guardar Documento",
                Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                CheckFileExists = true,
                CheckPathExists = true,
                RestoreDirectory = true
            };
        }


        private void showinitialVals()
        {
            
            foreach (Control c in this.tabcentral.Controls)
            {
                if (c is TextBox)
                    c.Text = "";
            }

            
            this.txtnombre.Text = (!(this.xtienda.nombre == null)) ? this.xtienda.nombre:this.leyenda;
            this.txtrutalogo.Text = (!(this.xtienda.logotienda == null)) ? "Logo Asignado" : this.leyenda;

            this.pboximagen.BackgroundImage = (!(this.xtienda.logotienda == null)) ? imageOperations.getImageFromBytes(this.xtienda.logotienda) :null;

            this.txtcalle.Text = (!(this.xtienda.calle == null)) ? this.xtienda.calle : this.leyenda;

            this.txtnumero.Text = this.xtienda.numero.ToString();

            this.txtcolonia.Text += (!(this.xtienda.colonia == null)) ? this.xtienda.colonia : this.leyenda;

            this.txtcp.Text+= (!(this.xtienda.cp == null)) ? this.xtienda.cp : this.leyenda;

            this.txtciudad.Text += (!(this.xtienda.ciudad == null)) ? this.xtienda.ciudad : this.leyenda;

            this.txtestado.Text += (!(this.xtienda.estado == null)) ? this.xtienda.estado : this.leyenda;

            this.txtpais.Text += (!(this.xtienda.pais == null)) ? this.xtienda.pais : this.leyenda;

            this.txttelefono.Text += (!(this.xtienda.telefono == null)) ? this.xtienda.telefono : this.leyenda;

            this.txtrazonsocial.Text += (!(this.xtienda.razonsocial == null)) ? this.xtienda.razonsocial : this.leyenda;

            this.txtrfc.Text += (!(this.xtienda.rfc == null)) ? this.xtienda.rfc : this.leyenda;

            this.flag = false;

        }

        private void update()
        {
            if (this.flag)
            {
                tienda send = new tienda();

                try
                {
                    send.calle = this.txtcalle.Text;
                    send.ciudad = this.txtciudad.Text;
                    send.colonia = this.txtcolonia.Text;
                    send.cp = this.txtcp.Text;
                    send.estado = this.txtestado.Text;
                    send.logotienda = imageOperations.getbytefromimage((Bitmap)this.pboximagen.BackgroundImage);
                    send.nombre = this.txtnombre.Text;
                    send.numero = int.Parse(this.txtnumero.Text);
                    send.pais = this.txtpais.Text;
                    send.razonsocial = this.txtrazonsocial.Text;
                    send.rfc = this.txtrfc.Text;
                    send.telefono = this.txttelefono.Text;
                    send.idtienda = this.xtienda.idtienda;                    
                }
                catch(Exception ex)
                {
                    genericDefinitions.error("Ingrese Datos Validos....\n"+ex.ToString());
                    return;
                }
                    
                if (this.db.updateTienda(send))
                {
                    genericDefinitions.ok("Datos Actualizados");
                    this.xtienda = this.db.getTienda();
                    this.showinitialVals();
                    this.txtnombre.Focus();
                }
                else
                {
                    genericDefinitions.error("Ha ocurrido un error!");                    
                    this.showinitialVals();
                }
            }
            else
            {
                genericDefinitions.dangerInfo("Datos iguales!");
            }
        }

        private void centraltienda_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.txtnombre;
            this.showinitialVals();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void btnexpand_Click(object sender, EventArgs e)
        {
            genericDefinitions.ok("Esta versión de \"Set Software\" no incorpora el motor de busqueda de Google Maps.");
        }

        private void bcancel_Click(object sender, EventArgs e)
        {
            this.showinitialVals();
        }

        private void baccept_Click(object sender, EventArgs e)
        {
            this.update();
        }

        private void btnloadimage_Click(object sender, EventArgs e)
        {
            this.chargeImagen();
        }

        private void chargeImagen()
        {
            if(this.ofd.ShowDialog() == DialogResult.OK)
            {
                Image load = Image.FromFile(this.ofd.FileName);                
                this.pboximagen.BackgroundImage = load;                            
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.pboximagen.BackgroundImage = MundoMusical.Properties.Resources.tiendaDefault;
        }
    }
}
