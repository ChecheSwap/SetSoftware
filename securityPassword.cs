using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MundoMusical.DB.DUMPS
{
    public partial class securityPassword : BaseForm
    {
        private dbop db;
        public bool result = false;
        public securityPassword()
        {
            InitializeComponent();
            this.Text = "Seguridad SMP";
            this.db = new dbop();
            this.MinimizeBox = false;

            this.txtpass.KeyPress += (a, b) => { if (b.KeyChar == (char)Keys.Enter) this.verify(); };
        }

        private void securityPassword_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.txtpass;
        }

        private void verify()
        {
            if (genericDefinitions.globalsesion != null)
            {
                if (this.txtpass.Text.Trim() == this.db.usrget(genericDefinitions.globalsesion.idusuario).contraseña + "setsoftware")
                {
                    this.result = true;

                    this.Close();
                }
                else
                {
                    genericDefinitions.error("Password Incorrecto");
                    this.txtpass.Text = "";
                    this.txtpass.Focus();
                }
            }
            else
            {
                genericDefinitions.error("Imposible Realizar la Operación.");
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.verify();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.txtpass.Text = "";
        }
    }
}
