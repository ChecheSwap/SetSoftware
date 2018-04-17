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
using System.Globalization;

namespace MundoMusical.CUSTOMER
{
    public partial class add : XBASE.customerBase

    {
        protected List<TextBox> txtboxes;
        protected List<Control> controls;
        protected string[] dataTxt;
        protected dbop db;
        public int idCliente;
        protected string rfc, curp;

        public add()
        {
            InitializeComponent();
            this.Text = "Agregar Cliente";
        }

        public add(midformCustomer midform)
        {
            InitializeComponent();
            this.Text = "Agregar Cliente";
            this.midform = midform;
            this.FormClosed += this.onClose;
            this.midform.addbtn.Enabled = false;
            this.BackColor = base.BackColor;
            this.stopBounds();


        }

        private void add_Load(object sender, EventArgs e)
        {            
            if (!DesignMode)
            {
                if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
                {
                    this.initializeT();
                }                
                this.baccept.Click += (xsender, args) => { this.onAccept(); };
                this.bcancel.Click += (xsender, args) => { this.clearTxt(); };
                this.ActiveControl = this.txt1;
            }           
        }

        private void onClose(object sender, EventArgs args)
        {
            this.midform.addbtn.Enabled = true;
        }

        private void getObject(Button control)
        {
            control.Enabled = false;
        }
        

        protected void initializeT()
        {
            this.db = new dbop();
            this.dataTxt = new string[12];
            this.cboxestados.DataSource = estados.getestados();
            this.txtboxes = new List<TextBox>() { this.txt1, this.txt2, this.txt3, this.txt4, this.txt5, this.txt6, this.txt7, this.txt8, this.txt9 , this.txt10};
            this.controls = new List<Control>() { this.txt1, this.txt2,this.dtpdate, this.txt3, this.txt4, this.txt5, this.txt6, this.txt7, this.txt8, this.txt9 , this.txt10 , this.cboxestados};
            this.txt1.KeyDown += (xsender, args) => { if (args.KeyCode == Keys.Enter && this.txt1.Text != "") this.ActiveControl = this.txt2;};
            this.txt2.KeyDown += (xsender, args) => { if (args.KeyCode == Keys.Enter && this.txt2.Text != "") this.ActiveControl = this.dtpdate; };
            this.dtpdate.KeyDown += (xsender, args) => { if (args.KeyCode == Keys.Enter)this.ActiveControl =  this.txt3;};
            this.txt3.KeyDown += (xsender, args) => { if (args.KeyCode == Keys.Enter && this.txt3.Text != "") this.ActiveControl = this.txt4;};
            this.txt4.KeyDown += (xsender, args) => { if (args.KeyCode == Keys.Enter && this.txt4.Text != "") this.ActiveControl = this.txt5; };
            this.txt5.KeyDown += (xsender, args) => { if (args.KeyCode == Keys.Enter && this.txt5.Text != "") this.ActiveControl = this.txt6;};
            this.txt6.KeyDown += (xsender, args) => { if (args.KeyCode == Keys.Enter && this.txt6.Text != "") this.ActiveControl = this.txt7; };
            this.txt7.KeyDown += (xsender, args) => { if (args.KeyCode == Keys.Enter && this.txt7.Text != "") this.ActiveControl = this.cboxestados; };
            this.cboxestados.KeyDown += (xsender, args) => { if (args.KeyCode == Keys.Enter && this.cboxestados.Text != "") this.ActiveControl = this.txt10; };
            this.txt10.KeyDown += (xsender, args) => { if (args.KeyCode == Keys.Enter && this.txt10.Text != "") this.ActiveControl = this.txt8; };
            this.txt8.KeyDown += (xsender, args) => { if (args.KeyCode == Keys.Enter && this.txt8.Text != "") this.ActiveControl = this.txt9; };
            this.txt9.KeyDown += (xsender, args) => { if (args.KeyCode == Keys.Enter && this.txt9.Text != "") this.onAccept(); };
            this.txt8.MaxLength = 18;
            this.txt9.MaxLength = 13;
            this.txt1.LostFocus += (xsender, args) => { if (this.txt1.Text != "") {this.txt1.Text = this.txt1.Text.ToUpper(); } };
            this.txt2.LostFocus += (xsender, args) => { if (this.txt2.Text != "") {this.txt2.Text = this.txt2.Text.ToUpper(); } };            
            this.txt3.LostFocus += (xsender, args) => { if (this.txt3.Text != "") {this.txt3.Text = this.txt3.Text.ToUpper(); } };
            this.txt4.LostFocus += (xsender, args) => { if (this.txt4.Text != "") {this.txt4.Text = this.txt4.Text.ToUpper(); } };
            this.txt5.LostFocus += (xsender, args) => { if (this.txt5.Text != "") {this.txt5.Text = this.txt5.Text.ToUpper(); } };
            this.txt6.LostFocus += (xsender, args) => { if (this.txt6.Text != "") {this.txt6.Text = this.txt6.Text.ToUpper(); } };
            this.txt7.LostFocus += (xsender, args) => { if (this.txt7.Text != "") {this.txt7.Text = this.txt7.Text.ToUpper(); } };
            this.txt8.LostFocus += (xsender, args) => { if (this.txt8.Text != "") {this.txt8.Text = this.txt8.Text.ToUpper(); } };
            this.txt9.LostFocus += (xsender, args) => { if (this.txt9.Text != "") {this.txt9.Text = this.txt9.Text.ToUpper(); } };
            behaviorDefinitions.txtOnlyNumbers(ref this.txt3);
            behaviorDefinitions.txtOnlyNumbers(ref this.txt10);
            this.dtpdate.Format = DateTimePickerFormat.Short;
        }


        protected virtual void onAccept()
        {
            this.insert();
            this.txt1.Focus();
        }

        private void insert()
        {
            if (this.validateData())
            {
                this.fillArray(ref this.dataTxt);

                if(this.db.insertClient(ref this.dataTxt))
                    this.clearTxt();

            }
        }

        protected void fillArray(ref string[]data)//FILL FROM TXTBOXES
        {
            int d = 0;
            foreach(Control c in this.controls)
            {
                if (c.Name == "dtpdate")
                {
                    data[d] = this.dtpdate.Value.Date.ToString("dd/MM/yyyy");
                }
                else
                {
                    data[d] = c.Text;
                }                
                ++d;
            }
        }
        protected void filltxtfromarray(ref string[] vals)//FILL TXTBOXES FROM ARRAY
        {
            int p = 0;

            foreach (Control c in this.controls)
            {
                if (c.Name == "dtpdate")
                {
                    try
                    {
                        this.dtpdate.Value = DateTime.Parse(vals[p]);
                    }
                    catch (Exception ex)
                    {
                        genericDefinitions.dangerInfo(ex.ToString(), "Advertencia");
                    }

                }
                else
                {
                    c.Text = vals[p];
                }
                ++p;
            }
        }

        protected void clearTxt()
        {            
            foreach(Control c in  this.Controls){

                foreach (Control x in c.Controls)
                {
                    if (x.GetType() == typeof(TextBox))
                    {
                        ((TextBox)x).Text = string.Empty;
                    }
                }
            }            
            this.cboxestados.SelectedIndex = 0;
            this.txt1.Focus();
        }

        protected bool validateData()
        {
            bool flag = true;

            foreach(TextBox c in this.txtboxes)
            {
                if (c.Text == "")
                {                    
                    flag = false;
                    break;
                }
                if(c.Name == "txt8" && c.TextLength != 18)
                {                    
                    flag = false;
                    break;
                }
                if(c.Name == "txt9" && c.TextLength < 12)
                {                    
                    flag = false;
                    break;
                }
                    
            }

            if (!flag)
            {
                genericDefinitions.dangerInfo("Datos Incorrectos, verifique datos de cliente.", "Aviso");
                return false;
            }
            return true;
        }

        protected void getInitialDataUPD()
        {
            if (db.existcliente(this.idCliente))
            {                
                if (this.db.isInFacturas(this.idCliente))
                {                    
                    this.dtpdate.Enabled = false;
                    this.txt1.Enabled = false;
                    this.txt2.Enabled = false;
                    this.txt8.Enabled = false;
                    this.txt9.Enabled = false;
                }
                this.dataTxt = this.db.getclientAsarray(this.idCliente);
                this.filltxtfromarray(ref this.dataTxt);
                this.curp = this.dataTxt[8];
                this.rfc = this.dataTxt[9];
            }
            else
            {
                genericDefinitions.error("ID No valido", "Error Sql Statment");
            }

        }

        protected void getInitialDataDEL()
        {
            this.dataTxt = this.db.getclientAsarray(this.idCliente);
            this.filltxtfromarray(ref this.dataTxt);
        }

        protected void disablecontrols()
        {
            foreach (Control t in controls)
            {
                t.Enabled = false;
            }
        }

        private void bcancel_Click(object sender, EventArgs e)
        {

        }

        private void baccept_Click(object sender, EventArgs e)
        {
         
        }
    }
}
