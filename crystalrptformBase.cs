using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MundoMusical.XBASE
{
    public partial class crystalrptformBase : BaseForm
    {
        protected DataTable dt;
        protected Size dimension;
        public crystalrptformBase()
        {
            this.InitializeComponent();
        }
        public crystalrptformBase(DataTable dt)
        {
            InitializeComponent();            
            this.dt = dt;
            this.ShowInTaskbar = false;            
            this.MinimizeBox = false;
            this.MaximizeBox = true;
           
        }

        private void crystalrptformBase_Load(object sender, EventArgs e)
        {
            this.crv.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            this.crv.ShowParameterPanelButton = false;            
            this.Text = "Vista de Impresión";
            this.Size = new Size(1280, 920);
            this.CenterToScreen();
        }
    }
}
