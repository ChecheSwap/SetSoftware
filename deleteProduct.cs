using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MundoMusical;
namespace MundoMusical.PRODUCT
{
    public partial class deleteProduct : updateProduct
    {
        public deleteProduct()
        {
            InitializeComponent();
            this.Text = "Eliminar Producto";
            this.KeyPreview = true;
            this.KeyPress += (s, a) => { if (a.KeyChar == (char)Keys.Enter) this.onAccept(); };
        }
        public deleteProduct(midformProduct origen)
        {
            InitializeComponent();
            this.Text = "Eliminar Producto";
            this.origen = origen;
            this.origen.deletebtn.Enabled = false;
            this.FormClosed += (x, y) => { this.origen.deletebtn.Enabled = true; };
            this.KeyPreview = true;
            this.KeyPress += (s, a) => { if (a.KeyChar == (char)Keys.Enter) this.onAccept(); };
            this.stopBounds();
        }

        private void deleteProduct_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.txt1;
        }

        protected override void onAccept()
        {
            if(genericDefinitions.ask("Desea eliminar Producto con ID: " + this.prod.idproducto + "?", "Confirmacion de seguridad"))
            {
                if (db.deleteproduct(this.prod))
                {
                    if (this.cbnotifica.Checked)
                    {
                        genericDefinitions.ok("Producto eliminado", "Echo");
                    }
                    
                    this.onCancel();
                }                
            }
            else
            {
                this.onCancel();
            }
        }
        protected override void setAtribs()
        {
            this.controls = new List<Control>() { this.txt1, this.txt2, this.txt3, this.cb1, this.txt4 };
            this.tboxes = new List<TextBox>() { this.txt1, this.txt2, this.txt3, this.txt4 };
            this.txt1.Enabled = false;
            this.txt2.Enabled = false;
            this.txt3.Enabled = false;
            this.txt4.Enabled = false;
            this.cb1.Enabled = false;
            this.txtcosto.Enabled = false;
            this.cbganancia.Enabled = false;

            for (int x = 0; x < 101; ++x)
            {
                this.cbganancia.Items.Add(x.ToString() + "%");
            }

            this.baccept.Click += (x, y) => { this.onAccept(); };
            this.bcancel.Click += (x, y) => { this.onCancel(); };
        }

        protected override void onshown()
        {
            if (!DesignMode)
            {
                productInternals pdi = new productInternals(this);
                pdi.ShowDialog();

                if (this.db.productisondetalles(this.idproduct))
                {
                    genericDefinitions.dangerInfo("No se puede eliminar este producto, contacte al administrador!.", "Advertencia");
                    this.onCancel();
                }

                if (this.idproduct == -1)
                    this.Close();
                else
                {
                    this.getdata();
                    this.txt1.Focus();
                }
            }
        }

    }
}
