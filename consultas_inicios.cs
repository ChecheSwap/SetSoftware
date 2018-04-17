using MundoMusical.DB;
using MundoMusical.XBASE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MundoMusical.CONSULTAS.INICIOS_SESION
{
    public partial class consultas_inicios : baseConsultas
    {

        private dbop db;

        public consultas_inicios(consultas_central central)
        {
            InitializeComponent();
            this.db = new dbop();

            this.setatribs();

            this.FormClosed += (sender, args) => { central.Close(); };
        }
        public consultas_inicios()
        {
            InitializeComponent();
            this.db = new dbop();

            this.setatribs();
        }

        private void setatribs()
        {
            this.cbusuario.DataSource = this.db.getusuariosNames();
            this.cbusuario.SelectedIndex = -1;
                        
            this.dgv.DataSourceChanged += (sender, args) => { this.lblregs.Text = "Registros:" + this.dgv.RowCount.ToString(); };
            this.dtpinicio.KeyUp += (sender, args) => { this.db.getinicios(ref this.dgv, "inicio", this.dtpinicio.Text.Trim()); };

            this.dtpinicio.ValueChanged += (sender, args) => { this.db.getinicios(ref this.dgv, "inicio", this.dtpinicio.Text.Trim()); };

            this.dtpcierre.KeyUp += (sender, args) => { this.db.getinicios(ref this.dgv, "inicio", this.dtpcierre.Text.Trim()); };

            this.dtpcierre.ValueChanged += (sender, args) => { this.db.getinicios(ref this.dgv, "inicio", this.dtpcierre.Text.Trim()); };

            this.cbusuario.TextChanged += (sender, args) => {

                if(this.cbusuario.Text != string.Empty)
                {
                    this.db.getinicios(ref this.dgv, "usuario", this.cbusuario.Text.Trim());
                }

            };

            this.dtpinicio.GotFocus += (sender, args) => { this.loadAll(); };
            this.dtpcierre.GotFocus += (sender, args) => { this.loadAll(); };
            this.cbusuario.GotFocus += (sender, args) => { this.loadAll(); };            
        }
        private void consultas_inicios_Load(object sender, EventArgs e)
        {
            this.loadAll();
        }        
        private void loadAll()
        {            
            this.dtpinicio.Text = DateTime.Now.ToShortDateString();
            this.dtpcierre.Text = DateTime.Now.ToShortDateString();
            this.cbusuario.SelectedIndex = -1;

            this.db.getinicios(ref this.dgv, "all", null);
        }
    }
}
