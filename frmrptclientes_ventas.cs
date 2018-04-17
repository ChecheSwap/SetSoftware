using MundoMusical.XBASE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MundoMusical.CONSULTAS.CLIENTES.CLIENTES_VENTAS
{
    public partial class frmrptclientes_ventas : crystalrptformBase
    {
        private rptclientes_ventas reporte;
        public frmrptclientes_ventas(DataTable dt):base(dt)
        {
            InitializeComponent();                                 
            this.FormClosed += (sender, args) => { this.reporte.Close(); this.reporte.Dispose();};
            this.reporte = new rptclientes_ventas();                     
        }
        public frmrptclientes_ventas()
        {
            InitializeComponent();            
        }

        private void frmrptclientes_ventas_Load(object sender, EventArgs e)
        {
            try
            {
                this.reporte.SetDataSource(this.dt);
                this.crv.ReportSource = this.reporte;
            }
            catch (Exception ex)
            {
                genericDefinitions.error(ex.ToString());
            }
                    
        }
    }
}
