using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MundoMusical.CUSTOMER
{
    public partial class midformCustomer : XBASE.middleformBase
    {
        private add addc;
        private customerUpdate updc;
        private delete delc;
        public midformCustomer()
        {
            InitializeComponent();
            this.Text = "Cliente";
        }
        public midformCustomer(Central2 origen)
        {
            InitializeComponent();
            this.Text = "Cliente";            
            this.origen = origen;
            this.FormClosed += this.OnClose;
            this.stopBounds();                                            
        }

        private void OnClose(object sender, EventArgs args)
        {            
            this.origen.customer = null;
        }

        private void midformCustomer_Load(object sender, EventArgs e)
        {

        }

        private void addbtn_Click(object sender, EventArgs e)
        {
            this.addc = new add(this) { MdiParent = this.origen};
           // this.WindowState = FormWindowState.Minimized;
            this.addc.Show();
        }

        private void updatebtn_Click(object sender, EventArgs e)
        {
            this.updc = new customerUpdate(this) { MdiParent = this.origen };
            //this.WindowState = FormWindowState.Minimized;
            this.updc.Show();
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            this.delc = new delete(this) { MdiParent = this.origen };
           // this.WindowState = FormWindowState.Minimized;
            this.delc.Show();
        }
    }
}
