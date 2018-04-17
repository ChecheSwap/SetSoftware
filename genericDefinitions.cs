using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.ComponentModel;

namespace MundoMusical
{
    
    public static class genericDefinitions
    {
        //RUTAS PATH

        public static string XMLSCHEMASPATH = Path.Combine(Application.StartupPath.ToString(),"XML_SCHEMAS"); //RUTA PARA LA CREACION DE OBJETOS XML BASANDONOS EN XML SCHEMA WRITER MI CLASE
        public static string RESPALDOSEGURIDADDBPATH = Path.Combine(Application.StartupPath.ToString(), "RESPALDOS SEGURIDAD"); //RUTA PARA LA CREACION DE LOS RESPALDOS DE SEGURIDAD DE LA BASE DE DATOS
        public static string TICKETLOGOSPATH = Path.Combine(Application.StartupPath.ToString(), "LOGOS TICKET"); //RUTA PARA GUARDAR LOS LOGOS TEMPORALES DEL TICKET
        public static string CODEBARSPATH = Path.Combine(Application.StartupPath.ToString(), "CODEBARS"); //RUTA PARA GUARDAR LOS CODEBARS QUE SE VAN A IMPRIMIR
        
        /// <summary>
        /// VARIABLES QUE CONTIENEN LOS PATH DE LOS DIRECTORIOS PARA USO ESPECIFICO
        /// </summary>

        public static Color btnselectedcolor = Color.DarkGray;
        public static Color btnfreecolor = Color.Transparent;

        public static string ALIAS;

        public static int WND_TOP;
        public static int WND_BOTTOM;
        public static int WND_RIGHT;
        public static int WND_LEFT;

        public static bool closeAccess = true; //CANDADO PARA PROCEDER CIERRE TOTAL DE LA APLICACION

        public static login globalsesion; // VAR GLOBAL PARA REGISTRO DE SESION      
        
        public static bool ifclose()
        {
            DialogResult result = MessageBox.Show("¿Desea Salir?","SET SOFTWARE", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                return true;
            else
                return false;
        } 
        
        public static bool ask(string quest, string title)
        {
            DialogResult dr = MessageBox.Show(quest, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                return true;
            }

            return false;
        }

        public static bool ask(string quest)
        {
            DialogResult dr = MessageBox.Show(quest, "Mundo Musical", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dr == DialogResult.Yes)
            {
                return true;
            }

            return false;
        }

        public static void dangerInfo(string info, string title)
        {
            MessageBox.Show(info, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public static void dangerInfo(string info)
        {
            MessageBox.Show(info, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public static void error(string msg, string title)
        {
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void error(string msg)
        {
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ok(string msg, string title)
        {
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ok(string msg)
        {
            MessageBox.Show(msg, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static bool yesno(string msg,string title)
        {
            DialogResult dt = MessageBox.Show(msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dt == DialogResult.Yes)
                return true;
            return false;
        }

        public static string getMinutes()
        {
            return System.DateTime.Now.TimeOfDay.Minutes < 10 ? "0" + System.DateTime.Now.TimeOfDay.Minutes.ToString() : System.DateTime.Now.TimeOfDay.Minutes.ToString();
        }

        public static string getHours()
        {
            return System.DateTime.Now.TimeOfDay.Hours < 10 ? "0" + System.DateTime.Now.TimeOfDay.Hours.ToString() : System.DateTime.Now.TimeOfDay.Hours.ToString();
        }

        public static string getSeconds()
        {
            return System.DateTime.Now.TimeOfDay.Hours < 10 ? "0" + System.DateTime.Now.TimeOfDay.Hours.ToString() : System.DateTime.Now.TimeOfDay.Seconds.ToString();
        }

        public static string getDate()
        {
            return System.DateTime.Today.ToString("dd/MM/yyyy");
        }

        public static string getTime()
        {
            return getHours()+ ":" + getMinutes();
        }

        public static string getTimeExact()
        {
            return getHours() + ":" + getMinutes() + ":" + getSeconds();
        }
    }  
    
    public static class statusVenta
    {
        public static string ACTIVA = "ACTIVA";
        public static string CANCELADA = "CANCELADA";

        public static List<string> getallstatus()
        {
            return new List<string>() { ACTIVA, CANCELADA };
        }
    }  

    public static class estados
    {
        public static List<string> getestados()
        {
            return new List<string>() {"AGUASCALIENTES","BAJA CALIFORNIA","BAJA CALIFORNIA SUR","CAMPECHE","COAHUILA DE ZARAGOZA",
                                       "COLIMA","CHIAPAS","CHIHUAHUA","DISTRITO FEDERAL", "DURANGO","GUANAJUATO","GUERRERO","HIDALGO",
                                       "JALISCO","MICHOACAN DE OCAMPO","MORELOS","NAYARIT","NUEVO LEON","OAXACA","PUEBLA","QUERETARO",
                                       "QUINTANA ROO","SAN LUIS POTOSI","SINALOA","SONORA","TABASCO","TAMAULIPAS","TLAXCALA","VERACRUZ",
                                       "YUCATAN","ZACATECAS"};
        }
    }
}
