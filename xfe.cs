using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MundoMusical.EFFECTS
{
    public static class xfe
    {
        public static void blendOpen(Control c, int millis)
        {
            Util.Animate(c, Util.Effect.Blend, 0, 180);
            Util.Animate(c, Util.Effect.Blend, millis, 180);
        }

        public static void slide(Control c, int millis)
        {
            Util.Animate(c, Util.Effect.Slide, millis, 180);
        }

        public static void blend(Control c , int millis)
        {
            Util.Animate(c, Util.Effect.Blend, millis, 180);
        }
        public static void center(Control c, int millis)
        {
            Util.Animate(c, Util.Effect.Center, millis, 180);
        }
        public static void roll(Control c, int millis)
        {
            Util.Animate(c, Util.Effect.Roll, millis, 180);
        }

        internal static void center(int v)
        {
            throw new NotImplementedException();
        }
    }
}
