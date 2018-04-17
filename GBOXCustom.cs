using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
namespace MundoMusical.CUSTOM_CONTROLS
{
    public class GBOXCustom : GroupBox
    {
        private Color borderColor;

        public Color BorderColor
        {
            get { return this.borderColor; }
            set { this.borderColor = value; }
        }

        public GBOXCustom()
        {
            this.borderColor = Color.Black;
            this.LostFocus += (a, b) => { this.Refresh(); };
            this.GotFocus += (a, b) => { this.Refresh(); };

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Color color = Color.Gray;
                        
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                      color, 1, ButtonBorderStyle.Solid,
                      color, 1, ButtonBorderStyle.Solid,
                      color, 1, ButtonBorderStyle.Solid,
                      color, 1, ButtonBorderStyle.Solid);
            /*
            Size tSize = TextRenderer.MeasureText(this.Text, this.Font);
            Rectangle borderRect = e.ClipRectangle;
            borderRect.Y += tSize.Height / 2;
            borderRect.Height -= tSize.Height / 2;
            ControlPaint.DrawBorder(e.Graphics, borderRect, this.borderColor, ButtonBorderStyle.Solid);
            Rectangle textRect = e.ClipRectangle;
            textRect.X += 6;
            textRect.Width = tSize.Width;
            textRect.Height = tSize.Height;
            e.Graphics.FillRectangle(new SolidBrush(this.BackColor), textRect);
            e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), textRect);            
            */
        }
    }
}
