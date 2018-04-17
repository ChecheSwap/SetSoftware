using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MundoMusical.CUSTOMER;
using MundoMusical.CUSTOM_CONTROLS;

namespace MundoMusical.PRODUCT
{
    public partial class productInternals : deleteInternals
    {

        protected DB.dbop db;
        private updateProduct centralupdate;       

        public productInternals(updateProduct updateprod)
        {
            InitializeComponent();
            this.Text = "Actualizar Producto";
            this.centralupdate = updateprod;
            this.db = new DB.dbop();
            this.ShowInTaskbar = false;
            behaviorDefinitions.txtOnlyNumbers(ref this.textBox1);            
        }

        public productInternals()
        {
            InitializeComponent();
            this.Text = "Actualizar Producto";            
            this.db = new DB.dbop();
            this.ShowInTaskbar = false;
            behaviorDefinitions.txtOnlyNumbers(ref this.textBox1);            

        }

        private void productInternals_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.textBox1;
        }

        protected override void onEnter()
        {
            this.existproduct();
        }

        protected void existproduct()
        {
            if (this.textBox1.Text.Trim() != "")
            {
                if (this.db.existproduct(int.Parse(this.textBox1.Text.Trim())))
                {
                    this.centralupdate.idproduct = int.Parse(this.textBox1.Text.Trim());                    
                    this.Close();
                    this.centralupdate.txt1.Focus();
                }
                else
                {
                    genericDefinitions.dangerInfo("No existe el producto con ID:" + this.textBox1.Text, "Advertencia");
                    this.textBox1.Text = "";
                }
            }    
        }


    }
}
