using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MundoMusical.PAYMODE
{
    public partial class deleteModopago : updatePaymode
    {
        public deleteModopago()
        {
            InitializeComponent();

            this.txtnombre.Enabled = false;
            this.rtb.Enabled = false;
            this.Text = "Eliminar Modo de pago";
            this.KeyPreview = true;
            this.KeyDown += (x, y) =>
            {
                if (y.KeyCode == Keys.F1)
                {
                    this.onaccept();
                }
            };
            this.label3.Text = "Presione F1 para Eliminar.";
        }

        protected override void onclosed()
        {
            this.central.delpay = null;
        }
        public deleteModopago(Central2 central) : base(central)
        {
            InitializeComponent();
            this.central = central;
            this.txtnombre.Enabled = false;
            this.rtb.Enabled = false;
            this.Text = "Eliminar Modo de pago";
            this.KeyPreview = true;
            this.KeyDown += (x, y) =>
            {
                if (y.KeyCode == Keys.F1)
                {
                    this.onaccept();
                }
            };
            this.label3.Text = "Presione F1 para Eliminar.";
            this.stopBounds();
        }
        private void deleteModopago_Load(object sender, EventArgs e)
        {           
        }

        protected override void onaccept()
        {
            if (this.db.modopagoinventas(this.modo.idmodopago))
            {
                genericDefinitions.error("No se puede eliminar este modo de pago, consulte al Administrador del Sistema.", "Error");
                this.initvals();
            }
            else
            {
                if(genericDefinitions.yesno("Esta seguro que desea eliminar este modo de pago? ", "Confirmacion"))
                {
                    if (this.db.deletemodopago(this.modo.idmodopago))
                    {
                        genericDefinitions.ok("Se ha eliminado", "Echo");
                        this.initvals();
                    }
                    
                }
                else
                {
                    this.initvals();
                }
            }              
        }

    }
}
