using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MundoMusical.CUSTOMER
{
    public partial class customerUpdate :add
    {

        public customerUpdate()
        {
            InitializeComponent();
            this.Text = "Actualizar Datos de Cliente";
        }

        public customerUpdate(midformCustomer origen)
        {
            InitializeComponent();
            this.midform = origen;
            this.midform.updatebtn.Enabled = false;
            this.FormClosed += (sender, args) => { this.midform.updatebtn.Enabled = true; };
            this.Shown += this.onshow;
            this.Text = "Actualizar Datos de Cliente";
            this.idCliente = -1;
            this.stopBounds();
        }

        private void customerUpdate_Load(object sender, EventArgs e)
        {            
           this.initializeT();                           
        }

        private void onshow(object sender, EventArgs args)
        {
            this.selfshow();
        }

        private void selfshow()
        {
            deleteInternals local = new deleteInternals(this);
            local.ShowDialog();
            if (this.idCliente == -1)
                this.Close();
            else
            {
                this.getInitialDataUPD();
            }
        }

        protected override void onAccept()
        {
            this.update();
        }

        private void update()
        {
            if (this.validateData())
            {
                this.fillArray(ref this.dataTxt);
                if (this.db.updateClient(this.dataTxt, rfc, curp))
                {
                    this.clearTxt();
                    this.idCliente = -1;
                    this.selfshow();
                }
                else
                    this.txt1.Focus();
                
            }
            
            
        }
        private void bcancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
