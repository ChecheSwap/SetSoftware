using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MundoMusical.DB;

namespace MundoMusical.CONFIGURACION_DE_IMPRESION
{
    public partial class printerticketdefault :BaseForm
    {
        private dbop db;
        private PrintDialog pdialog;
        private string result;

        public printerticketdefault()
        {
            InitializeComponent();
            this.db = new dbop();
        }
        public printerticketdefault(Central2 central) 
        {
            InitializeComponent();
            this.db = new dbop();
            this.FormClosed += (sender, args) =>
            {
                central.impresoras = null;
            };
            this.stopBounds();
        }

        private void printerticketdefault_Load(object sender, EventArgs e)
        {
            this.Text = "Configuración de Impresoras";
            this.initcharge();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.result = this.showprintdialog();

            if(!(result == null))
            {
                this.db.setprinterticketname(this.result);
            }            

            this.initcharge();
        }

        private void initcharge()
        {
            this.ticket.Text = this.db.getprinterticketname();

            if(this.ticket.Text == "")
            {
                this.ticket.Text = "Sin Asignar";
            }

            this.etiqueta.Text = this.db.getprinteretiquetaname();

            if(this.etiqueta.Text == "")
            {
                this.etiqueta.Text = "Sin Asignar";
            }

            this.general.Text = this.db.getprinterglobalname();

            if(this.general.Text == "")
            {
                this.general.Text = "Sin Asignar";
            }
        }

        private string showprintdialog()
        {
            this.pdialog = new PrintDialog();

            if (pdialog.ShowDialog() == DialogResult.OK)
            {
                return this.pdialog.PrinterSettings.PrinterName;
            }

            return null;
        }

        private void btnetiqueta_Click(object sender, EventArgs e)
        {
            this.result = this.showprintdialog();

            if(!(this.result == null))
            {
                this.db.setprinteretiquetaname(this.result);
            } 
            
            this.initcharge();
        }

        private void btngeneral_Click(object sender, EventArgs e)
        {
            this.result = this.showprintdialog();
            if(!(this.result == null))
            {
                this.db.setprinterglobalname(this.result);
            }            
            this.initcharge();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            genericDefinitions.ok("Set Software esta optimizado para realizar impresiones en impresoras de tickets de 80mm de ancho.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            genericDefinitions.ok("Set Software esta optimizado para realizar impresiones de etiquetas con dimensiones de (37mm x 25mm).");
        }
    }
}
