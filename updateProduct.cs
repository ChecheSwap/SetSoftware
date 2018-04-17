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
using System.Globalization;

namespace MundoMusical.PRODUCT
{
    public partial class updateProduct :addProduct
    {
        public int idproduct = -1;
                
        public updateProduct()
        {
            InitializeComponent();                                        
            this.Shown += (sender,args)=> { this.onshown(); };
            this.txt4.Enabled = false;
            this.Text = "Actualizar Producto";            
        }
        public updateProduct(midformProduct origen)
        {
            InitializeComponent();
            this.txt4.Enabled = false;
            this.Shown += (sender, args) => { this.onshown(); };
            this.Text = "Actualizar Producto";
            this.label3.Text = "Inventario:";
            this.label3.Location = new Point(25,txt3.Location.Y);
            this.stopBounds();
        }

        protected virtual void onshown()
        {
            if (!DesignMode)
            {
                productInternals pdi = new productInternals(this);
                pdi.ShowDialog();

                if (this.idproduct == -1)
                {
                    this.Close();
                }
                else
                {
                    this.getdata();
                    this.txt1.Focus();
                }
            }
        }

        private void updateProduct_Load(object sender, EventArgs e)
        {
            this.rbcodigo.Visible = false;
        }

        protected override void addProduct_Load(object sender, EventArgs e)
        {}
        protected void getdata()
        {
            this.prod = this.db.getdataproduct(this.idproduct);

            this.txt1.Text = this.prod.nombre;            
            this.txt3.Text = this.prod.stock.ToString();
            this.txt4.Text = this.prod.idproducto.ToString();
            this.txtcosto.Text = this.prod.preciocosto.ToString();            

            if (this.prod.precio != this.prod.preciocosto)
            {
                int res = (int)((((this.prod.precio - this.prod.preciocosto )* 100) / this.prod.preciocosto));
                
                if(res > 0 && res < 101)
                {
                    this.cbganancia.SelectedIndex = res;
                }
                
            }

            this.txt2.Text = this.prod.precio.ToString();
            this.picbox.Image = cbar.getImage(this.txt4.Text);
            this.fillcategoria();
            this.cb1.Text = this.db.getcategorianame(this.prod.idcategoria);            
        }

        protected override void onCancel()
        {            
            this.idproduct = -1;
            this.picbox.Image = null;
            this.cleartxt();
            this.cb1.Items.Clear();  
            this.onshown();                   
        }

        protected override void onAccept()
        {            
            this.update();
        }

        private void update()
        {
            string val = this.validatedata();

            switch (val)
            {
                case ("0"):
                    {
                        genericDefinitions.error("Datos invalidos, verifique campos.", "Integridad de datos");
                        this.txt1.Focus();
                        break;
                    }
                case ("00"):

                case ("1"):
                    {
                        this.prod = new producto
                        {
                            idcategoria = db.getidcategoria(this.cb1.Text),
                            nombre = this.txt1.Text,
                            precio = Double.Parse(this.txt2.Text.Trim(), CultureInfo.InvariantCulture),
                            stock = int.Parse(this.txt3.Text),
                            idproducto = int.Parse(this.txt4.Text),
                            preciocosto = Double.Parse(this.txtcosto.Text.Trim(), CultureInfo.InvariantCulture)
                        };
                        
                        if(this.prod.preciocosto > this.prod.precio)
                        {
                            genericDefinitions.dangerInfo("Costo del producto es mayor a precio de venta.");
                            this.txt2.Focus();
                            return;
                        }
                        
                        if (db.updateproducto(this.prod))
                        {
                            if(this.cbnotifica.Checked)
                            {                                
                                genericDefinitions.ok("Se ha actualizado el producto.", "Echo");
                            }                                                        
                        }

                        this.onCancel();
                        break;
                    }
            }
        }

        protected override void setAtribs()
        {
            this.controls = new List<Control>() { this.txt1, this.txt3, this.cb1, this.txtcosto, this.cbganancia, this.txt2, this.txt4 };
            this.tboxes = new List<TextBox>() { this.txt1, this.txt2, this.txt3, this.txt4, this.txtcosto };

            behaviorDefinitions.txtOnlyNumbers(ref this.txt3);
            behaviorDefinitions.txtOnlyNumbers(ref this.txt4);

            behaviorDefinitions.txtPrice(this.txt2);
            behaviorDefinitions.txtPrice(this.txtcosto);

            controls.ElementAt(0).KeyPress += (x, y) => { if (y.KeyChar == (char)Keys.Enter) controls.ElementAt(1).Focus(); };
            controls.ElementAt(1).KeyPress += (x, y) => { if (y.KeyChar == (char)Keys.Enter) controls.ElementAt(2).Focus(); };
            controls.ElementAt(2).KeyPress += (x, y) => { if (y.KeyChar == (char)Keys.Enter) controls.ElementAt(3).Focus(); };
            controls.ElementAt(3).KeyPress += (x, y) => { if (y.KeyChar == (char)Keys.Enter) controls.ElementAt(4).Focus(); };
            controls.ElementAt(4).KeyPress += (x, y) => { if (y.KeyChar == (char)Keys.Enter) controls.ElementAt(5).Focus(); };

            controls.ElementAt(5).KeyUp += (x, y) => { if (this.txt4.Text.Trim() != "") this.picbox.Image = this.cbar.getImage(this.txt4.Text.Trim()); };
            controls.ElementAt(5).KeyPress += (x, y) => { if (y.KeyChar == (char)Keys.Enter && (this.txt4.Text.Trim() != "")) this.onAccept(); };

            for (int x = 0; x < 101; ++x)
            {
                this.cbganancia.Items.Add(x.ToString() + "%");
            }

            this.cbganancia.SelectedIndex = 0;

            this.baccept.Click += (x, y) => 
            {
                this.onAccept();
            };

            this.bcancel.Click += (x, y) =>
            {
                this.onCancel();
            };

            this.cbganancia.SelectedIndexChanged += (sender, args) => {
                if (this.txtcosto.Text.Trim() != string.Empty)
                {
                    Double val = Double.Parse(this.txtcosto.Text, CultureInfo.InvariantCulture);
                    this.txt2.Text = (val + ((val / 100) * this.cbganancia.SelectedIndex)).ToString("F2");
                }
            };
        }

    }
}
