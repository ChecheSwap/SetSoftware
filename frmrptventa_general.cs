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

namespace MundoMusical.CONSULTAS.VENTAS.VENTAS_GENERAL
{
    public partial class frmrptventa_general : crystalrptformBase
    {
        private rptventa_general reporte;

        string total = null;
        public frmrptventa_general()
        {
            this.configs();
            InitializeComponent();
        }
        public frmrptventa_general(DataTable tabla):base(tabla)
        {
            this.configs();
            InitializeComponent();
        }

        public frmrptventa_general(DataTable tabla, string total) : base(tabla)
        {
            this.configs();
            InitializeComponent();

            this.total = total;
        }

        private void configs()
        {
            this.reporte = new rptventa_general();
            this.FormClosed += (sender, args) => { this.reporte.Close(); this.reporte.Dispose(); };
        }
        private void frmrptventa_general_Load(object sender, EventArgs e)
        {
            try
            {                
                this.reporte.SetDataSource(this.dt);
                this.reporte.SetParameterValue("total",this.total);

                this.crv.ReportSource = this.reporte;
                this.crv.Refresh();                                                      
            }
            catch(Exception ex)
            {
                genericDefinitions.error(ex.ToString());
            }
        }
    }
}
