using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MundoMusical.DB;
using MundoMusical.CUSTOM_CONTROLS;

namespace MundoMusical.PAYMODE
{
    public partial class addPayMode : XBASE.paymodeBase
    {
        protected dbop db;
        protected modo_pago modo;
        protected Central2 central;
        public addPayMode()
        {
            InitializeComponent();
            this.Text = "Agregar Modo de pago";

            this.db = new dbop();
            this.modo = new modo_pago();

            behaviorDefinitions.txtUPPER(this.txtnombre);

            this.txtnombre.KeyPress += (sender, args) =>
            {
                if(args.KeyChar == (char)Keys.Enter && this.txtnombre.Text != "")
                {
                    this.rtb.Focus();
                }
            };

            this.rtb.PreviewKeyDown += (sender, args) =>
            {
                if (this.rtb.Text.Trim() != "" && args.KeyCode == Keys.F1)
                {
                    this.onaccept();
                }
            };

            this.bcancel.Click += (x, y) => { this.oncancel(); };
            this.baccept.Click += (x, y) => { this.onaccept(); };

        }

        public addPayMode(Central2 central)
        {
            InitializeComponent();
            this.central = central;
            this.Text = "Agregar Modo de pago";

            this.db = new dbop();
            this.modo = new modo_pago();

            behaviorDefinitions.txtUPPER(this.txtnombre);

            this.txtnombre.KeyPress += (sender, args) =>
            {
                if (args.KeyChar == (char)Keys.Enter && this.txtnombre.Text != "")
                {
                    this.rtb.Focus();
                }
            };

            this.rtb.PreviewKeyDown += (sender, args) =>
            {
                if (this.rtb.Text.Trim() != "" && args.KeyCode == Keys.F1)
                {
                    this.onaccept();
                }
            };

            this.bcancel.Click += (x, y) => { this.oncancel(); };
            this.baccept.Click += (x, y) => { this.onaccept(); };

            this.FormClosed += (x, y) =>
            {
                this.onclosed();
            };

            this.stopBounds();
        }

        protected virtual void onclosed()
        {
            this.central.addpay = null;
        }
        protected virtual void oncancel()
        {
            this.clear();
        }
        protected virtual void onaccept()
        {
            if (this.validatedata())
            {
                if (this.db.existmodopago(this.modo.nombre))
                {
                    genericDefinitions.error("Este modo de pago ya existe", "Error");
                    this.txtnombre.SelectionStart = this.txtnombre.Text.Length;
                    this.txtnombre.Focus();
                }
                else
                {
                    if (this.db.insertmodopago(this.modo))
                    {
                        genericDefinitions.ok("Se ha dado de alta el modo de pago", "Echo");
                        this.clear();
                    }
                }
            }
            else
            {
                genericDefinitions.dangerInfo("Llene todos los campos.", "Advertencia");                
                this.txtnombre.SelectionStart=this.txtnombre.Text.Length;
                this.txtnombre.Focus();
            }
        }

        private void clear()
        {
            this.rtb.Text = "";
            this.txtnombre.Text = "";
            this.txtnombre.Focus();
        }
        protected bool validatedata()
        {
            if (this.txtnombre.Text.Trim() != "" && this.rtb.Text.Trim() != "")
            {
                this.modo.nombre = this.txtnombre.Text;
                this.modo.otros_detalles = this.rtb.Text.Trim();
                return true;
            }
                
            return false;                            
        }

        private void addPayMode_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.txtnombre;
        }
    }
}
