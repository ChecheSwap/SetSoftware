using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MundoMusical.PRODUCT
{
    public partial class midformProduct :XBASE.middleformBase
    {
        addProduct addprod;
        deleteProduct delprod;
        updateProduct upprod;

        public midformProduct()
        {
            InitializeComponent();
            this.Text = "Articulos";            
        }

        public midformProduct(Central2 origen)
        {
            InitializeComponent();
            this.origen = origen;            
            this.FormClosed += this.onClose;
            this.Text = "Mercancia";
            this.stopBounds();
        }

        void onClose(object sender, EventArgs args)
        {            
            this.origen.product = null;
        }
        

        private void midformProduct_Load(object sender, EventArgs e)
        {
           
        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            this.addprod = new addProduct(this) { MdiParent = this.origen};
            this.addprod.Show();
            //this.WindowState = FormWindowState.Minimized;
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            this.upprod = new updateProduct(this) { MdiParent = this.origen};
            this.upprod.Show();
           // this.WindowState = FormWindowState.Minimized;


        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            this.delprod = new deleteProduct(this) { MdiParent = this.origen };
            this.delprod.Show();
            //this.WindowState = FormWindowState.Minimized;
        }
    }
}
