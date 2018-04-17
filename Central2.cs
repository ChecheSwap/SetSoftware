using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MundoMusical.LOGIN;
using MundoMusical;
using MundoMusical.ATOM;
using MundoMusical.INVENTARIO;
using MundoMusical.PRODUCT;
using MundoMusical.CUSTOMER;
using MundoMusical.CATEGORY;
using MundoMusical.PAYMODE;
using MundoMusical.VENTA;
using MundoMusical.DB;
using MundoMusical.CONFIGURACION_DE_IMPRESION;
using System.Runtime.InteropServices;
using MundoMusical.TIENDA;
using MundoMusical.DB.DUMPS;
using MundoMusical.CONSULTAS;
using MundoMusical.LABELS;

namespace MundoMusical
{
    public partial class Central2 : BaseForm
    {
        [BrowsableAttribute(false)]
        private MdiClient myMDI;
        central2OnLoad info;

        public midformProduct product;
        
        public midformCustomer customer;

        public addCategory addcat;
        public updateCategory updcat;
        public deleteCategory delcat;
        public seecategory seecat;

        public addPayMode addpay;
        public deleteModopago delpay;
        public updatePaymode updpay;
        public seepaymodes seepay;
        public consultas_central consultas;

        public centralVenta xventa;

        private string val0, val1;

        public usuario usr;
        public login sesion;

        public printerticketdefault impresoras;

        public inventarioBase inventario;
        
        private dbop DB;

        public centraltienda tienda;

        public exportdb dboperations;

        public cancelarVenta xcancelarventa;

        public printcbGui etiquetas;

        private const int SB_BOTH = 3;
        private const int WM_NCCALCSIZE = 0x83;

        [DllImport("user32.dll")]

        private static extern int ShowScrollBar(IntPtr hWnd, int wBar, int bShow);

        protected override void WndProc(ref Message m)
        {
            if (this.myMDI != null)
            {
                ShowScrollBar(this.myMDI.Handle, SB_BOTH, 0 );//SCROLL BARS OCULTAS                
            }
            base.WndProc(ref m);
        }

        public Central2(string alias)
        {
            InitializeComponent();
            this.DB = new dbop();
            this.MaximizeBox = true;
            this.Text = "SET SOFTWARE";            
            this.Shown += this.showINFO;
            this.FormClosing += onClose;
            this.DoubleBuffered = true;
            this.FormBorderStyle = FormBorderStyle.None;            
            this.usr = this.DB.usrget(alias);

            this.sesion = new login()
            {
                idusuario = this.usr.id_usuario,
                hora_inicio = TimeSpan.Parse(genericDefinitions.getTimeExact()),
                fecha_inicio = DateTime.Parse(genericDefinitions.getDate()),                
            };

            genericDefinitions.globalsesion = this.sesion;
            
            this.lblnombreusuario.Text = (this.usr.nombre +" "+ this.usr.apellido).ToUpper();
            this.lblhorainicio.Text = this.sesion.hora_inicio.ToString();

            this.btnusuario.MouseHover += (sender, atgs) =>
            {
                this.panelSesion.Visible = true;
            };

            this.btnusuario.MouseLeave += (sender, atgs) =>
            {
                this.panelSesion.Visible = false;
            };

            this.product = null;            
            this.customer = null;
            this.addcat = null;
            this.updcat = null;
            this.delcat = null;
            this.addpay = null;
            this.delpay = null;
            this.updpay = null;
            this.seepay = null;
            this.xventa = null;                               
        }

        /*protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.Refresh();
        }*/

        private void showINFO(object sender, EventArgs args)
        {
            this.info.ShowDialog();
        }
        private void Central2_Load(object sender, EventArgs e)
        {
            this.info = new central2OnLoad("Bienvenido");
            this.WindowState = FormWindowState.Maximized;          

            foreach (Control ctl in this.Controls)
            {
                try
                {
                    myMDI = (MdiClient)ctl;
                    myMDI.BackgroundImage = global::MundoMusical.Properties.Resources.wf;                   
                }
                catch (InvalidCastException ex)
                {
                    //NOTHING
                }
            }

            this.DB = new dbop();
            this.setBounds();
            genericDefinitions.WND_BOTTOM -=(this.panel1.Height+this.menuStrip1.Height);
            
            if (!this.DB.insertlogin(ref this.sesion)) {
                genericDefinitions.dangerInfo("Problema al registrar inicio de sesion");
            };            
        }
        
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        { }

        private void button1_Click(object sender, EventArgs e)
        {
            this.openmidcustomer();
        }


        private void openmidcustomer()
        {
            if (this.customer == null)
            {
                this.customer = new CUSTOMER.midformCustomer(this) { MdiParent = this };
                this.customer.Show();
            }
            else if(this.customer.WindowState == FormWindowState.Minimized)
            {                
                this.customer.WindowState = FormWindowState.Normal;                
            }
            else
            {
                this.customer.WindowState = FormWindowState.Minimized;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.showmidprod();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {}

        /*
        private void formResize(object sender, EventArgs e) IMAGEN TOP, EVENTO ONRESIZE
        {
            centralPictureBox.Left = (this.Width - pictureBox1.Width) / 2;
            centralPictureBox.Top = (this.Height - pictureBox1.Height) / 2;
        }*/

        private void button3_Click(object sender, EventArgs e)
        {
            this.openxventa();
        }

        /*
        private void centerBackgroundPicture(object x, EventArgs arg) CENTRAR IMAGEN TOP
        {
            this.centralPictureBox.Left = (this.Width - this.centralPictureBox.Width) / 2;
            this.centralPictureBox.Top = (this.Height - this.centralPictureBox.Height) / 2 -50;
        }

        private void deactivateBackg()
        {
            this.centralPictureBox.Visible = false;
        }
        */
        public void openxventa()
        {
            if (this.xventa == null)
            {
                this.xventa = new centralVenta(this,ref this.usr) { MdiParent = this };
                this.xventa.Show();
            }
            else if (this.xventa.WindowState == FormWindowState.Normal)
            {
                this.xventa.WindowState = FormWindowState.Minimized;
            }
            else
            {
                this.xventa.WindowState = FormWindowState.Normal;
            }

        }
        private void informacionDelSistemaToolStripMenuItem_Click(object sender, EventArgs e){}

        private void panel1_Paint(object sender, PaintEventArgs e) {}

        private void opcionesToolStripMenuItem_Click(object sender, EventArgs e){}

        private void btnInventario_Click(object sender, EventArgs e)
        {
            if (this.inventario == null)
            {
                this.inventario = new inventarioBase(this) { MdiParent = this };
                this.inventario.Show();
            }
            else if(this.inventario.WindowState == FormWindowState.Normal)
            {
                this.inventario.WindowState = FormWindowState.Minimized;
            }
            else
            {
                this.inventario.WindowState = FormWindowState.Normal;
            }

        }

        private void button1_Click_1(object sender, EventArgs e){ }

        private void button2_Click_1(object sender, EventArgs e){ }

        private void button2_Click_2(object sender, EventArgs e)
        {}

        private void button1_Click_2(object sender, EventArgs e)
        {}

        private void informacionDelSistemaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.info = new central2OnLoad("Información del sistema");
            this.info.ShowDialog();
        }

        private void onesec_Tick(object sender, EventArgs e)
        {
            this.val0 = genericDefinitions.getHours();

            this.val1 = genericDefinitions.getMinutes();

            this.lbldate.Text = genericDefinitions.getDate();

            this.lbltime.Text = String.Format("{0:00}:{1:00}", val0, val1);
            
        }

        private void opcionesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {}

        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void agregarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.openaddcat();

        }

        private void openaddcat()
        {
            if (this.addcat == null)
            {
                this.addcat = new addCategory(this) { MdiParent = this };
                this.addcat.Show();
            }
            else 
            {
                this.addcat.WindowState = FormWindowState.Normal;
            }

        }

        private void openupdcat()
        {
            if (this.updcat == null)
            {
                this.updcat = new updateCategory(this) { MdiParent = this };
                this.updcat.Show();
                this.updcat.Location = new Point(this.updcat.Location.X, this.updcat.Location.Y - 180);

            }
            else 
            {
                this.updcat.WindowState = FormWindowState.Normal;
            }

        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.openupdcat();
        }

        private void opendelcat()
        {
            if (this.delcat == null)
            {
                this.delcat = new deleteCategory(this) { MdiParent = this };
                this.delcat.Show();
                this.delcat.Location = new Point(this.delcat.Location.X, this.delcat.Location.Y - 180);
            }
            else 
            {
                this.delcat.WindowState = FormWindowState.Normal;
            }

        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.opendelcat();
        }

        private void openseecat()
        {
            if (this.seecat == null)
            {
                this.seecat = new seecategory(this) { MdiParent = this };
                this.seecat.Show();
            }
            else 
            {
                this.seecat.WindowState = FormWindowState.Normal;
            }

        }

        private void verCategoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.openseecat();          
        }

        private void button2_Click_3(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            this.Close();
        }

        private void openEtiquetas()
        {
            if (this.etiquetas == null)
            {
                this.etiquetas = new printcbGui(this) { MdiParent = this, TopLevel = false };
                this.etiquetas.Show();
            }
            else if (this.etiquetas.WindowState == FormWindowState.Minimized)
            {
                this.etiquetas.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.etiquetas.WindowState = FormWindowState.Minimized;
            }
        }
        private void openaddpay()
        {
            if(this.addpay == null)
            {
                this.addpay = new addPayMode(this) { MdiParent = this };
                this.addpay.Show();
            }
            else 
            {
                this.addpay.WindowState = FormWindowState.Normal;
            }

        }
        private void openupdpay()
        {
            
            if (this.updpay == null)
            {
                this.updpay = new updatePaymode(this) { MdiParent = this };
                this.updpay.Show();
            }
            else 
            {                
                this.updpay.WindowState = FormWindowState.Normal;
            }
        }

        private void opendelpay()
        {
           
            if (this.delpay == null)
            {
                this.delpay = new deleteModopago(this) { MdiParent = this };
                this.delpay.Show();
            }
            else 
            {                
                this.delpay.WindowState = FormWindowState.Normal;
            }

        }

        private void openseepay(){
            if (this.seepay == null)
            {
                this.seepay = new seepaymodes(this) { MdiParent = this };
                this.seepay.Show();
            }
            else 
            {
                this.seepay.WindowState = FormWindowState.Normal;
            }
        }

        private void showmidprod()
        {
            if (this.product == null)
            {
                this.product = new midformProduct(this) { MdiParent = this };
                this.product.Show();
            }
            else if(this.product.WindowState == FormWindowState.Minimized)
            {
                this.product.WindowState = FormWindowState.Normal;
            }
            else
            {
                this.product.WindowState = FormWindowState.Minimized;
            }
        }
        private void agregarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.openaddpay();
        }

        private void modificarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.openupdpay();
        }

        private void eliminarToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.opendelpay();
        }

        private void mercanciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.showmidprod();
        }

        private void verModosDePagoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.openseepay();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.info = new central2OnLoad("Información del sistema");
            this.info.ShowDialog();
        }

        private void configuracionDelSistemaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void impresoraDeEtiquetasPredeterminadaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(this.impresoras == null)
            {
                this.impresoras = new printerticketdefault(this) { MdiParent = this };
                this.impresoras.Show();
            }
            else
            {
                this.impresoras.WindowState = FormWindowState.Normal;
            }

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btntienda_Click(object sender, EventArgs e)
        {
            this.tiendainteraction();
        }

        private void ventaDeMostradorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.openxventa();
        }

        private void tiendainteraction()
        {
            if (this.tienda == null)
            {
                this.tienda = new centraltienda(this) { MdiParent = this };
                this.tienda.Show();
            }
            else if (this.tienda.WindowState == FormWindowState.Normal)
            {
                this.tienda.WindowState = FormWindowState.Minimized;
            }
            else
            {
                this.tienda.WindowState = FormWindowState.Normal;
            }
        }

        private void btnexportdb_Click(object sender, EventArgs e)
        {
            this.opendboperations();
        }

        private void opendboperations()
        {
            if(this.dboperations == null)
            {
                this.dboperations = new exportdb(this) { MdiParent = this };
                this.dboperations.Show();
            }
            else if(this.dboperations.WindowState == FormWindowState.Normal)
            {
                this.dboperations.WindowState = FormWindowState.Minimized;
            }
            else
            {
                this.dboperations.WindowState = FormWindowState.Normal;
            }
        }

        private void btnConsultas_Click(object sender, EventArgs e)
        {
            this.openconsultas();
        }

        private void cancelarVentaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.opencancelarventa();
        }

        private void opencancelarventa()
        {
            if (this.xcancelarventa == null)
            {
                this.xcancelarventa = new cancelarVenta(this) { MdiParent = this };
                this.xcancelarventa.Show();
            }
            else
            {
                this.xcancelarventa.WindowState = FormWindowState.Normal;
            }
        }

        private void btnetiqueta_Click(object sender, EventArgs e)
        {
            this.openEtiquetas();
        }

        private void openconsultas()
        {
            if(this.consultas == null)
            {
                this.consultas = new consultas_central(this) {TopLevel = false, MdiParent = this};
                this.consultas.Show();               
            }
            else if(this.consultas.WindowState == FormWindowState.Normal)
            {
                this.consultas.WindowState = FormWindowState.Minimized;
            }
            else
            {
                this.consultas.WindowState = FormWindowState.Normal;
            }
        }

    }
}
