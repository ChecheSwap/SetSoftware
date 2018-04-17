using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MundoMusical.CUSTOM_CONTROLS;

namespace MundoMusical.PAYMODE
{
    public partial class updatePaymode : addPayMode
    {
        public updatePaymode()
        {
            InitializeComponent();
            this.Text = "Actualizar Modo De Pago";
            this.txtname.KeyPress += (sender, args) =>
            {
                if (this.txtname.Text.Trim() != "" && args.KeyChar == (char)Keys.Enter)
                {
                    this.check();
                }
            };
            behaviorDefinitions.txtUPPER(this.txtname);
            
        }
        public updatePaymode(Central2 central)
        {
            InitializeComponent();
            this.central = central;
            this.Text = "Actualizar Modo De Pago";
            this.txtname.KeyPress += (sender, args) =>
            {
                if (this.txtname.Text.Trim() != "" && args.KeyChar == (char)Keys.Enter)
                {
                    this.check();
                }
            };
            behaviorDefinitions.txtUPPER(this.txtname);
            this.stopBounds();
            this.FormClosed += (sender, args) => { this.onclosed(); };
        }

        protected override void oncancel()
        {
            this.initvals();
        }

        protected override void onaccept()
        {
            if(this.txtnombre.Text == "" || this.rtb.Text == "")
            {
                genericDefinitions.error("Llene todos los campos", "Error");
            }
            else if(this.txtnombre.Text == this.modo.nombre)
            {
                this.modo.otros_detalles = this.rtb.Text;

                this.update();
            }
            else if(this.db.existmodopago(this.txtnombre.Text))
            {
                genericDefinitions.error("Este modo de pago ya existe!", "Error");
                this.txtnombre.Focus();
            }
            else
            {
                this.modo.nombre = this.txtnombre.Text;
                this.modo.otros_detalles = this.rtb.Text;
                this.update();
            }
        }

        private void update()
        {            

            if (this.db.updatemodopago(this.modo))
            {
                genericDefinitions.ok("Listo", "Echo");
                this.initvals();
            }
            else
            {
                this.txtnombre.Focus();
            }
        }

        protected override void onclosed()
        {
            this.central.updpay = null;
        }
        private void updatePaymode_Load(object sender, EventArgs e)
        {
            this.initvals();
        }

        protected void initvals()
        {
            this.Size = new Size(502,312);            
            this.panel.Parent = this;
            this.panel.Visible = true;
            this.gb.Visible = false;
            this.ActiveControl = this.txtname;
            this.txtname.Text = null;
        }

        protected void secondvals()
        {
            this.Size = new Size(502, 467);            
            this.panel.Visible = false;
            this.gb.Visible = true;
            this.txtnombre.Focus();

            this.filldata();
        }

        protected void filldata()
        {
            this.txtnombre.Text = this.modo.nombre;
            this.rtb.Text = this.modo.otros_detalles;
        }

        protected void check()
        {
            if (this.db.existmodopago(this.txtname.Text))
            {
                this.modo = this.db.getmodopago(this.txtname.Text);
                this.secondvals();
            }
            else
            {
                genericDefinitions.error("No existe modo de pago", "Error");
                this.txtname.Text = null;
                this.txtname.Focus();
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            this.txtname.Text = null;
            this.txtname.Focus();
        }
    }
}
