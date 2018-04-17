using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Forms;

namespace MundoMusical.PRODUCT.SEE_PRODUCTS_INTERNAL
{
    public partial class rptseeproductsinternal : BaseForm
    {
        private rptproductos1 myrpt;        
        private DataTable dt;

        public rptseeproductsinternal(DataTable ds)
        {
            InitializeComponent();
            this.Text = "Vista de Impresion";
            this.myrpt = new rptproductos1();
            this.dt = ds;
            this.ShowInTaskbar = false;
            this.FormClosed += (sender, args)=>{ this.myrpt.Close(); this.myrpt.Dispose(); };
            this.MinimizeBox = false;
            this.MaximizeBox = true;
        }

        private void rptseeproductsinternal_Load(object sender, EventArgs e)
        {
            try
            {
                this.myrpt.SetDataSource(this.dt);
                this.myrpt.SummaryInfo.ReportTitle = "VISTA PRODUCTOS";
                this.myrpt.SummaryInfo.ReportAuthor = "SET SOFTWARE";
                this.crv.ReportSource = myrpt;
                this.crv.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;                
            }
            catch(Exception ex)
            {
                genericDefinitions.error(ex.ToString());
            }
        }
    }
}
