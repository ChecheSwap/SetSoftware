using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MundoMusical.ATOM;

namespace MundoMusical.XBASE
{
    public partial class gridquerybase : BaseForm
    {
        protected Central2 central;
        protected bool listenerSIZE = true;

        protected Point onInit;
        protected int width, height;

        protected DB.dbop db;
        public gridquerybase()
        {
            InitializeComponent();

            if (!DesignMode)
            {
                this.Resize += (x, y) =>
                {

                    if (WindowState == FormWindowState.Maximized)
                    {

                        this.pb.Location = new Point((this.gb1.Width / 2) + this.gb1.Location.X - this.pb.Width / 2, this.pb.Location.Y);
                    }

                    if (WindowState == FormWindowState.Normal)
                    {
                        this.pb.Location = new Point((this.gb1.Width / 2) + this.gb1.Location.X - this.pb.Width / 2, this.pb.Location.Y);
                    }

                };

                if(genericDefinitions.WND_BOTTOM != 0)
                {
                    this.stopBounds();
                }

            }

        }
        protected virtual void gridquerybase_Load(object sender, EventArgs e)
        {
            this.onInit = new Point(this.Location.X, this.Location.Y);
            this.width = this.Width;
            this.height = this.Height;            
        }

        private void button1_Click(object sender, EventArgs e)
        {}

        private void button2_Click(object sender, EventArgs e)
        {}

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (this.listenerSIZE)
            {
                this.listenerSIZE = false;            
                this.Height = (this.central.Height - this.central.menuStrip1.Height - this.central.panel1.Height)-20;
                this.Width = this.central.Width - 40;
                this.Location = new Point((this.central.Width - this.Width) / 2, this.central.Location.Y);
                this.blapse.BackgroundImage = global::MundoMusical.Properties.Resources.minimize;
                this.toolTip1.SetToolTip(this.blapse, "Minimizar");
            }
            else
            {
                this.blapse.BackgroundImage = global::MundoMusical.Properties.Resources.maximize;
                this.toolTip1.SetToolTip(this.blapse, "Maximizar");
                this.listenerSIZE = true;
                this.Width = this.width;
                this.Height = this.height;
                this.Location = this.onInit;
            }
        }

        protected virtual void onClose()
        {
            this.central.seecat = null;
        }

    }
}
