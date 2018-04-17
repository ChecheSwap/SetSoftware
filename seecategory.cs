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

namespace MundoMusical.CATEGORY
{
    public partial class seecategory : gridquerybase
    {
        public seecategory()
        {
            InitializeComponent();
            this.dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.Text = "Categorias";

            this.Resize += (x, y) => {
                this.pb.Location = new Point((this.gb1.Width / 2) + this.gb1.Location.X - this.pb.Width / 2, this.pb.Location.Y);
            };
            
            this.db = new DB.dbop();
            behaviorDefinitions.txtOnlyNumbers(ref this.txtnumero);
            behaviorDefinitions.txtUPPER(this.txtname);
            behaviorDefinitions.txtUPPER(this.txtdesc);
            this.txtname.KeyUp += (x, y) =>
            {
                this.onfirst();
            };

            this.txtdesc.KeyUp += (x, y) =>
            {
                this.onsecond();
            };

            this.txtnumero.KeyUp += (x, y) =>
            {
                this.onthird();
            };


            this.txtname.GotFocus += (sender, args) => {
                this.chargeInit();
                this.txtdesc.Text = null;
                this.txtnumero.Text = null;
            };

            this.txtdesc.GotFocus += (sender, args) => {
                this.chargeInit();
                this.txtname.Text = null;
                this.txtnumero.Text = null;
            };

            this.txtnumero.GotFocus += (sender, args) => {
                this.chargeInit();
                this.txtdesc.Text = null;
                this.txtname.Text = null;
            };

            this.txtname.KeyDown += (e, x)=>{
                if (x.KeyCode == Keys.Enter)
                {                    
                    this.txtdesc.Focus();   
                }
            };
            this.txtdesc.KeyDown += (e, x) => {
                if (x.KeyCode == Keys.Enter)
                {
                    this.txtnumero.Focus();
                }
            };
            this.txtnumero.KeyDown += (e, x) => {
                if (x.KeyCode == Keys.Enter)
                {
                    this.txtname.Focus();
                }
            };
        }
        public seecategory(Central2 central)
        {
            
            InitializeComponent();
            this.dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.Text = "Categorias";

            this.central = central;

            this.Resize += (x, y) => {                
                    this.pb.Location = new Point((this.gb1.Width / 2) + this.gb1.Location.X - this.pb.Width / 2, this.pb.Location.Y);
            };

            this.FormClosed += (x, y) => { this.onClose(); };
            this.db = new DB.dbop();
            behaviorDefinitions.txtOnlyNumbers(ref this.txtnumero);
            behaviorDefinitions.txtUPPER(this.txtname);
            behaviorDefinitions.txtUPPER(this.txtdesc);

            this.txtname.KeyUp += (x, y) =>
            {
                this.onfirst();
            };

            this.txtdesc.KeyUp += (x, y) =>
            {
                this.onsecond();
            };

            this.txtnumero.KeyUp += (x, y) =>
            {
                this.onthird();
            };


            this.txtname.GotFocus += (sender, args)=>{
                this.chargeInit();
                this.txtdesc.Text = null;
                this.txtnumero.Text = null;
            };

            this.txtdesc.GotFocus += (sender, args)=>{
                this.chargeInit();
                this.txtname.Text = null;
                this.txtnumero.Text = null;
            };

            this.txtnumero.GotFocus += (sender, args)=>{
                this.chargeInit();
                this.txtdesc.Text = null;
                this.txtname.Text = null;
            };

            this.txtname.KeyDown += (e, x) => {
                if (x.KeyCode == Keys.Enter)
                {
                    this.txtdesc.Focus();
                }
            };
            this.txtdesc.KeyDown += (e, x) => {
                if (x.KeyCode == Keys.Enter)
                {
                    this.txtnumero.Focus();
                }
            };
            this.txtnumero.KeyDown += (e, x) => {
                if (x.KeyCode == Keys.Enter)
                {
                    this.txtname.Focus();
                }
            };
        }

        protected virtual void onfirst()
        {
            this.db.getcustomcatName(ref this.dgv, this.txtname.Text.Trim());
        }

        protected virtual void onsecond()
        {
            this.db.getcustomcatdescripcion(ref this.dgv, this.txtdesc.Text.Trim());
        }

        protected virtual void onthird()
        {
            this.db.getcustomcatnumero(ref this.dgv, this.txtnumero.Text.Trim());
        }

        protected virtual void chargeInit()
        {
            if (!this.db.getallcategoriesDGV(ref this.dgv))
            {
                this.labelx.Visible = true;
                this.gbx.Enabled = false;
            }
                
        }
        private void seecategory_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {                           
                this.chargeInit();
                this.ActiveControl = this.txtname;
            }
        }
    }
}
