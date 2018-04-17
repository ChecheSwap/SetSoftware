using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MundoMusical.DB;
using MundoMusical.CUSTOM_CONTROLS;

namespace MundoMusical.CATEGORY
{
    public partial class addCategory : XBASE.categoryBase
    {

        protected dbop db;
        protected categoria cat;
        protected Central2 central;
        public addCategory()
        {
            InitializeComponent();
            this.Text = "Agregar Categoria";
            this.setatribs();
            this.KeyPreview = true;
            this.db = new dbop();
            this.cat = new categoria();
        }

        public addCategory(Central2 central)
        {
            InitializeComponent();
            this.Text = "Agregar Categoria";
            this.setatribs();
            this.KeyPreview = true;
            this.db = new dbop();
            this.cat = new categoria();
            this.central = central;
            this.FormClosed += (sender, args) => { this.closed(); };
            this.stopBounds();

        }

        protected virtual void onAccept()
        {
            if (this.validatedata())
            {
                this.cat.nombre = this.txtnombre.Text;
                this.cat.descripcion = this.rtbdesc.Text;

                if (this.db.addcategoria(ref this.cat))
                {
                    genericDefinitions.ok("Se ha dado de alta la categoria.", "Echo");
                    this.clear();
                }
                else
                {
                    this.txtnombre.Focus();
                }
            }

        }

        protected void clear()
        {
            this.txtnombre.Text = null;
            this.rtbdesc.Text = null;
            this.txtnombre.Focus();
        }

        protected bool validatedata()
        {
            bool flag = false;

            if(this.txtnombre.Text != "" && this.rtbdesc.Text != "")
            {
                if (!(db.existcategoria(this.txtnombre.Text.Trim())))
                {
                    flag = true;
                }
                else
                {
                    genericDefinitions.dangerInfo("Esta categoria ya existe","Aviso");
                    this.txtnombre.Focus();
                }
            }
            else
            {
                genericDefinitions.dangerInfo("Llene todos los datos", "Informacion");
            }
            return flag;
        }
        protected virtual void setatribs()
        {
            behaviorDefinitions.txtupperfocus(this.txtnombre);
            this.txtnombre.KeyPress += (x, y) => {
                if (this.txtnombre.Text != "" && y.KeyChar == (char)Keys.Enter) this.rtbdesc.Focus();
            };

            this.rtbdesc.KeyDown += (x, y) =>
            {
                if(y.KeyCode == Keys.F1 && this.rtbdesc.Text != "")
                {                    
                    this.onAccept();
                }
                
            };        
        }

        private void addCategory_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.txtnombre;
        }

        protected virtual void bcancel_Click(object sender, EventArgs e)
        {
            this.clear();
        }

        protected void baccept_Click(object sender, EventArgs e)
        {
            this.onAccept();
        }

        protected virtual void closed()
        {
            this.central.addcat = null;
        }
    }
}
