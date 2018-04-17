using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MundoMusical.XBASE;

namespace MundoMusical.DB.DUMPS
{
    public partial class inneradvice : Form
    {
        public inneradvice()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ShowIcon = false;
        }

        public inneradvice(string texto)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.lblver.Text = texto;
            this.ShowIcon = false;
        }

        private void onExport_Load(object sender, EventArgs e)
        {
            this.centerlabel();
        }

        private void centerlabel()
        {
            this.lblver.Location = new Point(this.Width / 2 - this.lblver.Width / 2, this.lblver.Location.Y);
        }

        public void settext(string texto)
        {
            this.lblver.Text = texto;            
        }
    }
}
