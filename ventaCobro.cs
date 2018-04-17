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
using MundoMusical.DB;
using System.Globalization;

namespace MundoMusical.VENTA
{
    public partial class ventaCobro : BaseForm
    {
        dbop db;
        centralVenta source;
        private double total;
        public ventaCobro(centralVenta source)
        {
            InitializeComponent();            
            this.gb.BorderColor = Color.DarkSeaGreen;
            this.Text = "Confirmar Venta";
            this.ShowInTaskbar = false;
            this.MinimizeBox = false;
            behaviorDefinitions.txtPrice(this.txtpagocon);

            this.cbmodopago.KeyPress += (sender, args) =>
            {
                if(args.KeyChar == (char)Keys.Enter)
                {
                    this.txtpagocon.Focus();
                }
            };
            this.db = new dbop();
            this.source = source;
            this.txttotal.Text += this.source.ventatmp.total;
            this.total = this.source.ventatmp.total; 

            this.txtpagocon.KeyUp += (sender, args) =>
            {                
                if(this.txtpagocon.Text.Trim() == "")
                {
                    this.txtcambio.Text = "$";
                }
                else
                {                    
                    this.txtcambio.Text = "$"+(Double.Parse(this.txtpagocon.Text,CultureInfo.InvariantCulture)-this.total).ToString("F2");
                }                    

                if (args.KeyData == Keys.F12) {
                    this.executeventa();
                }
                                
            };

            this.cbticket.Checked = true;

        }
      
        private async void executeventa()
        {
            if (this.txtpagocon.Text == "")
            {
                genericDefinitions.error("Ingrese cantidad valida", "Alerta");
                this.txtpagocon.Focus();
            }
            else if (Double.Parse(this.txtpagocon.Text, CultureInfo.InvariantCulture) - this.total >= 0)
            {
                if (this.cbmodopago.Text.Trim() == string.Empty)
                {
                    genericDefinitions.error("Modo de Pago Invalido");
                    this.cbmodopago.Focus();
                    return;
                }
                if (genericDefinitions.yesno("¿Confirmar venta?", "Autorizar Venta"))
                {

                    this.source.modopago = this.db.getidmodopago(this.cbmodopago.Text.Trim());

                    if (await this.source.VentaFinal())
                    {
                        this.source.initializeAll();
                        this.Close();
                    }
                    else
                    {
                        this.txtpagocon.Focus();
                    }

                }
                else
                {
                    this.txtpagocon.Focus();
                }
            }
            else
            {
                genericDefinitions.error("Ingrese cantidad Valida", "Error");
                this.txtpagocon.Focus();
            }

        }
        public ventaCobro()
        {
            InitializeComponent();
            this.gb.BorderColor = Color.DarkSeaGreen;
            this.Text = "Confirmar Venta";
            this.ShowInTaskbar = false;
            this.MinimizeBox = false;
            behaviorDefinitions.txtPrice(this.txtpagocon);

            this.cbmodopago.KeyPress += (sender, args) =>
            {
                if (args.KeyChar == (char)Keys.Enter)
                {
                    this.txtpagocon.Focus();
                }
            };
            this.db = new dbop();
        }

        private void ventaCobro_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.cbmodopago;
            this.cbmodopago.DataSource = this.db.getModoPagosNombres();
        }

        private void gboxCustom1_Enter(object sender, EventArgs e)
        {
            
        }

        private void btnaccept_Click(object sender, EventArgs e)
        {
            this.executeventa();
            this.txtpagocon.Focus();
        }
    }
}
