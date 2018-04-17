using MundoMusical.DB;
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
namespace MundoMusical.VENTA
{
    public partial class cancelarVenta : BaseForm
    {
        private DataTable tabla;
        private dbop db;
        private venta xventa;
        private Central2 central;

        private int heightinit;
        private int heightlast = 633;
        public cancelarVenta(Central2 central)
        {
            InitializeComponent();
            this.central = central;
            this.FormClosed += (sender, args) => { central.xcancelarventa = null; };
            this.initfunc();
        }
        public cancelarVenta()
        {
            InitializeComponent();
            this.MinimizeBox = false;
            this.initfunc();
        }

        private void initfunc()
        {
            this.heightinit = this.Height;
            this.Text = "Cancelar Venta";
            this.tabla = new DataTable();
            this.db = new dbop();
            this.Move += (sender, args) =>
            {
                this.Refresh();
            };
            this.dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            behaviorDefinitions.txtOnlyNumbers(ref this.txtfirst);
            this.txtfirst.KeyPress += (x, y) =>
            {
                if (y.KeyChar == (char)Keys.Enter)
                {
                    this.validate();
                }
            };
            this.btncancelarventa.GotFocus += (sender, args) =>
            {
                this.dgv.Focus();
            };
            this.ShowInTaskbar = false;
        }

        private void validate()
        {
            if (this.db.existventa(int.Parse(this.txtfirst.Text.Trim())))
            {
                this.xventa = this.db.getventa(int.Parse(this.txtfirst.Text.Trim()));
                if (this.xventa.status == statusVenta.ACTIVA)
                {
                    this.panelfirst.Visible = false;
                    this.label2.Visible = true;
                    this.txtnumerodeventa.Visible = true;                    
                    this.panel1.Visible = true;
                    this.btncancelarventa.Visible = true;
                    this.Height = this.heightlast;
                    this.CenterToScreen();
                    this.initcommit();
                }
                else
                {
                    genericDefinitions.error("Esta venta ya ha sido cancelada", "Seguridad");
                    this.txtfirst.Text = "";
                    this.txtfirst.Focus();
                }
            }
            else
            {
                genericDefinitions.error("Venta invalida", "Seguridad");
                this.txtfirst.Text = "";
                this.txtfirst.Focus();
            }
        }

        private void cancelarVenta_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.txtfirst;            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            if (genericDefinitions.ask("¿Desea cancelar venta?", "Confirmación"))
            {
                if (this.db.cancelarventa(this.xventa.idventa))
                {
                    if (this.db.restablecerdetalles(this.xventa.idventa))
                    {
                        genericDefinitions.ok("Venta Cancelada", "Seguridad");                        
                    }
                    else
                    {
                        genericDefinitions.error("Error al restablecer detalles, consulte inventario de forma manual", "Seguridad");
                    }                    
                }
                else
                {
                    genericDefinitions.error("Error al cancelar venta", "Seguridad");                    
                }
            }
            if (this.central == null)
            {
                this.Close();
            }
            else
            {
                this.panelfirst.Visible = true;
                this.label2.Visible = false;
                this.txtnumerodeventa.Visible = false;
                this.panel1.Visible = false;
                this.btncancelarventa.Visible = false;
                this.Height = this.heightinit;
                this.txtfirst.Text = string.Empty;
                this.txtfirst.Focus();
            }
        }

        private void initcommit()
        {
            this.db.filldetalledgv(this.xventa.idventa, ref this.dgv);
            this.txtnumerodeventa.Text = this.xventa.idventa.ToString();
            this.txtarticulos.Text = this.xventa.articulos.ToString();
            this.txtsubtotal.Text = "$"+this.xventa.subtotal.ToString();
            this.txtiva.Text = "$"+this.xventa.iva.ToString();
            this.txtdescuentoextra.Text = "$"+this.xventa.descuentoextra.ToString();
            this.txttotal.Text = this.xventa.total.ToString();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            genericDefinitions.ok("Si usted usa un lector de codigos de barras, no es necesario presionar enter, de lo contrario es obligatorio", "Instrucciones");
        }
    }
}
