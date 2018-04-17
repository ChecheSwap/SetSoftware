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
    public partial class delete : add
    {
        deleteInternals myObjFIRST;
        public delete()
        {
            InitializeComponent();                        
            this.Text = "Eliminar Cliente";
            this.Shown += this.onShow;
            this.idCliente = -1;
        }


        public delete(midformCustomer origen)
        {
            InitializeComponent();
            this.midform = origen;
            this.midform.deletebtn.Enabled = false;
            this.FormClosed += (sender, args) => { this.midform.deletebtn.Enabled = true; };
            this.Text = "Eliminar Cliente";
            this.Shown += this.onShow;
            this.idCliente = -1;
            this.stopBounds();
        }

        private void onShow(object sender, EventArgs args)
        {
            this.initcommit();
        }

        private void initcommit()
        {
            genericDefinitions.dangerInfo("Al eliminar un cliente es posible que se pierdan registros y datos importantes del sistema: contacte al desarrollador del sistema para obtener ayuda.", "Aviso");
            this.myObjFIRST = new deleteInternals(this);
            while (true)
            {
                this.myObjFIRST.ShowDialog();
                break;
            }
            if (this.idCliente != -1)
                this.getInitialDataDEL();
            else
                this.Close();
        }

        private void delete_Load(object sender, EventArgs e)
        {           
            this.initializeT();
            this.disablecontrols();            
        }

        protected override void onAccept()
        {            
                this.deleteclient();           
        }

        private void deleteclient()
        {
            if (!this.db.isinVentas(this.idCliente))
            {
                if (genericDefinitions.yesno("Desea Eliminar El usuario con id: " + this.idCliente + "?", "Validar"))
                    if (this.db.deleteClient(this.idCliente))
                    {
                        this.clearTxt();
                        this.idCliente = -1;
                        this.initcommit();
                    }
                        
            }
            else
                genericDefinitions.error("No se puede eliminar cliente, tiene un historial de compra!", "Error");

        }

        private void bcancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
