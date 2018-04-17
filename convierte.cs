using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundoMusical.TRANSLATOR
{
    public class convierte
    {
        private StringBuilder stb;
        private string numeral;
        private string aux;
        private int xlength;

        public string getstring(double number)
        {
            if (this.numeral != "")
            {
                this.numeral = "";
            }

            if (number > 1000000)
            {
                return number.ToString();
            }

            this.stb = new StringBuilder(number.ToString("F2").Replace(",", "."));
            this.xlength = this.stb.Length;

            switch (this.xlength)
            {
                case (4):
                    {
                        this.funidad();
                        this.numeral += "Pesos ";
                        this.fcentavos();
                        break;
                    }
                case (5):
                    {
                        if (this.fdecena())
                        {
                            this.funidad();
                        }
                        this.numeral += "Pesos ";
                        this.fcentavos();
                        break;
                    }
                case (6):
                    {
                        if (this.fcentena())
                        {
                            if (this.fdecena())
                            {
                                this.funidad();
                            }
                        }
                        this.numeral += "Pesos ";
                        this.fcentavos();
                        break;
                    }
                case (7):
                    {
                        this.funidad();
                        this.numeral += "Mil ";
                        if (this.fcentena())
                        {
                            if (this.fdecena())
                            {
                                this.funidad();
                            }
                        }
                        this.numeral += "Pesos ";
                        this.fcentavos();
                        break;
                    }
                case (8):
                    {
                        if (this.fdecena())
                        {
                            this.funidad();
                        }
                        this.numeral += "Mil ";

                        if (this.fcentena())
                        {
                            if (this.fdecena())
                            {
                                this.funidad();
                            }
                        }
                        this.numeral += "Pesos ";
                        this.fcentavos();
                        break;
                    }
                case (9):
                    {
                        if (this.fcentena())
                        {
                            if (this.fdecena())
                            {
                                this.funidad();
                            }
                        }
                        this.numeral += "Mil ";

                        if (this.fcentena())
                        {
                            if (this.fdecena())
                            {
                                this.funidad();
                            }
                        }
                        this.numeral += "Pesos ";
                        this.fcentavos();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return this.numeral;
        }

        private void fcentavos()
        {            
            this.stb = new StringBuilder(this.stb.ToString().Replace(".", ""));            

            if (stb[0] == '0' && stb[1] == '0')
            {
                return;
            }

            this.numeral += "Con ";

            this.aux = propios(stb[0].ToString() + stb[1].ToString());

            if (!(this.aux==null))
            {
                this.numeral += this.aux + " Centavos ";
                return;
            }

            if (!(stb[0] == '0'))
            {
                this.numeral += decena(stb[0]);
            }

            if (!(stb[1] == '0') && !(stb[0]=='0'))
            {
                this.numeral +=" y " + unidad(stb[stb.Length - 1]);
            }
            else if(!(stb[1] == '0'))
            {
                this.numeral +=unidad(stb[stb.Length - 1]);
            }
                
            this.numeral += " Centavos ";
        }
        private void funidad()
        {
            if ((stb[0] != '0'))
            {
                this.numeral += unidad(stb[0])+" ";
            }
            stb[0] = 'X';
            this.stb = new StringBuilder(this.stb.ToString().Replace("X", ""));           
        }

        private bool fdecena()
        {
            this.aux = propios(this.stb[0].ToString() + this.stb[1].ToString());            

            if (!(this.aux == null))
            {
                this.numeral += this.aux + " ";
                this.stb[0] = 'X';
                this.stb[1] = 'X';
                this.stb = new StringBuilder(this.stb.ToString().Replace("X", ""));
                return false;
            }

            if (!(stb[0] == '0'))
            {
                this.numeral += decena(stb[0])+" ";
                stb[0] = 'X';
                this.stb = new StringBuilder(this.stb.ToString().Replace("X", ""));

                if (this.stb[0] != '0')
                {
                    this.numeral += "y ";
                }

                return true;
            }

            stb[0] = 'X';
            this.stb = new StringBuilder(this.stb.ToString().Replace("X", ""));
            return true;
        }

        private bool fcentena()
        {
            this.aux = propios(stb[0].ToString() + stb[1].ToString() + stb[2].ToString());

            if(!(this.aux == null))
            {
                this.numeral += this.aux + " ";

                stb[0] = 'X';
                stb[1] = 'X';
                stb[2] = 'X';

                this.stb = new StringBuilder(this.stb.ToString().Replace("X", ""));

                return false;
            }

            if (!(stb[0] == '0'))
            {                
                this.numeral += centena(stb[0]) + " ";
            }
            stb[0] = 'X';
            this.stb = new StringBuilder(this.stb.ToString().Replace("X", ""));
            return true;
        }       

        private static string unidad(char n)
        {
            switch (n)
            {
                case '1':
                    return "Un";
                case '2':
                    return "Dos";
                case '3':
                    return "Tres";
                case '4':
                    return "Cuatro";
                case '5':
                    return "Cinco";
                case '6':
                    return "Seis";
                case '7':
                    return "Siete";
                case '8':
                    return "Ocho";
                case '9':
                    return "Nueve";
            }

            return null;
        }

        private static string decena(char n)
        {
            switch (n)
            {
                case '1':
                    return "Diez";
                case '2':
                    return "Veinte";
                case '3':
                    return "Treinta";
                case '4':
                    return "Cuarenta";
                case '5':
                    return "Cincuenta";
                case '6':
                    return "Sesenta";
                case '7':
                    return "Setenta";
                case '8':
                    return "Ochenta";
                case '9':
                    return "Noventa";
            }

            return null;
        }

        private static string centena(char n)
        {
            switch (n)
            {
                case '1':
                    return "Ciento";
                case '2':
                    return "Doscientos";
                case '3':
                    return "Trescientos";
                case '4':
                    return "Cuatrocientos";
                case '5':
                    return "Quinientos";
                case '6':
                    return "Seiscientos";
                case '7':
                    return "Setecientos";
                case '8':
                    return "Ochocientos";
                case '9':
                    return "Novecientos";
            }

            return null;
        }

        private static string propios(string numero)
        {
            switch (numero)
            {
                case ("11"):
                    return "Once";
                case ("12"):
                    return "Doce";
                case ("13"):
                    return "Trece";
                case ("14"):
                    return "Catorce";
                case ("15"):
                    return "Quince";
                case ("16"):
                    return "Dieciseis";
                case ("17"):
                    return "Diecisiete";
                case ("18"):
                    return "Dieciocho";
                case ("19"):
                    return "Diecinueve";
                case ("21"):
                    return "Veintiuno";
                case ("22"):
                    return "Veintidos";
                case ("23"):
                    return "Veintitres";
                case ("24"):
                    return "Veinticuatro";
                case ("25"):
                    return "Veinticinco";
                case ("26"):
                    return "Veintiseis";
                case ("27"):
                    return "Veintisiete";
                case ("28"):
                    return "Veintiocho";
                case ("29"):
                    return "Veintinueve";
                case ("100"):
                    return "Cien";
                default:
                    return null;
            }
        }
    }    
}
