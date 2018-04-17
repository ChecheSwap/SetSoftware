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
namespace MundoMusical.CATEGORY
{
    public partial class updateCategory : addCategory
    {
        protected int catid;
        public updateCategory()
        {
            InitializeComponent();
            this.Text = "Actualizar Categoria";
            this.initvals();
            this.start.KeyPress += (send, args) => {
                if (args.KeyChar == (char)Keys.Enter && this.start.Text != "")
                    this.setcat();
                if (this.start.Text == "" && args.KeyChar == (char)Keys.Enter)
                {
                    genericDefinitions.dangerInfo("Ingrese Nombre de Categoria!", "Aviso");
                    this.start.Focus();
                }

            };

            behaviorDefinitions.txtUPPER(this.start);
        }

        public updateCategory(Central2 central)
        {
            InitializeComponent();
            this.Text = "Actualizar Categoria";
            this.initvals();
            this.start.KeyPress += (send, args) => {
                if (args.KeyChar == (char)Keys.Enter && this.start.Text != "")
                    this.setcat();
                if (this.start.Text == "" && args.KeyChar == (char)Keys.Enter)
                {
                    genericDefinitions.dangerInfo("Ingrese Nombre de Categoria!", "Aviso");
                    this.start.Focus();
                }

            };
            this.central = central;
            this.FormClosed += (sender, args) => { this.closed(); };
            behaviorDefinitions.txtUPPER(this.start);
            this.stopBounds();
        }

        protected override void onAccept()
        {
            if(this.txtnombre.Text == "" || this.rtbdesc.Text == "")
            {
                genericDefinitions.dangerInfo("Ingrese valores", "Advertencia");
                this.txtnombre.Focus();
                return;
            }

            if (this.txtnombre.Text != this.cat.nombre)
                if (this.db.existcategoria(this.txtnombre.Text))
                {
                    genericDefinitions.error("Esta categoria ya existe!", "Error");
                    this.txtnombre.Focus();
                    return;
                }


            this.cat.nombre = this.txtnombre.Text;
            this.cat.descripcion = this.rtbdesc.Text;

            if (this.db.updatecategoria(this.cat))
            {
                genericDefinitions.ok("Se ha Actualizado categoria.", "Echo");
                this.initvals();
            }
            else
            {
                genericDefinitions.error("No se ha podido actualizar", "Error");
                this.txtnombre.Focus();
            }

        }

        protected override void bcancel_Click(object sender, EventArgs e)
        {
            this.initvals();
        }
        protected virtual void initvals()
        {
            this.start.Text = "";
            this.gb.Visible = false;
            this.gb2.Visible = true;
            this.Height = 287;
            this.ActiveControl = start;
        }

        protected void setcat()
        {
            if (this.db.existcategoria(this.start.Text))
            {
                this.catid = db.getcategoriaid(this.start.Text);
                this.cat = db.getcategoria(this.catid);
                this.gb2.Visible = false;
                this.gb.Visible = true;
                this.txtnombre.Text = this.cat.nombre;
                this.rtbdesc.Text = this.cat.descripcion;
                this.Height = 467;
                this.txtnombre.Focus();
            }
            else
            {
                genericDefinitions.error("No existe dicha categoria!", "Error");                
            }
            
        }

        private void updateCategory_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.start;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.start.Text = "";
            this.ActiveControl = this.start;
        }
        protected override void closed()
        {
            this.central.updcat = null;
        }
    }
}
