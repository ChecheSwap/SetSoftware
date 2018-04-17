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

namespace MundoMusical.CUSTOMER
{
    public partial class deleteInternals : BaseForm {

        private delete delete;        
        private customerUpdate update;
        private DB.dbop db;
        
        public deleteInternals(delete delete) 
        {
            InitializeComponent();
            this.MinimizeBox = false;
            this.Text = "Ingrese Curp";
            this.delete = delete;                      
            this.textBox1.MaxLength = 18;
            this.textBox1.KeyPress += (x,y)=> { this.textBox1.Text = this.textBox1.Text.ToUpper(); this.textBox1.Select(this.textBox1.TextLength, 0);};
            this.textBox1.KeyPress += (x, y) => { if (y.KeyChar == (char)Keys.Enter) this.deletesearch(); };
            this.db = new DB.dbop();
            this.ShowInTaskbar = false;
        }

        public deleteInternals(customerUpdate cu) 
        {
            InitializeComponent();
            this.MinimizeBox = false;
            this.Text = "Ingrese Curp";
            this.update = cu;            
            this.textBox1.MaxLength = 18;
            this.textBox1.KeyPress += (x, y) => { this.textBox1.Text = this.textBox1.Text.ToUpper(); this.textBox1.Select(this.textBox1.TextLength, 0); };
            this.textBox1.KeyPress += (x, y) => { if (y.KeyChar == (char)Keys.Enter) this.onupdatesearch(); };
            this.db = new DB.dbop();
            this.ShowInTaskbar = false;
        }


        public deleteInternals()
        {
            InitializeComponent();
            this.MinimizeBox = false;
            this.Text = "Ingrese Curp";
            this.delete = null;            
            this.textBox1.KeyPress += (x, y) => {this.textBox1.Text = this.textBox1.Text.ToUpper(); this.textBox1.Select(this.textBox1.TextLength, 0);};
            this.textBox1.KeyPress += (x, y) => { if (y.KeyChar == (char)Keys.Enter) this.onEnter(); };
            this.textBox1.MaxLength = 18;
            this.db = new DB.dbop();
            this.ShowInTaskbar = false;
        }

        private void deleteInternals_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.textBox1;
        }

        private void busquedaToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        protected virtual void onEnter()
        {
            this.deletesearch();
        }
        private void deletesearch()
        {
            if (this.db.existcurp(this.textBox1.Text.Trim()))
            {
                this.delete.idCliente = (this.db.getIdcliente(this.textBox1.Text.Trim()));

                this.delete.txt1.Focus();

                this.Close();
            }
            else
                genericDefinitions.error("CURP No existenste en el sistema", "Error");
        }

        private void onupdatesearch()
        {
            if (this.db.existcurp(this.textBox1.Text.Trim())){
                this.update.idCliente = this.db.getIdcliente(this.textBox1.Text.Trim());                
                this.Close();
                this.update.txt1.Focus();
            }
            else
                genericDefinitions.error("No existe curp en el sistema","Error");
                
        }
    }
}
