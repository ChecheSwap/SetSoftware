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
using MundoMusical.PRODUCT;

namespace MundoMusical.INVENTARIO
{
    public partial class inventarioBase :baseformextends
    {
        private altaproducto ap;
        private seeproductsinternal spi;
        private bajaproducto bp;
        private Color cfondo;
        private Color cnone;

        public inventarioBase(Central2 central)
        {
            InitializeComponent();
          
            this.central = central;
            
            this.FormClosed += (s, a) =>
            {
                this.central.inventario = null;
            };
            this.initialsetts();
        }

        public inventarioBase()
        {
            
            InitializeComponent();
            this.initialsetts();
        }

        private void initialsetts()
        {
            this.ActiveControl = this.lblinst;
            this.Text = "Inventario";

            this.SizeChanged += (sender, args) =>
            {
                this.lblinst.Location = new Point(this.panelcentral.Width / 2 - this.lblinst.Width / 2, this.panelcentral.Height/2 - this.lblinst.Height / 2);
            };

            this.cfondo = genericDefinitions.btnselectedcolor;

            this.cnone = genericDefinitions.btnfreecolor;

        }

        private void inventarioBase_Load(object sender, EventArgs e)
        {           
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void blapse_Click(object sender, EventArgs e)
        {

        }

        private void addbtn_Click(object sender, EventArgs e)
        {

            this.panelcentral.Controls.Clear();
            this.panelaltas.BackColor = this.cfondo;
            this.panelbajas.BackColor = this.cnone;
            this.panelconsultas.BackColor = this.cnone;
            this. ap = new altaproducto(this) {TopLevel = false};            
            this.panelcentral.Controls.Add(ap);
            ap.Dock = DockStyle.Fill;
            ap.Show();
        }

        private void deletebtn_Click(object sender, EventArgs e)
        {
            this.panelcentral.Controls.Clear();
            this.panelaltas.BackColor = this.cnone;
            this.panelbajas.BackColor = this.cfondo;
            this.panelconsultas.BackColor = this.cnone;
            this.bp = new bajaproducto(this) { TopLevel = false };
            this.panelcentral.Controls.Add(bp);
            bp.Dock = DockStyle.Fill;
            bp.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.panelcentral.Controls.Clear();
            this.panelaltas.BackColor = this.cnone;
            this.panelbajas.BackColor = this.cnone;
            this.panelconsultas.BackColor = this.cfondo;

            this.spi = new seeproductsinternal(this) {TopLevel = false};
            this.panelcentral.Controls.Add(spi);
            this.spi.Dock = DockStyle.Fill;
            spi.Show();
        }

    }
}
