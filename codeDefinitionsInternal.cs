using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MundoMusical
{
   public  class codeDefinitionsInternal
    {
        

        public codeDefinitionsInternal()
        {}
        public void onlyNumbers_TXTCURP(object e, KeyPressEventArgs me)
        {//97-122
            if ((!(me.KeyChar >= 48 && 57 >= me.KeyChar)) && (!(me.KeyChar >= 65 && 90 >= me.KeyChar)) && (!(me.KeyChar >= 90 && 122 >= me.KeyChar)))
            {
                if(!(8 == me.KeyChar))
                    me.Handled = true;
            }
                
        }
    }
}
