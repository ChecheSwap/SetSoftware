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
using MundoMusical.CONSULTAS.VENTAS;

namespace MundoMusical.VENTA
{
    public partial class seleccionarCliente :gridquerybase
    {
        private centralVenta source;
        private consulta_ventas baseventas;       
        public seleccionarCliente()
        {
            InitializeComponent();
            this.MinimizeBox = false;
            this.Text = "Seleccionar Cliente-Venta";
            this.db = new dbop();
            this.txtname.KeyUp += (sender, args) =>
            {
                this.getGrid();
            };            
        }

        public seleccionarCliente(consulta_ventas baseventas) // CONSTRUCTOR BASADO EN FORMA CONSULTAS VENTAS
        {
            InitializeComponent();
            this.MinimizeBox = false;
            this.Text = "Seleccionar Cliente";
            this.baseventas = baseventas;
            this.db = new dbop();

            this.txtname.KeyUp += (sender, args) =>
            {
                this.getGrid();
            };

            this.dgv.CellClick += (sender, args) =>
            {
                bool flag = false;
                if (args.RowIndex > -1 && args.ColumnIndex > -1)
                {
                    for(int x=0; x<this.baseventas.arrclientes.Length; ++x)
                    {
                        if(this.baseventas.arrclientes[x].id.ToString() == this.dgv.Rows[args.RowIndex].Cells[0].Value.ToString().Trim())
                        {                            
                            this.baseventas.cbidcliente.SelectedIndex = x;
                            flag = true;
                            break;
                        }
                    }

                    if (!flag)
                    {
                        genericDefinitions.error("Error al realizar busqueda Avanzada, contacte al desarrollador");
                    }
                    this.Close();
                }
            };
            this.dgv.Cursor = Cursors.Hand;
        }
        public seleccionarCliente(centralVenta source) //CONSTRUCTOR BASADO EN FORMA VENTAS CENTRAL
        {
            InitializeComponent();
            this.MinimizeBox = false;
            this.Text = "Seleccionar Cliente-Venta";
            this.source = source;
            this.db = new dbop();
            this.txtname.KeyUp += (sender, args) =>
            {
                this.getGrid();
            };
            this.dgv.CellClick += (sender, args) =>
            {
                if (args.RowIndex > -1 && args.ColumnIndex > -1)
                {
                    this.source.txtcliente.Text = this.dgv.Rows[args.RowIndex].Cells[1].Value + " " + this.dgv.Rows[args.RowIndex].Cells[2].Value;
                    this.source.txtid.Text = this.dgv.Rows[args.RowIndex].Cells[0].Value.ToString();
                    this.source.checkboxCP.Checked = false;                    
                    this.Close();
                }
            };
            this.dgv.Cursor = Cursors.Hand;            
        }        

        private void seleccionarCliente_Load(object sender, EventArgs e)
        {
            getGrid();
            this.ActiveControl = this.txtname;
        }
        private void getGrid()
        {
            this.db.getSeleccionarclienteventaDGV(ref this.dgv, this.txtname.Text.Trim());
            this.dgv.Columns[0].HeaderText = "Id";
            this.dgv.Columns[1].HeaderText = "Nombre";
            this.dgv.Columns[2].HeaderText = "Apellido";
            this.dgv.Columns[3].HeaderText = "Curp";
            this.dgv.Columns[4].HeaderText = "Rfc";
        }

    }
}
