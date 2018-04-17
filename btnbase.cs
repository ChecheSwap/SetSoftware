using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MundoMusical.CUSTOM_CONTROLS
{
    public class btnbase:Button
    {
        public btnbase()
        {
            this.BackColor = System.Drawing.Color.Transparent;
            this.FlatStyle = FlatStyle.Flat;
            //this.FlatAppearance.BorderColor = System.Drawing.Color.Transparent;
            this.FlatAppearance.BorderSize = 0;
            this.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.Cursor = Cursors.Hand;
        }
    }
}
