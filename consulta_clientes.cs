using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MundoMusical.XBASE;
using MundoMusical.DB;
using MundoMusical.CUSTOM_CONTROLS;
using MundoMusical.XML_SCHEMA_WRITER;
using MundoMusical.CONSULTAS.CLIENTES.CLIENTES_GENERAL;
using MundoMusical.CONSULTAS.CLIENTES.CLIENTES_VENTAS;

namespace MundoMusical.CONSULTAS.CLIENTES
{
    public partial class consulta_clientes : baseConsultas
    {
        private dbop db;

        private enum modegrid {CONSULTA_CLIENTES, CONSULTA_CLIENTES_VENTA};

        private string gridmode;

        private dgvtoxml_dataset schemacommit;

        private frmrptclientes_general reportegeneral;
        private frmrptclientes_ventas reporteclientesventas;

        public consulta_clientes(consultas_central central)
        {
            InitializeComponent();
            this.initialsets();

            this.db = new dbop();

            this.FormClosed += (a,b) => { central.Close(); };
        }

        private void initialsets()
        {           
            this.dtpfechanacimiento.Format = DateTimePickerFormat.Short;
            this.dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            behaviorDefinitions.txtOnlyNumbers(ref this.txtidcliente);
            behaviorDefinitions.txtOnlyNumbers(ref this.txtcp);
            behaviorDefinitions.txtOnlyNumbers(ref this.txttelefono);

            this.txtidcliente.GotFocus += (sender, args) => { this.loadall(); this.cleartxt(); };
            this.txtcp.GotFocus += (sender, args) => { this.loadall(); this.cleartxt(); };
            this.txtcurp.GotFocus += (sender, args) => { this.loadall(); this.cleartxt(); };
            this.txtnombreapellido.GotFocus += (sender, args) => { this.loadall(); this.cleartxt(); };
            this.txtrfc.GotFocus += (sender, args) => { this.loadall(); this.cleartxt(); };
            this.txttelefono.GotFocus += (sender, args) => { this.loadall(); this.cleartxt(); };

            this.txtidcliente.KeyUp += (sender, args) => { 

                if (this.txtidcliente.Text != string.Empty) {
                    
                    this.db.getclientesdgvconsultas(ref this.dgv, this.txtidcliente.Text.Trim(), "idcliente");
                }
                else
                {
                    this.loadall();
                }
            };

            this.txtnombreapellido.KeyUp += (sender, args) => {

                if(this.txtnombreapellido.Text != string.Empty)
                {
                    this.db.getclientesdgvconsultas(ref this.dgv, this.txtnombreapellido.Text, "nombreapellido");
                }
                else
                {
                    this.loadall();
                }

            };

            this.dtpfechanacimiento.ValueChanged += (sender, args) =>
            {
                this.db.getclientesdgvconsultas(ref this.dgv, this.dtpfechanacimiento.Text.Trim(), "fechanacimiento");
            };


            this.dtpfechanacimiento.KeyUp += (sender, args) =>
            {
                this.db.getclientesdgvconsultas(ref this.dgv, this.dtpfechanacimiento.Text.Trim(), "fechanacimiento");
            };

            this.txttelefono.KeyUp += (sender, args) =>
            {
                if(this.txttelefono.Text.Trim() == string.Empty)
                {
                    this.loadall();
                }
                else
                {
                    this.db.getclientesdgvconsultas(ref this.dgv, this.txttelefono.Text.Trim(), "telefono");
                }
            };

            this.txtcp.KeyUp += (sender, args) =>
            {
                if(this.txtcp.Text.Trim() == string.Empty)
                {
                    this.loadall();
                }
                else
                {
                    this.db.getclientesdgvconsultas(ref this.dgv, this.txtcp.Text.Trim(), "codigopostal");
                }
            };

            this.txtcurp.KeyUp += (sender, args) => {

                if(this.txtcurp.Text.Trim() == string.Empty)
                {
                    this.loadall();
                }
                else
                {
                    this.db.getclientesdgvconsultas(ref this.dgv, this.txtcurp.Text.Trim(), "curp");
                }
                
            };

            this.txtrfc.KeyUp += (sender, args) => {
                if(this.txtrfc.Text.Trim() == string.Empty)
                {
                    this.loadall();
                }
                else
                {
                    this.db.getclientesdgvconsultas(ref this.dgv, this.txtrfc.Text.Trim(), "rfc");
                }
            };

        }

        private void showventas()
        {
            if(this.dgv.RowCount == 0)
            {
                genericDefinitions.dangerInfo("No hay clientes en tabla");
            }
            else
            {
                this.gridmode = modegrid.CONSULTA_CLIENTES_VENTA.ToString();                
                
                string[] envio = new string[this.dgv.RowCount];

                for(int x = 0; x< this.dgv.RowCount; ++x)
                {
                    envio[x] = this.dgv.Rows[x].Cells[0].Value.ToString();
                }                
                this.db.getclientesventasdgvconsultas(ref this.dgv, envio);
            }
        }

        private void cleartxt()
        {
            this.txtcp.Text = "";
            this.txtcurp.Text = "";
            this.txtidcliente.Text = "";
            this.txtnombreapellido.Text = "";
            this.txtrfc.Text = "";
            this.txttelefono.Text = "";
        }
        private void consulta_clientes_Load(object sender, EventArgs e)
        {
            this.loadall();
        }

        private void loadall()
        {
            this.gridmode = modegrid.CONSULTA_CLIENTES.ToString();

            this.db.getclientesdgvconsultas(ref this.dgv);
        }

        private void btnverventas_Click(object sender, EventArgs e)
        {
            this.showventas();
        }

        private void btnverventas2_Click(object sender, EventArgs e)
        {
            this.showventas();
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            this.reporting();
        }

        private void reporting()
        {
            if(this.dgv.RowCount == 0)
            {
                genericDefinitions.dangerInfo("No hay registros para imprimir.");
                this.txtidcliente.Focus();
            }
            else
            {
                switch (this.gridmode)
                {
                    case ("CONSULTA_CLIENTES"):
                        {
                            this.schemacommit = new dgvtoxml_dataset(this.dgv, this.Name+"_"+this.gridmode);
                            this.schemacommit.create();                            
                            this.reportegeneral = new frmrptclientes_general(this.schemacommit.getdatatable());
                            this.reportegeneral.ShowDialog();
                            break;
                        }
                    case ("CONSULTA_CLIENTES_VENTA"):
                        {
                            this.schemacommit = new dgvtoxml_dataset(this.dgv, this.Name + "_" + this.gridmode);
                            this.schemacommit.create();
                            this.reporteclientesventas = new frmrptclientes_ventas(this.schemacommit.getdatatable());
                            this.reporteclientesventas.ShowDialog();                            
                            break;
                        }
                }
            }
            
        }
    }
}
