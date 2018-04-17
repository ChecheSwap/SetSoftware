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
using MundoMusical.INVENTARIO;
using MundoMusical.XML_SCHEMA_WRITER;
using MundoMusical.PRODUCT.SEE_PRODUCTS_INTERNAL;
using System.Threading;
using MundoMusical.CONSULTAS;
using MundoMusical.VENTA;
using MundoMusical.LABELS;

namespace MundoMusical.PRODUCT
{
    public partial class seeproductsinternal : BaseForm
    {
        private dbop db;
        private List<categoria> lcat;
        private dgvtoxml_dataset schemadgv;
        private rptseeproductsinternal reporte;
        private centralVenta venta;
        private printcbGui cbprint;
        private bool isforventa = false;
        private bool isforprintcb = false;
        public seeproductsinternal()
        {
            InitializeComponent();
            this.initialconfigs();           
        }

        public seeproductsinternal(inventarioBase IB)
        {
            InitializeComponent();
            this.initialconfigs();
            this.FormClosed += (sender, args) => { IB.Close(); };                    
        }

        public seeproductsinternal(consultas_central CC)
        {
            InitializeComponent();
            this.initialconfigs();
            this.FormClosed += (sender, args) => { CC.Close(); };            
        }

        public seeproductsinternal(centralVenta venta)
        {
            InitializeComponent();
            this.venta = venta;
            this.isforventa = true;

            this.initialconfigs();            
        }

        public seeproductsinternal(printcbGui cb)
        {
            InitializeComponent();
            this.cbprint = cb;
            this.isforprintcb = true;

            this.initialconfigs();
        }
        private void initialconfigs()
        {
            if ((!this.isforventa)&&(!this.isforprintcb))
            {
                this.FormBorderStyle = FormBorderStyle.None;                
            }
            else if(this.isforventa)
            {
                this.MaximizeBox = true;
                this.btnprint.Visible = false;
                this.MinimizeBox = false;

                this.dgv.CellClick += (sender, args) => {

                    if (args.RowIndex > -1)
                    {
                        this.venta.txtidproducto.Text = this.dgv.Rows[args.RowIndex].Cells[0].Value.ToString();
                        this.Close();
                    }
                };

                this.Text = "Seleccion de Producto";
                this.tp1.Text = ">>Productos en Inventario";                
            }
            else if(this.isforprintcb){

                this.MaximizeBox = true;
                this.btnprint.Visible = false;
                this.MinimizeBox = false;

                this.dgv.CellClick += (sender, args) => {

                    if (args.RowIndex > -1)
                    {
                        this.cbprint.rbindividual.Checked = true;
                        this.cbprint.txtcodigo.Text = this.dgv.Rows[args.RowIndex].Cells[0].Value.ToString();
                        this.Close();
                    }
                };

                this.Text = "Seleccion de codigo de Producto";
                this.tp1.Text = ">>Productos en Inventario";

            }


            this.SizeChanged += (sender, args) => { this.centercruds(); };

            behaviorDefinitions.txtOnlyNumbers(ref this.txtid);

            this.db = new dbop();

            this.lcat = this.db.getCategorias();


            if (this.lcat != null)
            {
                foreach (categoria cat in this.lcat)
                {
                    this.cbcat.Items.Add(cat.nombre);
                };
            }

            this.db.getallproductosDGV(ref this.dgv);
            this.dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.txtname.GotFocus += (sender, args) => {
                this.txtid.Text = "";
                this.cbcat.SelectedIndex = -1;
                this.db.getallproductosDGV(ref this.dgv);
            };

            this.txtname.KeyUp += (sender, args) =>
            {
                if(!(this.txtname.Text.Trim() == string.Empty))
                {
                    this.db.getallproductosDGV(this.dgv,this.txtname.Text.Trim(),"nombre");
                }
                else
                {
                    this.db.getallproductosDGV(ref this.dgv);
                }
            };
            this.txtid.GotFocus += (a, b) => {
                this.txtname.Text = "";
                this.cbcat.SelectedIndex = -1;
                this.db.getallproductosDGV(ref this.dgv);
            };

            this.txtid.KeyUp += (sender, args) =>
            {
                if (!(this.txtid.Text.Trim() == string.Empty))
                {
                    this.db.getallproductosDGV(this.dgv, this.txtid.Text.Trim(), "id");
                }
                else
                {
                    this.db.getallproductosDGV(ref this.dgv);
                }
            };            
            this.cbcat.SelectedIndexChanged += (sender, args) =>
            {
                this.db.getallproductosDGV(this.dgv,this.cbcat.Text.Trim(),"categoria");
            };
            this.cbcat.GotFocus += (sender, args) =>
            {
                this.txtid.Text = "";
                this.txtname.Text = "";
                this.db.getallproductosDGV(ref this.dgv);
            };
        }
        private void seeproducts_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.txtname;

            if (this.isforventa || this.isforprintcb)
            {
                genericDefinitions.ok("Para seleccionar producto haga click en el registro que desee en la tabla de busqueda.");
            }
            
        }

        private void centercruds()
        {
            this.pbox.Location = new Point(this.tp1.Width / 2 - this.pbox.Width / 2, this.pbox.Location.Y);
            this.gb1.Location = new Point(this.tp1.Width / 2 - this.gb1.Width/2, this.gb1.Location.Y);
            this.btnprint.Location = new Point(this.gb1.Location.X + this.gb1.Width - this.btnprint.Width, this.btnprint.Location.Y);
        }

        private void btnprint_Click(object sender, EventArgs e)
        {
            this.schemadgv = new dgvtoxml_dataset(this.dgv, this.Name);
            this.schemadgv.create();
            this.reporte = new rptseeproductsinternal(this.schemadgv.getdatatable());
            this.reporte.ShowDialog();           
        }
    }
}
