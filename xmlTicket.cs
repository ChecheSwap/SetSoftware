using MundoMusical.DB;
using MundoMusical.XML_SCHEMA_WRITER;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MundoMusical.TICKET.CRYSTAL_RPT_TICKET.XML_SCHEMA
{
    public partial class xmlTicket : Form
    {

        private int numeroventa = 6;
        private dbop db;
        private dgvtoxml_dataset xmlwritter;
        public xmlTicket()
        {
            InitializeComponent();
            this.db = new DB.dbop();
            this.xmlwritter = new dgvtoxml_dataset(this.db.getdetallesticketasTable(this.numeroventa), "TicketVenta");
        }

        private void xmlTicket_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {           
            this.xmlwritter.create();
        }
    }
}
