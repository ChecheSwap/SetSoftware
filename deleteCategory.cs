using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MundoMusical.CATEGORY
{
    public partial class deleteCategory : updateCategory
    {
        public deleteCategory()
        {
            InitializeComponent();
            this.Text = "Eliminar Categoria";
            this.KeyPreview = true;
            this.KeyDown += (abs, det) => { if (det.KeyData == Keys.F1) this.onAccept(); };
        }
        public deleteCategory(Central2 central)
        {
            InitializeComponent();
            this.Text = "Eliminar Categoria";
            this.KeyPreview = true;
            this.KeyDown += (abs, det) => { if (det.KeyData == Keys.F1) this.onAccept(); };
            this.central = central;
            this.FormClosed += (sender, args) => { this.closed(); };
            this.stopBounds();
        }

        private void deleteCategory_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                this.txtnombre.Enabled = false;
                this.rtbdesc.Enabled = false;
                this.Height = 287;
                this.ActiveControl = this.start;
            }
        }

        protected override void onAccept()
        {
            if (this.db.existcategoriaproductos(this.cat.idcategoria))
            {
                genericDefinitions.error("No se puede eliminar esta categoria, consulte al administrador.", "Seguridad");
                this.initvals();
                return;
            }

            if (this.db.deletecategoria(this.cat.idcategoria))
            {
                genericDefinitions.ok("Se ha eliminado la categoria.", "Echo");                
            }

            this.initvals();
        }
        protected override void initvals()
        {
            this.Height = 287;
            this.start.Text = null;
            this.gb.Visible = false;
            this.gb2.Visible = true;            
            this.ActiveControl = start;
        }
        protected override void closed()
        {
            this.central.delcat = null;
        }
    }
}
