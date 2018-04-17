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
using MundoMusical.CONSULTAS.CLIENTES;
using MundoMusical.CONSULTAS.VENTAS;
using MundoMusical.PRODUCT;
using MundoMusical.CONSULTAS.INICIOS_SESION;

namespace MundoMusical.CONSULTAS
{
    public partial class consultas_central : baseformextends
    {
        private consulta_clientes consclientes;
        private consulta_ventas consventas;
        private seeproductsinternal consproductos;
        private consultas_inicios consinicios;

        public consultas_central()
        {
            InitializeComponent();
            this.initialsettings();
        }

        public consultas_central(Central2 central)
        {
            this.central = central;
            InitializeComponent();
            this.initialsettings();
            this.FormClosed += (sender, args) => { this.central.consultas = null; };
        }

        private void initialsettings()
        {
            this.Text = "Consultas";
        }

        private void consultas_central_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.panelcentral;                       
        }

        private void blapse_Click(object sender, EventArgs e)
        {
            this.lblinst.Location = new Point(this.panelcentral.Width / 2 - this.lblinst.Width / 2, this.panelcentral.Height / 2 - this.lblinst.Height / 2);
        }

        private void btnventas_Click(object sender, EventArgs e)
        {
            this.lblinst.Visible = false;

            this.panelcentral.Controls.Clear();

            this.panelventas.BackColor = genericDefinitions.btnselectedcolor;
            this.panelclientes.BackColor = genericDefinitions.btnfreecolor;
            this.panelproductos.BackColor = genericDefinitions.btnfreecolor;
            this.panelSesion.BackColor = genericDefinitions.btnfreecolor;

            this.consventas = new consulta_ventas(this) {TopLevel = false};            
            this.panelcentral.Controls.Add(this.consventas);
            this.consventas.Dock = DockStyle.Fill;
            this.consventas.Show();
        }

        private void btnclientes_Click(object sender, EventArgs e)
        {
            this.lblinst.Visible = false;

            this.panelcentral.Controls.Clear();

            this.panelventas.BackColor = genericDefinitions.btnfreecolor;
            this.panelclientes.BackColor = genericDefinitions.btnselectedcolor;
            this.panelproductos.BackColor = genericDefinitions.btnfreecolor;
            this.panelSesion.BackColor = genericDefinitions.btnfreecolor;

            this.consclientes = new consulta_clientes(this) { TopLevel = false };
            this.consclientes.Dock = DockStyle.Fill;
            this.panelcentral.Controls.Add(this.consclientes);
            this.consclientes.Show();
        }

        private void btnproductos_Click(object sender, EventArgs e)
        {
            this.lblinst.Visible = false;

            this.panelcentral.Controls.Clear();

            this.panelventas.BackColor = genericDefinitions.btnfreecolor;
            this.panelclientes.BackColor = genericDefinitions.btnfreecolor;
            this.panelproductos.BackColor = genericDefinitions.btnselectedcolor;
            this.panelSesion.BackColor = genericDefinitions.btnfreecolor;
            this.consproductos = new seeproductsinternal(this) {TopLevel = false};
            this.panelcentral.Controls.Add(this.consproductos);
            this.consproductos.Dock = DockStyle.Fill;
            this.consproductos.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.lblinst.Visible = false;

            this.panelcentral.Controls.Clear();

            this.panelventas.BackColor = genericDefinitions.btnfreecolor;
            this.panelclientes.BackColor = genericDefinitions.btnfreecolor;
            this.panelproductos.BackColor = genericDefinitions.btnfreecolor;
            this.panelSesion.BackColor = genericDefinitions.btnselectedcolor;

            this.consinicios = new consultas_inicios(this) {TopLevel = false};
            this.consinicios.Dock = DockStyle.Fill;
            this.panelcentral.Controls.Add(this.consinicios);
            this.consinicios.Show();
            

        }
    }
}
