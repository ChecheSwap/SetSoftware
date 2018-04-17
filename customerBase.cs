using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MundoMusical.CUSTOMER;

namespace MundoMusical.XBASE
{
    public partial class customerBase : BaseForm
    {
        public midformCustomer midform;

        public customerBase()
        {
            InitializeComponent();
            this.baccept.Cursor = Cursors.Hand;
            this.BackColor = DefaultBackColor;
            this.Load += setColor;
            this.KeyPreview = true;            
        }

        private void customerBase_Load(object sender, EventArgs e)
        {

        }

        public void setColor(object sender, EventArgs args)
        {
            this.BackColor = Color.AliceBlue;
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {

        }
    }
}
