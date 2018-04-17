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
using MundoMusical.INVENTARIO;
namespace MundoMusical.PRODUCT
{
    public partial class altaproducto : BaseForm
    {
        protected dbop db;
        protected producto prod;
        protected int codigoproducto;

        public altaproducto(inventarioBase invbase)
        {
            InitializeComponent();
            this.FormClosed += (sender, args) => { invbase.Close(); };            
            this.centercruds();
            this.nupd1.Maximum = 500000;
            this.nupd1.Minimum = 1;

            this.Resize += (sender, args) => {
                this.centercruds();
            };

            behaviorDefinitions.txtOnlyNumbers(ref this.txtcodigo);

            this.txtcodigo.KeyPress += (sender, args) => {

                if (args.KeyChar == (char)Keys.Enter)
                {
                    this.txtcodigoenter();
                }
            };

            this.db = new dbop();

            this.btnagregar.Click += (sender, args) => { this.agregar(); };

            this.nupd1.KeyPress += (sender, args) =>
            {
                if (args.KeyChar == (char)Keys.Enter)
                {
                    this.agregar();
                }
            };

            this.btnagregar.GotFocus += (sender, args) => { this.nupd1.Focus(); };

            this.btnreload.Visible = false;

            this.nupd1.GotFocus += (sender, args) => { this.nupd1.Select(0, 0); };

        }
        public altaproducto()
        {            
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.centercruds();
            this.nupd1.Maximum = 500000;
            this.nupd1.Minimum = 1;
            
            this.Resize += (sender, args) => {
                this.centercruds();
            };

            behaviorDefinitions.txtOnlyNumbers(ref this.txtcodigo);

            this.txtcodigo.KeyPress += (sender, args) => {

                if(args.KeyChar == (char)Keys.Enter)
                {
                    this.txtcodigoenter();
                }                
            };

            this.db = new dbop();

            this.btnagregar.Click += (sender, args) => { this.agregar(); };

            this.nupd1.KeyPress += (sender, args) =>
            {
                if (args.KeyChar == (char)Keys.Enter)
                {
                    this.agregar();
                }
            };

            this.btnagregar.GotFocus += (sender, args) => { this.nupd1.Focus(); };

            this.btnreload.Visible = false;                       
        }

        protected virtual void agregar()
        {
            if(genericDefinitions.yesno("¿Confirma Alta?", "Alta"))
            {
                if(this.db.surtirproducto(this.prod.idproducto, Decimal.ToInt32(this.nupd1.Value)))
                {
                    genericDefinitions.ok("Cantidad Agregada.");
                    this.cargaproducto();
                    this.nupd1.Value = 1;
                }
                else
                {
                    genericDefinitions.error("Error al agregar cantidad, Intente de nuevo");
                }
            }
        }

        protected void txtcodigoenter()
        {

            if (this.txtcodigo.Text.Trim() == "")
            {
                genericDefinitions.dangerInfo("Ingrese codigo de producto");
                this.txtcodigo.Focus();
            }
            else
            {
                
                int.TryParse(this.txtcodigo.Text.Trim(), out this.codigoproducto);
               

                if (this.db.existproduct(this.codigoproducto))
                {
                    this.cargaproducto();
                    this.txtcodigo.Text = "";
                    this.panelfirst.Visible = false;
                    this.btnreload.Visible = true;
                    this.nupd1.Focus();

                }
                else
                {
                    genericDefinitions.error("Codigo de producto Invalido");
                    this.txtcodigo.Text = "";
                    this.txtcodigo.Focus();
                }
            }
        }

        protected void cargaproducto()
        {
            this.prod = this.db.getdataproduct(this.codigoproducto);
            this.nupd1.Value = 1;
            this.txtnombre.Text = this.prod.nombre;
            this.txtidproducto.Text = this.prod.idproducto.ToString();
            this.txtstock.Text = this.prod.stock.ToString();
        }

        private void altaproducto_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                this.FormBorderStyle = FormBorderStyle.None;
            }
            this.centercruds();
            this.ActiveControl = this.txtcodigo;
        }

        protected void centercruds()
        {
            this.panel1.Location = new Point(this.tabaltas.Width / 2 - this.panel1.Width / 2, this.panel1.Location.Y);
            this.panel2.Location = new Point(this.tabaltas.Width / 2 - this.panel2.Width / 2, this.panel2.Location.Y);
            this.pbox.Location = new Point(this.tabaltas.Width/2 - this.pbox.Width/ 2, this.pbox.Location.Y);
            this.panelfirst.Location = new Point(this.tabaltas.Width / 2 - this.panelfirst.Width / 2, this.panelfirst.Location.Y);
            this.btnreload.Location = new Point(this.pbox.Location.X - this.btnreload.Width * 2, this.btnreload.Location.Y);            
        }

        protected void button2_Click(object sender, EventArgs e)
        {
            this.txtcodigoenter();               
        }

        protected void button3_Click(object sender, EventArgs e)
        {
            this.txtcodigo.Text = "";
            this.txtcodigo.Focus();
        }

        protected void button1_Click(object sender, EventArgs e)
        {
            this.panelfirst.Visible = true;
            this.btnreload.Visible = false;
            this.txtcodigo.Focus();
        }

        
        
        
    }
}
