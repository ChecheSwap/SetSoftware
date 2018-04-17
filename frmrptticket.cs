using CrystalDecisions.Shared;
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

namespace MundoMusical.TICKET.CRYSTAL_RPT_TICKET
{
    public partial class frmrptticket : crystalrptformBase
    {
        private rptticket reporte;
        public frmrptticket()
        {
            InitializeComponent();
        }

        public frmrptticket(rptticket reporte):base(new DataTable())
        {
            InitializeComponent();
            this.reporte = reporte;
            this.FormClosed += (sender, args) => { this.reporte.Close(); this.reporte.Dispose(); };            
        }

        public frmrptticket(rptticket reporte, ParameterFields listparams):base(new DataTable())
        {
            InitializeComponent();
            this.reporte = reporte;
            this.FormClosed += (sender, args) => { this.reporte.Close(); this.reporte.Dispose(); };
            this.crv.ParameterFieldInfo = listparams;
            this.crv.ReuseParameterValuesOnRefresh = false;
        }

        private void frmrptticket_Load(object sender, EventArgs e)
        {
            this.crv.ReportSource = this.reporte;
            this.crv.ShowPrintButton = false;
            this.crv.ShowCloseButton = false;
            this.crv.ShowCopyButton = false;
            this.crv.ShowGroupTreeButton = false;
            this.crv.ShowRefreshButton = false;                        
        }
    }
}
