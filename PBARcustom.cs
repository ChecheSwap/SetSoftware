using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MundoMusical.CUSTOM_CONTROLS
{
    public class PBARcustom:ProgressBar
    {
        public PBARcustom()
        {
            this.SetStyle(ControlStyles.UserPaint, true); 
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rec = e.ClipRectangle;

            rec.Width = (int)(rec.Width * ((double)Value / Maximum)) - 4;

            if (ProgressBarRenderer.IsSupported)
            {
                ProgressBarRenderer.DrawHorizontalBar(e.Graphics, e.ClipRectangle);
            }

            rec.Height = rec.Height - 4;

            e.Graphics.FillRectangle(Brushes.Black, 2, 2, rec.Width, rec.Height);
        }
    }
}


