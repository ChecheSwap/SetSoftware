using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MundoMusical.CUSTOM_CONTROLS
{
    public class TXTBOXOnlyRead:TextBox
    {
        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);
        public TXTBOXOnlyRead()
        {
            this.ReadOnly = true;
            this.Cursor = Cursors.Arrow;
            //this.BorderStyle = BorderStyle.None;
            this.GotFocus += (sender, args) =>
            {
                HideCaret(this.Handle);
            };
        }
    }
}
