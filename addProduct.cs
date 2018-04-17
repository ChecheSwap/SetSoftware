using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MundoMusical.CODE_BAR;
using MundoMusical.DB;
using MundoMusical.CUSTOM_CONTROLS;
using System.Globalization;
using MundoMusical.LABELS;

namespace MundoMusical.PRODUCT
{
    public partial class addProduct : XBASE.productBase
    {

        protected dbop db;
        protected cb cbar;
        protected producto prod;
        protected List<Control> controls;
        protected List<TextBox> tboxes;

        private printCbar cbprint;
        public addProduct()
        {
            InitializeComponent();
            this.Text = "Agregar Producto";
            this.cbar = new cb();
            this.db = new dbop();
            this.setAtribs();
        }
        public addProduct(midformProduct origen)
        {
            InitializeComponent();
            this.Text = "Agregar Producto";
            this.origen = origen;
            this.origen.addbtn.Enabled = false;
            this.FormClosed += (x, y) => { this.origen.addbtn.Enabled = true; };
            this.cbar = new cb();
            this.db = new dbop();
            this.setAtribs();
            this.stopBounds();
        }

        protected virtual void setAtribs()
        {            
            this.controls = new List<Control>() { this.txt1, this.txt3, this.cb1, this.txtcosto, this.cbganancia, this.txt2, this.txt4 };
            this.tboxes = new List<TextBox>() { this.txt1, this.txt2, this.txt3, this.txt4, this.txtcosto};

            behaviorDefinitions.txtOnlyNumbers(ref this.txt3);
            behaviorDefinitions.txtOnlyNumbers(ref this.txt4);

            behaviorDefinitions.txtPrice(this.txtcosto);
            behaviorDefinitions.txtPrice(this.txt2);

            controls.ElementAt(0).KeyPress += (x, y) => { if (y.KeyChar == (char)Keys.Enter) controls.ElementAt(1).Focus(); };
            controls.ElementAt(1).KeyPress += (x, y) => { if (y.KeyChar == (char)Keys.Enter) controls.ElementAt(2).Focus(); };
            controls.ElementAt(2).KeyPress += (x, y) => { if (y.KeyChar == (char)Keys.Enter) controls.ElementAt(3).Focus(); };
            controls.ElementAt(3).KeyPress += (x, y) => { if(y.KeyChar == (char)Keys.Enter)controls.ElementAt(4).Focus(); };
            controls.ElementAt(4).KeyPress += (x, y) => { if (y.KeyChar == (char)Keys.Enter) controls.ElementAt(5).Focus(); };
            controls.ElementAt(5).KeyPress += (x, y) => { if (y.KeyChar == (char)Keys.Enter) controls.ElementAt(6).Focus(); };

            this.txt4.KeyUp += (x, y) => { if (this.txt4.Text.Trim() != "") this.picbox.Image = this.cbar.getImage(this.txt4.Text.Trim()); };
            this.txt4.KeyPress += (x, y) => { if (y.KeyChar == (char)Keys.Enter && (this.txt4.Text.Trim() != "")) this.onAccept(); };
            this.txt4.KeyUp += (x, y) => { if(this.txt4.Text.Trim() == "") this.generateCBAR(); };

            this.baccept.Click += (x, y) =>
            {
                this.onAccept();
            };
            this.bcancel.Click += (x, y) =>
            {
                this.onCancel();
            };

            this.prod = new producto();
            
            for(int x=0; x<101; ++x)
            {
                this.cbganancia.Items.Add(x.ToString() + "%");
            }

            this.cbganancia.SelectedIndex = 0;
            this.cbganancia.SelectedIndexChanged += (sender, args) => {
                if(this.txtcosto.Text.Trim() != string.Empty)
                {
                    Double val = Double.Parse(this.txtcosto.Text, CultureInfo.InvariantCulture);
                    this.txt2.Text = (val + ((val / 100) * this.cbganancia.SelectedIndex)).ToString("F2");
                }
            };

            this.cbprint = new printCbar();
        }

        protected virtual void onAccept()
        {
            this.insertProduct();           
        }

        protected virtual void onCancel()
        {
            this.cleartxt();
            this.generateCBAR();
            this.txt1.Focus();
        }

        private void insertProduct()
        {
            string val = this.validatedata();

            switch (val)
            {
                case ("00"):
                    {
                        genericDefinitions.error("Codigo de producto invalido", "Codigo de barras error");                        
                        this.txt1.Focus();
                        break;
                    }
                case ("0"):
                    {
                        genericDefinitions.dangerInfo("Datos Invalidos, Llene todos los campos.", "Datos incorrectos");                        
                        this.txt1.Focus();
                        break;
                    }
                case ("1"):
                    {
                        
                        this.prod.idcategoria = db.getidcategoria(this.cb1.Text);
                        this.prod.nombre = this.txt1.Text;
                        this.prod.precio = double.Parse(this.txt2.Text.Trim(), CultureInfo.InvariantCulture);
                        this.prod.stock = int.Parse(this.txt3.Text);
                        this.prod.idproducto = int.Parse(this.txt4.Text);
                        this.prod.preciocosto = double.Parse(this.txtcosto.Text.Trim(), CultureInfo.InvariantCulture);
                        
                        if(this.prod.preciocosto > this.prod.precio)
                        {
                            genericDefinitions.dangerInfo("Precio de producto menor a costo del mismo.");
                            this.txt2.Focus();
                            return;
                        }

                        if (db.insertproduct(this.prod))
                        {
                            if (this.rbcodigo.Checked)
                            {
                                this.cbprint.setParams(this.prod.idproducto);
                                this.cbprint.print(this.prod.stock);
                            }

                            if (this.cbnotifica.Checked)
                            {
                                genericDefinitions.ok("Se ha dado de alta articulo", "Echo");
                            }                                                        
                            this.cleartxt();
                            this.generateCBAR();
                            this.txt1.Focus();
                        }
                        else
                        {
                            this.txt1.Focus();
                        }
                        break;
                    }
            }
        }
        protected void cleartxt()
        {
            foreach(Control c in this.controls)
            {
                c.Text = null;
            }
            this.cbganancia.SelectedIndex = 0;            
        }
        protected string validatedata()
        {
            foreach(Control c in this.controls)
            {
                if (c.Text == ""){                    
                    return "0";
                }
                
                if(c.Name == "txt4")
                {
                    if (!(this.db.isavailableIDproducto(int.Parse(this.txt4.Text.Trim()))))                        
                    return "00";
                }
            }

            return "1";
        }
        protected void fillcategoria()
        {
            List<categoria> loc = db.getCategorias();
            if (loc != null)
            {
                foreach (categoria c in loc)
                    this.cb1.Items.Add(c.nombre);
            }           
        }

        protected void generateCBAR()
        {
            this.txt4.Text = this.db.getcurrentidproductos().ToString();
            this.picbox.Image = cbar.getImage(this.txt4.Text);            
        }

        protected virtual void addProduct_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                this.fillcategoria();
                this.generateCBAR();
            }

            this.picbox.SizeMode = PictureBoxSizeMode.CenterImage;
            this.ActiveControl = this.txt1;


        }
        private void baccept_Click(object sender, EventArgs e)
        {}

        private void btncat_Click(object sender, EventArgs e)
        {
            this.cb1.Items.Clear();
            this.fillcategoria();
            this.cb1.Focus();
        }
    }
}
