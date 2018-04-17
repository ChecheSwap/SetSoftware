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

namespace MundoMusical.CONSULTAS.CLIENTES.CLIENTES_GENERAL
{
    public partial class frmrptclientes_general : crystalrptformBase
    {
        private rptclientes_general reporte;

        public frmrptclientes_general(DataTable dt):base(dt)
        {
            InitializeComponent();
            this.reporte = new rptclientes_general();
            
            this.FormClosed += (sender, args) => { this.reporte.Close(); this.reporte.Dispose();};                 
               
        }

        private void frmrptclientes_general_Load(object sender, EventArgs e)
        {
            
            try
            {
                this.reporte.SetDataSource(this.dt);
                this.crv.ReportSource = this.reporte;
            }
            catch(Exception ex)
            {
                genericDefinitions.error(ex.ToString());
            }
                            
        }
    }
}
