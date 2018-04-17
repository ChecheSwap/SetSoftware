using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MundoMusical.CATEGORY;

namespace MundoMusical.PAYMODE
{
    public partial class seepaymodes : seecategory
    {
        public seepaymodes():base()
        {
            InitializeComponent();
            this.Text = "Ver modos de pago";
        }

        public seepaymodes(Central2 central) : base(central)
        {
            InitializeComponent();
            this.Text = "Ver modos de pago";
        }

        protected override void onfirst()
        {
            this.db.getcustommodopagonombre(ref this.dgv, this.txtname.Text.TrimStart());
        }

        protected override void onsecond()
        {
            this.db.getcustommodopagodes(ref this.dgv, this.txtdesc.Text.TrimStart());
        }

        protected override void onthird()
        {
            this.db.getcustommodopagoid(ref this.dgv, this.txtnumero.Text.TrimStart());
        }

        protected override void chargeInit()
        {
            if (!this.db.getmodopagoDGV(ref this.dgv)){
                this.labelx.Visible = true;
                this.gbx.Enabled = false;
            }
        }

        protected override void onClose()
        {
            this.central.seepay = null;
        }
        private void seepaymodes_Load(object sender, EventArgs e)
        {

        }
    }
}
