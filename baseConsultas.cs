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
    public partial class baseConsultas :BaseForm
    {
        public baseConsultas()
        {
            InitializeComponent();
            this.SizeChanged += (sender, args) => { this.centercruds(); };
            this.dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;          
        }

        private void baseConsultas_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                this.FormBorderStyle = FormBorderStyle.None;
            }
        }

        protected void centercruds()
        {
            this.pbox.Location = new Point(this.tp1.Width / 2 - this.pbox.Width / 2, this.pbox.Location.Y);
            this.gb1.Location = new Point(this.tp1.Width / 2 - this.gb1.Width / 2, this.gb1.Location.Y);
            this.btnprint.Location = new Point(this.gb1.Location.X + this.gb1.Width - this.btnprint.Width, this.btnprint.Location.Y);
        }
    }
}
