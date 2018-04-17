using MundoMusical.CODE_BAR;
using MundoMusical.CONFIGURACION_DE_IMPRESION;
using MundoMusical.DB;
using MundoMusical.LABELS.TEMPLATES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundoMusical.LABELS
{
    public class printCbar
    {
        private string id;
        private cb codabar;
        private labelBase25_35 reporte;
        private bool flag = false;
        private dbop db;
        public printCbar()
        {
            this.codabar = new cb();
            this.db = new dbop();
        }


        public void setParams(int id)
        {
            this.flag = false;
            this.reporte = new labelBase25_35();

            if (!this.codabar.saveImage(this.codabar.getImage(id.ToString(),1280,1280,false), "tmp", genericDefinitions.CODEBARSPATH)){
                genericDefinitions.error("Error de interoperabilidad con el Fichero");
                return;
            }

            try
            {
                this.reporte.SetParameterValue("imageSource", genericDefinitions.CODEBARSPATH + "\\tmp.jpeg");
                this.reporte.SetParameterValue("id", id.ToString());
            }
            catch(Exception ex)
            {
                genericDefinitions.error(ex.ToString());
                return;
            }

            this.flag = true;
        }

        public void print()
        {
            if (this.flag)
            {
                try
                {

                    if (printerControl.printIsSafe(this.db.getprinteretiquetaname()))
                    {
                        this.reporte.PrintOptions.PrinterName = this.db.getprinteretiquetaname();

                        this.reporte.PrintToPrinter(1, true, 0, 0);
                    }
                    else
                    {
                        using (printWarning pw = new printWarning())
                        {
                            pw.ShowDialog();
                        }
                    }
                }
                catch (Exception ex)
                {
                    genericDefinitions.error(ex.ToString());
                }
            }
            else
            {
                genericDefinitions.error("No se puede proceder a realizar la impresión, consulte al desarrollador");
            }


            this.reporte.Close();

            this.reporte.Dispose();
        }

        public void print(int veces)
        {
            if (veces > 0)
            {
                if (this.flag)
                {
                    try
                    {

                        if (printerControl.printIsSafe(this.db.getprinteretiquetaname()))
                        {
                            this.reporte.PrintOptions.PrinterName = this.db.getprinteretiquetaname();

                            this.reporte.PrintToPrinter(veces, true, 0, 0);
                        }
                        else
                        {
                            using (printWarning pw = new printWarning())
                            {
                                pw.ShowDialog();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        genericDefinitions.error(ex.ToString());
                    }
                }
                else
                {
                    genericDefinitions.error("No se puede proceder a realizar la impresión, consulte al desarrollador");
                }
            }
            else
            {
                genericDefinitions.error("Numero de copias Invalido.");
            }


            this.reporte.Close();

            this.reporte.Dispose();
        }



    }
}
