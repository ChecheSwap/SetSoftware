using CrystalDecisions.Shared;
using MundoMusical.CODE_BAR;
using MundoMusical.DB;
using MundoMusical.IMAGE_OPERATIONS;
using MundoMusical.TRANSLATOR;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Printing;
using System.Data;
using MundoMusical.CONFIGURACION_DE_IMPRESION;

namespace MundoMusical.TICKET.CRYSTAL_RPT_TICKET
{
    public class printTicket
    {
       
        private tienda xtienda;
        private venta xventa;
        private cliente xcliente;
        private dbop db;
        private bool flag = false;
        private rptticket reporte;
        private convierte translate;
        private cb codbar;

        private string pathlogo;
        private string pathcbar;

        private int widthcbar = 14;
        private int heightcbar = 1;

        private Bitmap logotienda;
        private Bitmap logocodebar;

        private ParameterFields baseparams;
        private ParameterField parametro;
        private ParameterDiscreteValue parametroval;
        public printTicket(venta xventa, tienda xtienda)
        {
            this.xtienda = xtienda;
            this.xventa = xventa;
            this.db = new dbop();
            this.reporte = new rptticket();
            this.translate = new convierte();
            this.codbar = new cb();

            this.xcliente = this.db.getclientAsobject(this.xventa.idcliente);

            this.pathcbar = Path.Combine(genericDefinitions.TICKETLOGOSPATH + @"\logocbar.png");
            this.pathlogo = Path.Combine(genericDefinitions.TICKETLOGOSPATH + @"\logotienda.png");

            this.widthcbar = (int)(widthcbar * 50 / 2.54);
            this.heightcbar = (int)(heightcbar * 50 / 2.54);
        }

        public void print()
        {
            try
            {
                if (printerControl.printIsSafe(this.db.getprinterticketname()))
                {
                    this.reporte.PrintOptions.DissociatePageSizeAndPrinterPaperSize = true;
                    this.reporte.PrintToPrinter(1, true, 0, 0);
                }
                else
                {
                    using (printWarning pw = new printWarning())
                    {
                        pw.ShowDialog();
                    }
                }

                this.reporte.Close();
                this.reporte.Dispose();
            }
            catch(Exception ex)
            {
                genericDefinitions.error(ex.ToString());
            }
        }
        
        private bool deleteFile(string path)
        {
            bool flag = false;
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                flag = true;
            }
            catch(Exception ex)
            {
                genericDefinitions.error(ex.ToString());
            }

            return flag;
        }
        private bool commitlogos()
        {
            bool flag = true;

            if (!Directory.Exists(genericDefinitions.TICKETLOGOSPATH))
            {
                Directory.CreateDirectory(genericDefinitions.TICKETLOGOSPATH);
            }

            this.logotienda = new Bitmap(imageOperations.getImageFromBytes(this.db.getTienda().logotienda));

            if (this.deleteFile(this.pathlogo))
            {
                logotienda.Save(this.pathlogo);
            }
            else
            {
                flag = false;
            }
            
            this.logocodebar = this.codbar.getonlycodabar(this.xventa.idventa.ToString(), this.widthcbar, this.heightcbar);

            if (this.deleteFile(this.pathcbar))
            {
                this.logocodebar.Save(this.pathcbar);
            }
            else
            {
                flag = false;
            }
          
            return flag;
                
        }
        public bool setparams()
        {
            this.flag = false;            
            try
            {
                /*
                DataTable dt = this.db.getdetallesticketasTable(this.xventa.idventa);

                for (int x=0; x<1000; ++x)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = "9999";
                    dr[1] = "OK";
                    dr[2] = "OK";
                    dr[3] = "OK";

                    dt.Rows.Add(dr);
                }
                
                this.reporte.SetDataSource(dt);

                */

                this.reporte.SetDataSource(this.db.getdetallesticketasTable(this.xventa.idventa));
                                            
                this.reporte.SetParameterValue("nombretienda",this.xtienda.nombre);
                this.reporte.SetParameterValue("rfc", "R.F.C. "+this.xtienda.rfc.ToUpper());
                this.reporte.SetParameterValue("callenumero", this.xtienda.calle.ToUpper() + " #" +this.xtienda.numero.ToString());
                this.reporte.SetParameterValue("colonia", "COL."+this.xtienda.colonia.ToUpper());
                this.reporte.SetParameterValue("ciudadestado", (this.xtienda.ciudad + ", "+this.xtienda.estado).ToUpper());
                this.reporte.SetParameterValue("telefono", "TEL. "+this.xtienda.telefono);
                this.reporte.SetParameterValue("fecha", this.xventa.fecha.ToShortDateString());                
                this.reporte.SetParameterValue("cliente", this.xcliente.nombre + " " +this.xcliente.apellido);
                this.reporte.SetParameterValue("hora", this.xventa.hora.ToString());
                this.reporte.SetParameterValue("ticket", this.db.getticketid(this.xventa.idventa).ToString());
                this.reporte.SetParameterValue("totalletra", "**"+this.translate.getstring(this.xventa.total).Trim()+"**");
                this.reporte.SetParameterValue("total", "$" + this.xventa.total.ToString());
                this.reporte.SetParameterValue("iva", "$" + (this.xventa.iva).ToString());
                this.reporte.SetParameterValue("subtotal", "$" + this.xventa.subtotal.ToString());
                this.reporte.SetParameterValue("descuento", "$" + this.xventa.descuentoextra.ToString());
                
                if (this.commitlogos())
                {
                    this.reporte.SetParameterValue("urlLogo", this.pathlogo);
                    this.reporte.SetParameterValue("urlcodigobarras", this.pathcbar);
                }   
                                                 
                this.reporte.PrintOptions.PrinterName = this.db.getprinterticketname();

                flag = true;
            }
            catch(Exception ex)
            {
                genericDefinitions.error(ex.ToString());
            }

            return this.flag;
        }

        public rptticket getreporte()
        {
            return this.reporte;
        }

        public ParameterFields getparamsList()
        {
               this.baseparams = new ParameterFields();

               this.parametro = new ParameterField();
               this.parametroval = new ParameterDiscreteValue();
               parametro.Name = "nombretienda";
               parametroval.Value = this.xtienda.nombre;
               this.parametro.CurrentValues.Add(parametroval);
               this.baseparams.Add(parametro);

               this.parametro = new ParameterField();
               this.parametroval = new ParameterDiscreteValue();
               parametro.Name = "rfc";
               parametroval.Value = "RFC:" + this.xtienda.rfc.ToUpper();
               this.parametro.CurrentValues.Add(parametroval);
               this.baseparams.Add(parametro);

               this.parametro = new ParameterField();
               this.parametroval = new ParameterDiscreteValue();
               parametro.Name = "callenumero";
               parametroval.Value = this.xtienda.calle.ToUpper();
               this.parametro.CurrentValues.Add(parametroval);
               this.baseparams.Add(parametro);

               this.parametro = new ParameterField();
               this.parametroval = new ParameterDiscreteValue();
               parametro.Name = "colonia";
               parametroval.Value = "COL. " + this.xtienda.colonia.ToUpper();
               this.parametro.CurrentValues.Add(parametroval);
               this.baseparams.Add(parametro);

               this.parametro = new ParameterField();
               this.parametroval = new ParameterDiscreteValue();
               parametro.Name = "ciudadestado";
               parametroval.Value = (this.xtienda.ciudad + ", " + this.xtienda.estado).ToUpper();
               this.parametro.CurrentValues.Add(parametroval);
               this.baseparams.Add(parametro);

               this.parametro = new ParameterField();
               this.parametroval = new ParameterDiscreteValue();
               parametro.Name = "telefono";
               parametroval.Value = this.xtienda.telefono;
               this.parametro.CurrentValues.Add(parametroval);
               this.baseparams.Add(parametro);

               this.parametro = new ParameterField();
               this.parametroval = new ParameterDiscreteValue();
               parametro.Name = "fecha";
               parametroval.Value = this.xventa.fecha.ToShortDateString();
               this.parametro.CurrentValues.Add(parametroval);
               this.baseparams.Add(parametro);

               this.parametro = new ParameterField();
               this.parametroval = new ParameterDiscreteValue();
               parametro.Name = "cliente";
               parametroval.Value = this.xcliente.nombre + " " + this.xcliente.apellido;
               this.parametro.CurrentValues.Add(parametroval);
               this.baseparams.Add(parametro);

               this.parametro = new ParameterField();
               this.parametroval = new ParameterDiscreteValue();
               parametro.Name = "hora";
               parametroval.Value = this.xventa.hora.ToString();
               this.parametro.CurrentValues.Add(parametroval);
               this.baseparams.Add(parametro);

               this.parametro = new ParameterField();
               this.parametroval = new ParameterDiscreteValue();
               parametro.Name = "ticket";
               parametroval.Value = this.db.getticketid(this.xventa.idventa).ToString();
               this.parametro.CurrentValues.Add(parametroval);
               this.baseparams.Add(parametro);

               this.parametro = new ParameterField();
               this.parametroval = new ParameterDiscreteValue();
               parametro.Name = "totalletra";
               parametroval.Value = this.translate.getstring(this.xventa.total);
               this.parametro.CurrentValues.Add(parametroval);
               this.baseparams.Add(parametro);

               this.parametro = new ParameterField();
               this.parametroval = new ParameterDiscreteValue();
               parametro.Name = "total";
               parametroval.Value ="$"+this.xventa.total.ToString();
               this.parametro.CurrentValues.Add(parametroval);
               this.baseparams.Add(parametro);

               this.parametro = new ParameterField();
               this.parametroval = new ParameterDiscreteValue();
               parametro.Name = "iva";
               parametroval.Value = "$"+(this.xventa.subtotal / 100 * 16).ToString();
               this.parametro.CurrentValues.Add(parametroval);
               this.baseparams.Add(parametro);

               this.parametro = new ParameterField();
               this.parametroval = new ParameterDiscreteValue();
               parametro.Name = "subtotal";
               parametroval.Value = "$" + this.xventa.subtotal.ToString();
               this.parametro.CurrentValues.Add(parametroval);
               this.baseparams.Add(parametro);

               this.parametro = new ParameterField();
               this.parametroval = new ParameterDiscreteValue();
               parametro.Name = "descuento";
               parametroval.Value = "$" + this.xventa.descuentoextra.ToString();
               this.parametro.CurrentValues.Add(parametroval);
               this.baseparams.Add(parametro);

               if (this.commitlogos())
               {
                   this.parametro = new ParameterField();
                   this.parametroval = new ParameterDiscreteValue();
                   parametro.Name = "urlLogo";
                   parametroval.Value = this.pathlogo;
                   this.parametro.CurrentValues.Add(parametroval);
                   this.baseparams.Add(parametro);

                   this.parametro = new ParameterField();
                   this.parametroval = new ParameterDiscreteValue();
                   parametro.Name = "urlcodigobarras";
                   parametroval.Value = this.pathcbar;
                   this.parametro.CurrentValues.Add(parametroval);
                   this.baseparams.Add(parametro);
               }
               
            return this.baseparams;
        }
    }
}
