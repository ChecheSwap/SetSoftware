using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MundoMusical.CONFIGURACION_DE_IMPRESION
{
    public partial class printWarning : BaseForm
    {
        private Size init;
        private Size after;
        public printWarning()
        {
            InitializeComponent();
            this.Text = "Excepción de Impresión";
            this.init = new Size(648, 247);
            this.after = new Size(648, 390);
            this.MinimizeBox = false;
        }

        private void printWarning_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.button1;
            this.button1.Focus();
        }

        private void btnbase1_Click(object sender, EventArgs e)
        {
            if(this.Size == init)
            {
                this.Size = after;
            }
            else
            {
                this.Size = init;
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
