using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MundoMusical.CUSTOM_CONTROLS
{
    public partial class DGVCustom : System.Windows.Forms.DataGridView
    {
        public DGVCustom()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            
            base.OnPaint(pe);
            this.DoubleBuffered = true;
            this.BackgroundColor = Color.Silver;
            this.BorderStyle = BorderStyle.Fixed3D;
            this.ForeColor = Color.Black;            
            this.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Times New Roman", 12,FontStyle.Bold);
            this.Font = new Font("Times New Roman", 12, FontStyle.Regular);
            this.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            this.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSkyBlue;
            this.DefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.Gainsboro,Alignment = DataGridViewContentAlignment.MiddleCenter, SelectionBackColor = Color.LightSkyBlue};                     
            this.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.WhiteSmoke,Alignment = DataGridViewContentAlignment.MiddleCenter };           
            this.ReadOnly = true;
            this.RowHeadersVisible = false;
            this.ScrollBars = ScrollBars.Both;
            //this.AutoSizeColumnsMode = null;
            //this.AllowUserToResizeColumns = false;
            this.EnableHeadersVisualStyles = true;
            this.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Sunken;
            this.Dock = DockStyle.Fill;
            this.AllowUserToAddRows = false;
            this.AllowUserToResizeRows = false;            
        }

        protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
        {
            base.OnColumnAdded(e);
            e.Column.SortMode = DataGridViewColumnSortMode.Programmatic;
        }
    }
}
