using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MundoMusical.XBASE
{
    public partial class baseformextends : BaseForm
    {
        protected Central2 central;
        protected bool listenerSIZE = true;
        protected Point onInit;
        protected int width, height;

        public baseformextends()
        {
            this.exitFlag = exitState.highest;
            InitializeComponent();
            
            if (!DesignMode)
            {
                if (genericDefinitions.WND_BOTTOM != 0)
                {
                    this.stopBounds();
                }
            }
        }

        private void blapse_Click(object sender, EventArgs e)
        {
            if (this.listenerSIZE)
            {
                this.maximiza();
            }
            else
            {
                this.minimiza();
            }
        }

        protected void maximiza()
        {
            this.listenerSIZE = false;
            this.Height = (this.central.Height - this.central.menuStrip1.Height - this.central.panel1.Height) - 20;
            this.Width = this.central.Width - 40;
            this.Location = new Point((this.central.Width - this.Width) / 2, this.central.Location.Y);
            this.blapse.BackgroundImage = global::MundoMusical.Properties.Resources.minimize;
            this.toolTipBASE.SetToolTip(this.blapse, "Minimizar");
            this.pb.Location = new Point(this.Width / 2, this.pb.Location.Y);
        }

        protected void minimiza()
        {
            this.blapse.BackgroundImage = global::MundoMusical.Properties.Resources.maximize;
            this.toolTipBASE.SetToolTip(this.blapse, "Maximizar");
            this.listenerSIZE = true;
            this.Width = this.width;
            this.Height = this.height;
            this.Location = this.onInit;
            this.pb.Location = new Point(this.Width / 2 - (this.blapse.Width * 2), this.pb.Location.Y);
        }

        private void baseformextends_Load(object sender, EventArgs e)
        {
            this.onInit = new Point(this.Location.X, this.Location.Y);
            this.width = this.Width;
            this.height = this.Height;
        }
    }
}
