using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using MundoMusical.DB;
using MundoMusical.CODE_BAR;
using System.Windows.Forms;
using MundoMusical.TRANSLATOR;
using System.Globalization;

namespace MundoMusical.TICKET
{
    // inches = pixels / dpi

    //centimeters/2.54 = pixels / dpi

    //pixels = (cm*dpi)/2.54

    public class globalTicket
    {
        private const float inchesCM = .4F;
        private const float cbarwidth = 10;//CM -> 4CM APROX
        private const float cbarheight = 1;//CM -> .5CM

        //DATOS DE PROCESAMIENTO
        private dbop db;
        private tienda xtienda;
        private venta xventa;
        private cliente xcliente;
        private cb codebar;
        private Form xform;
        private convierte xconvierte;

        //DATOS FISICOS DE DOCUMENTO
        private PrintDocument PD { get; set; }
        private PaperSize PS { get; set; }
        private Graphics g { get; set; }
        private Font myfont { get; set; }
        private SolidBrush brush { get; set; }
        private SizeF fDimensions { get; set;}

        //ATRIBUTOS DE IMPRESION
        int marginLeft { get; set; }
        float vertical { get; set; }
        float widthDoc { get; set; }
        float heightDoc { get; set; }
        private int maxLong { get; set; }
        
        private float tabizq1;
        private float tabizq2;
        private float tabder2;
        private float tabder1;
        private float tabsubtotal;
        private float tabiva;
        private float tabtotal;
        private float tabdescuento;
        private float productotablength;
        
        //DATOS DE IMPRESION
        public string nombretienda { get; set; }
        public string rfc { get; set; }
        public string direccion { get; set; }
        public string colonia { get; set; }
        public string ciudad { get; set; }
        public string telefono { get; set; }
        public string fecha { get; set; }
        public string numticket { get; set; }
        public string clientename { get; set; }
        public string linea { get; set; }
        public string cantidad { get; set; }
        public string producto { get; set; }
        public string preciounit { get; set; }
        public string importe { get; set; }
        public string subtotal { get; set; }
        public string total { get; set; }
        public string iva { get; set; }
        public string descuento { get; set; }
        public string precioletra { get; set; }
        public string leyenda { get; set; }
        private string subtotalletra { get; set; }
        private string totalletra { get; set; }
        private string ivaletra { get; set; }
        private string descuentoletra { get; set; }
        private string xtotalletra { get; set; }

        private Bitmap codebarobj;

        //private Bitmap logotienda;

        private List<detallesTicket> detalles;

        public globalTicket(List<detallesTicket> dt, venta xventa)
        {            
            this.xventa = xventa;
            this.detalles = dt;
            this.codebar = new cb();
            this.xform = new Form();
            this.db = new dbop();
            this.xcliente = this.db.getclientAsobject(this.xventa.idcliente);

            this.xtienda = this.db.getTienda();            

            this.myfont = new Font("Lucida Console", 7, FontStyle.Regular);               

            this.brush = new SolidBrush(Color.Black);
            this.marginLeft = 5;
            this.vertical = 0;
            this.widthDoc = inchesCM * 100 * 8; // AUTOMATIZAR PARA FORMATOS
            this.heightDoc = 0;
            this.setAttribs(dt.Count);

            this.nombretienda = this.xtienda.nombre.ToUpper();
            this.rfc = "RFC "+this.xtienda.rfc;
            this.direccion = this.xtienda.calle.ToUpper()+" #"+this.xtienda.numero.ToString().ToUpper();
            this.colonia = "COL. "+this.xtienda.colonia.ToUpper();
            this.ciudad = (this.xtienda.ciudad+","+this.xtienda.estado).ToUpper();
            this.telefono = "TEL. "+this.xtienda.telefono.ToUpper();
            this.fecha = ">>FECHA: "+ this.xventa.fecha.ToString("dd/MM/yyyy",CultureInfo.InvariantCulture) + " >>HORA: " +this.xventa.hora.ToString();
            this.numticket = ">>TICKET #: "+this.db.getticketid(this.xventa.idventa).ToString();
            this.clientename = ">>CLIENTE: " + this.xcliente.nombre.ToUpper();
            this.subtotalletra = "SUBTOTAL $";
            this.subtotal = this.xventa.subtotal.ToString("F1").Replace(",", ".");
            this.totalletra = "TOTAL $";
            this.total = this.xventa.total.ToString("F2").Replace(",", ".");
            this.descuentoletra = "DESCUENTO $";
            this.descuento = this.xventa.descuentoextra.ToString("F1").Replace(",", ".");

            this.ivaletra = "IVA $";
            this.iva = (this.xventa.subtotal / 100 * 16).ToString("F1").Replace(",",".");

            this.precioletra = "PROXIMAMENTE";
            this.leyenda = "¡Gracias por su compra!";

            this.cantidad = "Cant.";
            this.producto = "Producto"; 
            this.preciounit = "P.Unit";
            this.importe = "Importe";

            this.xconvierte = new convierte();
            this.xtotalletra = this.xconvierte.getstring(this.xventa.total).ToUpper() + "MN.";
            
        }
        public void createTicket(Object sender, PrintPageEventArgs e)
        {
            this.g = e.Graphics;
              
            this.fDimensions = g.MeasureString("j", this.myfont);

            this.tabizq1 = this.marginLeft;
            this.tabizq2 = this.marginLeft + this.g.MeasureString(this.cantidad, this.myfont).Width+this.fDimensions.Width;//OBTENGO EL TABULADOR PARA COLOCAR DESCRIPCION            
            this.tabder1 = this.widthDoc - this.g.MeasureString(this.importe, this.myfont).Width - this.marginLeft - this.fDimensions.Width;//OBTENGO TABULADOR PARA COLOCAR IMPORTE
            this.tabder2 = this.tabder1 - this.g.MeasureString(this.preciounit, this.myfont).Width- this.fDimensions.Width;//OBTENGO TABULADOR PARA PRECIO UNITARIO        


            this.tabdescuento = this.tabder1 - this.g.MeasureString(this.descuentoletra, this.myfont).Width;
            this.tabsubtotal = this.tabder1 - this.g.MeasureString(this.subtotalletra, this.myfont).Width;
            this.tabiva = this.tabder1 - this.g.MeasureString(this.ivaletra, this.myfont).Width;
            this.tabtotal = this.tabder1 - this.g.MeasureString(this.totalletra, this.myfont).Width;

            this.productotablength = (tabder2 - tabizq2-this.g.MeasureString(" ",this.myfont).Width);//MIDO EL LARGO DE EL ESPACIO DESCRIPCION           

            g.DrawRectangle(Pens.Black, 0, 0, widthDoc, this.calcHeight(this.detalles.Count));

            this.maxLong = maxLengthstring(this.g, myfont, widthDoc - marginLeft*2); //MAXIMO DE CARACTERES EN STRING  
            
            vertical += this.fDimensions.Height * 2; //2+

            g.DrawString(this.adapt(this.nombretienda), myfont, brush, this.getcenterDoc(this.nombretienda), vertical);

            vertical += this.fDimensions.Height; //1+

            g.DrawString(this.adapt(this.rfc), myfont, brush, this.getcenterDoc(this.rfc), vertical);

            vertical += this.fDimensions.Height; //1+

            g.DrawString(this.adapt(this.direccion), myfont, brush, this.getcenterDoc(this.direccion), vertical);

            vertical += this.fDimensions.Height; //1+

            g.DrawString(this.adapt(this.colonia), myfont, brush, this.getcenterDoc(this.colonia), vertical);

            vertical += this.fDimensions.Height; //1+

            g.DrawString(this.adapt(this.ciudad), myfont, brush, this.getcenterDoc(this.ciudad), vertical);

            vertical += this.fDimensions.Height; //1+

            g.DrawString(this.adapt(this.telefono), myfont, brush, this.getcenterDoc(this.telefono), vertical);

            vertical += this.fDimensions.Height*2;//1+

            g.DrawString(this.adapt(this.fecha), myfont, brush, this.marginLeft, vertical);

            vertical += this.fDimensions.Height;//2+

            g.DrawString(this.adapt(this.numticket), myfont, brush, this.marginLeft, vertical);

            vertical += this.fDimensions.Height;//1+

            g.DrawString(this.adapt(this.clientename), myfont, brush, this.marginLeft, vertical);

            vertical += this.fDimensions.Height;//1+

            g.DrawLine(Pens.Black, this.marginLeft, vertical, this.widthDoc-this.marginLeft, vertical);

            vertical += this.fDimensions.Height/2;//.5+

            g.DrawString(this.cantidad, this.myfont, brush, marginLeft, vertical);
            g.DrawString(this.producto, this.myfont, brush, tabizq2, vertical);
            g.DrawString(this.preciounit, this.myfont, brush, tabder2, vertical);
            g.DrawString(this.importe, this.myfont, brush, tabder1, vertical);

            vertical += this.fDimensions.Height;//1+            

            g.DrawLine(Pens.Black, this.marginLeft, vertical, this.widthDoc - this.marginLeft, vertical);

            vertical += this.fDimensions.Height;//1+

            foreach (detallesTicket detalle in this.detalles)
            {                
                g.DrawString(this.adapt(detalle.cantidad.ToString(), this.cantidad.Length+1), this.myfont, brush, this.getcenterDoc(this.adapt(detalle.cantidad.ToString()), this.tabizq1, tabizq2-this.fDimensions.Width), vertical);
                g.DrawString(this.adapt(detalle.descripcion, this.maxLengthstring(this.g,this.myfont,this.productotablength)), this.myfont, brush, this.tabizq2, vertical);
                g.DrawString(this.adapt(detalle.precio.ToString("F1").Replace(",", "."), this.preciounit.Trim().Length+1,'*'), this.myfont, brush, tabder2, vertical);
                g.DrawString(this.adapt(detalle.total.ToString("F1").Replace(",", "."), this.importe.Trim().Length+1,'*'), this.myfont, brush, tabder1, vertical);
                vertical += this.fDimensions.Height;
            }

            g.DrawLine(Pens.Black, this.tabder1, vertical, this.widthDoc - this.marginLeft, vertical);
            vertical += this.fDimensions.Height / 2;//.5

            g.DrawString(this.subtotalletra, this.myfont, this.brush, this.tabsubtotal, vertical);
            g.DrawString(this.subtotal, this.myfont, this.brush, this.tabder1, vertical);

            vertical += this.fDimensions.Height;//1+

            g.DrawString(this.descuentoletra, this.myfont, this.brush, this.tabdescuento, vertical);
            g.DrawString(this.descuento, this.myfont, this.brush, this.tabder1, vertical);
            vertical += this.fDimensions.Height;//1+

            g.DrawString(this.ivaletra, this.myfont, this.brush, this.tabiva, vertical);
            g.DrawString(this.iva, this.myfont, this.brush, this.tabder1, vertical);
            vertical += this.fDimensions.Height;//1+

            g.DrawLine(Pens.Black, this.tabder1, vertical, this.widthDoc - this.marginLeft, vertical);
            vertical += this.fDimensions.Height / 2;//.5+

            g.DrawString(this.totalletra, this.myfont, this.brush, this.tabtotal, vertical);
            g.DrawString(this.total, this.myfont, this.brush, this.tabder1, vertical);
            vertical += this.fDimensions.Height*2;//2+

            string driver = null;

            while (this.xtotalletra.Length >= this.maxLong)
            {
                driver = this.xtotalletra.Substring(0, maxLong);
                this.xtotalletra = this.xtotalletra.Substring(maxLong, this.xtotalletra.Length - maxLong);
                this.g.DrawString(driver, this.myfont, this.brush, this.getcenterDoc(driver), this.vertical);
                vertical += this.fDimensions.Height;
            }

            if (this.xtotalletra != "")
            {
                this.g.DrawString(this.xtotalletra, this.myfont, this.brush, this.getcenterDoc(this.xtotalletra), vertical);
                vertical += this.fDimensions.Height;//+1
            }

            vertical += this.fDimensions.Height;

            g.DrawString(this.adapt(this.leyenda), this.myfont, this.brush, this.getcenterDoc(this.leyenda), vertical);
            vertical += this.fDimensions.Height;//1+                        

            //IMPRIMIR CODIGO DE BARRAS
            this.codebarobj = this.codebar.getonlycodabar(this.xventa.idventa.ToString(), (int)((cbarwidth *50) / 2.54), (int)((cbarheight * 50) / 2.54));
                        
            g.DrawImage(this.codebarobj, this.getcenterDoc(this.codebarobj.PhysicalDimension.Width), vertical);
            
            g.Dispose();
                
        }

        private void setAttribs(int ats)
        {
            this.PD = new PrintDocument();
            this.PD.PrinterSettings.PrinterName = this.db.getprinterticketname();
            this.PD.PrintController = new StandardPrintController();
            this.PD.DefaultPageSettings.Margins.Left = 0;
            this.PD.DefaultPageSettings.Margins.Right = 0;
            this.PD.DefaultPageSettings.Margins.Top = 0;
            this.PD.DefaultPageSettings.Margins.Bottom = 0;
            this.PD.DefaultPageSettings.PaperSize = new PaperSize("NewPS", (int)this.widthDoc, (int)this.calcHeight(ats));

            this.PD.PrintPage += (sender, e) =>
            {                
                this.createTicket(sender, e);
            };
        }
        
        public void print()
        {
            try
            {
                this.PD.Print();
            }
            catch (Exception ex)
            {
                genericDefinitions.error(ex.ToString(),"Error de impresion");
            }
        }

        private float calcHeight(int items)
        {
            items += 24; //2 LINEAS BOTTOM + ATRIBUTOS

            items += 3; //SECCION DE PRECIO LETRA

            return this.fDimensions.Height * items;
        }

        private float getWidthstring(string word)//CALCULO ANCHO DE STRING EN DIMENSIONES DE IMPRESION
        {
            return this.g.MeasureString(word, this.myfont).Width;
        }

        private float getcenterDoc(string word,float init, float last)//init cordenada x0 y last cordenada x1 en absisa
        {
            return (last - init) / 2 - this.g.MeasureString(word, this.myfont).Width / 2;
        }
        private float getcenterDoc(string word)
        {
            return this.widthDoc / 2 - this.getWidthstring(word) / 2;
        }

        private float getcenterDoc(float length)
        {
            return this.widthDoc / 2 - length / 2;
        }
        private string adapt(string source)
        {
            if (source.Length > this.maxLong)
            {
                source = source.Substring(0, this.maxLong);
                StringBuilder sb = new StringBuilder(source);
                sb[this.maxLong - 1] = '.';
                source = sb.ToString();
            }
            return source;
        }

        private string adapt(string source,int max)
        {
            if (source.Length > max)
            {
                source = source.Substring(0, max);
                StringBuilder sb = new StringBuilder(source);
                sb[max - 1] = '.';
                source = sb.ToString();
            }
            return source;
        }

        private string adapt(string source, int max,char ad)
        {
            if (source.Length > max)
            {
                source = source.Substring(0, max);
                StringBuilder sb = new StringBuilder(source);
                sb[max - 1] = ad;
                source = sb.ToString();
            }
            return source;
        }

        private int maxLengthstring(Graphics gr, Font font, float width)
        {
            string s = "";
            SizeF size;
            int max = 0;

            while (true)
            {                
                size = gr.MeasureString(s, font);                

                if (size.Width >= width)
                {
                    break;
                }
                s += "x";
                ++max;
            }
            return max;
        }             
    }
}
