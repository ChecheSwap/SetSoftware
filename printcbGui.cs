using MundoMusical.CONFIGURACION_DE_IMPRESION;
using MundoMusical.CUSTOM_CONTROLS;
using MundoMusical.DB;
using MundoMusical.PRODUCT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MundoMusical.LABELS
{
    public partial class printcbGui : BaseForm
    {

        private dbop db;
        private Thread thprint;
        private printCbar xprint;
        private List<int> codigos;
        private seeproductsinternal buscaProducto;
        public printcbGui()
        {
            InitializeComponent();
            this.db = new dbop();
            this.xprint = new printCbar();


            this.myload();
        }

        public printcbGui(Central2 central)
        {
            InitializeComponent();
            this.db = new dbop();
            this.xprint = new printCbar();


            this.myload();

            this.FormClosed += (sender, args) => { central.etiquetas = null; };
        }

        private void printcbGui_Load(object sender, EventArgs e)            
        {
            this.ActiveControl = this.txtcodigo;
        }

        private void rbtodas_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbtodas.Checked)
            {
                this.rbindividual.Checked = false;
            }
        }

        private void rbindividual_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbindividual.Checked)
            {
                this.rbtodas.Checked = false;
            }
        }

        private void loadCodigos()
        {
            this.codigos = this.db.getallProductId();
        }


        private void myload()
        {
            behaviorDefinitions.txtOnlyNumbers(ref this.txtcodigo);
            this.txttotalprods.Text = this.db.getProductosCount().ToString() + " Codigos";
            this.loadCodigos();
        }

        private void printSelected()
        {
            if (this.rbindividual.Checked || this.rbtodas.Checked)
            {
                this.thprint = new Thread(new ThreadStart(this.print)) { IsBackground = true };
                this.thprint.Start();
            }
            else
            {
                genericDefinitions.dangerInfo("Seleccione una opcion!");
            }
        }

        private void print()
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                int copies =(int) this.nup.Value;

                if (this.rbindividual.Checked)
                {

                    if (this.db.existproduct(int.Parse(this.txtcodigo.Text.Trim())))
                    { 
                        this.xprint.setParams(int.Parse(this.txtcodigo.Text.Trim()));
                        this.xprint.print(copies);                                    
                    }
                    else
                    {
                        genericDefinitions.error("Id producto Invalido!");
                        this.txtcodigo.Focus();                        
                    }
                }
                else if (this.rbtodas.Checked)
                {
                    foreach (int obj in this.codigos)
                    {
                        this.xprint.setParams(obj);
                        this.xprint.print(copies);
                    }
                }

                this.nup.Value = 1;
                this.txtcodigo.Text = "";

            }));
        }

        private void baccept_Click(object sender, EventArgs e)
        {
            if (this.rbindividual.Checked && this.txtcodigo.Text.Trim() == "")
            {
                genericDefinitions.dangerInfo("Ingrese Un Codigo de Producto a Imprimir.");
                this.txtcodigo.Focus();

                return;
            }

            if (this.rbtodas.Checked)
            {
                this.loadCodigos();

                if (genericDefinitions.yesno("¿Confirma la Impresion de " + (this.codigos.Count * this.nup.Value).ToString() + " Etiquetas?", "Security Process"))
                {
                    if (!printerControl.printIsSafe(this.db.getprinteretiquetaname()))
                    {
                        using (printWarning pw = new printWarning())
                        {
                            pw.ShowDialog();
                        }

                        return;
                    }
                }
                else
                {
                    return;
                }
            }

            this.printSelected();
        }

        private void btncat_Click(object sender, EventArgs e)
        {
            this.myload();
        }

        private void btnBuscaproducto_Click(object sender, EventArgs e)
        {
            this.buscaProducto = new seeproductsinternal(this);
            this.buscaProducto.ShowDialog();
        }
    }
}
