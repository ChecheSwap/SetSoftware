using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace MundoMusical.CUSTOM_CONTROLS
{
    public static class behaviorDefinitions
    {
        /// <summary>
        /// TEXTO A MAYUSCULAS CADA VEZ QUE SE INGRESA TECLA
        /// </summary>
        /// <param name="control"></param>
        public static void txtUPPER(TextBox control)
        {
            control.KeyUp += (x, y) => { control.Text = control.Text.ToUpper(); control.SelectionStart = control.Text.Length; };
        }
        /// <summary>
        /// SOLO NUMEROS Y PERMITE TECLA BACKSPACE
        /// </summary>
        /// <param name="control"></param>
        public static void txtOnlyNumbers(ref TextBox control)
        {            
            control.KeyPress += (sender, args) => { if ((char.IsDigit(args.KeyChar)) || (args.KeyChar == (char)Keys.Back)) args.Handled = false; else args.Handled = true; };
        }
        /// <summary>
        /// SOLO NUMEROS
        /// </summary>
        /// <param name="control"></param>
        public static void txtOnlyDigits(ref TextBox control)
        {
            control.KeyPress += (sender, args) => { if (!(char.IsDigit(args.KeyChar))) args.Handled = true; };
        }
        /// <summary>
        /// CONVIERTE A MAYUSCULAS EL TEXTO DEL CONTROL AL PERDER EL FOCO
        /// </summary>
        /// <param name="control"></param>
        public static void txtupperfocus(TextBox control)
        {
            control.LostFocus += (sender, args) => { control.Text = control.Text.ToUpper(); };
        }
        /// <summary>
        /// COMPORTAMIENTO PARA INGRESAR PRECIOS CON PUNTO DECIMAL
        /// </summary>
        /// <param name="control"></param>
        public static void txtPrice( TextBox control)
        {
            control.KeyPress += (sender, args) => {
                bool point = false;
                
                if (control.Text.Contains("."))
                {
                    point = true;
                }
                   
                if (char.IsDigit(args.KeyChar) || args.KeyChar == char.Parse(".")||args.KeyChar == (char)Keys.Back)
                {
                    if(args.KeyChar == char.Parse("."))
                    {                                                
                        if(point)
                        args.Handled = true;
                    }
                }
                else
                {
                    args.Handled = true;
                }                

            };
        }
    }
}
