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
using System.Threading;

namespace MundoMusical
{
   
    public partial class Login1 :BaseForm
    {
        private struct Tcomponents
        {
            public LOGIN.LogInternals Strup;            
            public Tcomponents(Login1 x)
            {
                this.Strup = new LOGIN.LogInternals(x) { TopLevel = false};
            }
        }

        Tcomponents mycomp;
        public Login1()
        {
            InitializeComponent();            
            this.Text = "Iniciar Sesión";
            this.makeinnerform();
            this.FormBorderStyle = FormBorderStyle.None;            
            this.KeyPreview = false;
            this.FormClosing += this.onClose;

            this.SizeChanged += (sender, args) => {
                if(this.WindowState == FormWindowState.Normal)
                {
                    if(this.mycomp.Strup.WindowState == FormWindowState.Minimized)
                    {
                        this.mycomp.Strup.WindowState = FormWindowState.Normal;
                        this.centralpanel.Controls.Clear();
                        this.makeinnerform();
                    }
                }
            };
        }

        private void makeinnerform()
        {
            this.mycomp = new Tcomponents(this);
            this.centralpanel.Controls.Add(this.mycomp.Strup);
            this.mycomp.Strup.Show();
        }

        private void Login1_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.mycomp.Strup.txtuser;
            this.mycomp.Strup.txtuser.Focus();            
        }
    }
}
