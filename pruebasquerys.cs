using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MundoMusical.CUSTOM_CONTROLS;
using MundoMusical.DB;
using MundoMusical.TICKET;
using MundoMusical.TRANSLATOR;
using MundoMusical.CUSTOMER;
using MundoMusical;
using MundoMusical.XML_SCHEMA_WRITER;
using MundoMusical.TICKET.CRYSTAL_RPT_TICKET;
using MundoMusical.CONFIGURACION_DE_IMPRESION;
using MundoMusical.LABELS;

namespace MundoMusical
{    
    public partial class pruebasquerys : Form
    {
        private dbop db;
        private printCbar cbs;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        extern static int SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        public pruebasquerys()
        {
            InitializeComponent();
            this.db = new dbop();         
        }

        public pruebasquerys(int x)
        {
            InitializeComponent();
            this.db = new dbop();
            this.Move += (sender, args) =>
            {
                if(this.Top < genericDefinitions.WND_TOP)
                {
                    this.Top = genericDefinitions.WND_TOP;
                }

                if (this.Left < genericDefinitions.WND_LEFT)
                {
                    this.Left = genericDefinitions.WND_LEFT;
                }

                if(this.Right > genericDefinitions.WND_RIGHT)
                {
                    this.Left = genericDefinitions.WND_RIGHT - this.Width;
                }

                if(this.Bottom > genericDefinitions.WND_BOTTOM)
                {
                    this.Top = genericDefinitions.WND_BOTTOM - Height;
                }

            };

            this.tb1.KeyUp += (a, b) => { if (b.KeyCode == Keys.F12) genericDefinitions.ok("ok"); };
        }
       
        private void pruebasquerys_Load(object sender, EventArgs e)
        {
            this.cbs = new printCbar();
            this.cbs.setParams(100);
            this.cbs.print();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            pruebasquerys xf = new pruebasquerys(1);
            SetParent(xf.Handle, this.Handle);
            xf.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            printTicket pt = new printTicket(this.db.getventa(16),this.db.getTienda());
            pt.setparams();
            //pt.print();
            
            frmrptticket ver = new frmrptticket(pt.getreporte());
            ver.ShowDialog();
        }
    }
}
