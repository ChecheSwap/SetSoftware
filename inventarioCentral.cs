using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MundoMusical.INVENTARIO
{
    public partial class inventarioCentral : BaseForm
    {
        public inventarioCentral()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.Text = "Inventario";
            this.LostFocus += (x, y) => { this.WindowState = FormWindowState.Minimized; };
        }

        private void inventarioCentral_Load(object sender, EventArgs e)
        {

        }
    }
}
