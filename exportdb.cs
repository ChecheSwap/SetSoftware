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
using MundoMusical.XBASE;
using System.IO;

namespace MundoMusical.DB.DUMPS
{
    public partial class exportdb : BaseForm
    {
        private dbop db;
        private SaveFileDialog sfd;
        private OpenFileDialog ofd;
        private Central2 central;

        private Thread th1;
        private Thread th2;

        private inneradvice aviso;

        private securityPassword importpass;

        public exportdb(Central2 central)
        {
            InitializeComponent();
            this.central = central;
            this.initialsettings();
            this.FormClosed += (a, b) => { this.central.dboperations = null; };
            this.aviso = new inneradvice();
        }

        public exportdb()
        {
            InitializeComponent();
            this.initialsettings();
            this.aviso = new inneradvice();
        }

        private void initialsettings() // CONFIGURACION INICIAL
        {           

            this.db = new dbop();

            this.ofd = new OpenFileDialog()
            {
                Title = "Seleccione Script Sql",
                Filter = ".sql (Script .sql)|*.sql",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                CheckFileExists = true,
                CheckPathExists = true,
                RestoreDirectory = true
            };

            this.sfd = new SaveFileDialog()
            {
                Title = "Guardar Script Sql",
                Filter = ".sql (Script .sql)|*.sql",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                CheckPathExists = true,
                RestoreDirectory = true
            };

            this.Text = "Exportar Base de Datos";

            this.cbopt.Items.Add("Exportar");
            this.cbopt.Items.Add("Importar");
            this.cbopt.SelectedItem = "Exportar";
            this.cbopt.SelectedIndexChanged += (a, b) => { this.typeoperation(); };
        }

        private void typeoperation() //OCURRE AL CAMBIAR EL TIPO DE OPERACION EN EL COMBO BOX
        {
            if (this.cbopt.SelectedItem.ToString() == "Exportar")
            {
                this.lblleyenda.Text = "Destino de Script";
                this.gb.Text = ">>Seleccione Ruta de Destino";
                this.tt1.SetToolTip(this.btnaction, "Exportar Base de datos");
                this.Text = "Exportar Base de Datos";
            }
            else
            {
                this.lblleyenda.Text = "Origen de Script";
                this.gb.Text = ">>Seleccione Ruta de Origen";
                this.tt1.SetToolTip(this.btnaction, "Importar Base de datos");
                this.Text = "Importar Base de Datos";
            }
        }

        private void make() //OCURRE AL REALIZAR CLICK DE ACCION
        {
            if (this.txtor1.Text.Trim() == string.Empty)
            {
                if (this.cbopt.SelectedItem.ToString() == "Exportar")
                {
                    genericDefinitions.dangerInfo("Seleccione Ruta!");
                }
                else
                {
                    genericDefinitions.dangerInfo("Seleccione Origen!");
                }
                return;

            }

            if (this.cbopt.SelectedItem.ToString() == "Exportar")
            {
                if (this.th1 != null && this.th1.IsAlive)
                {
                    genericDefinitions.dangerInfo("Se esta exportando la Base de Datos!");
                }
                else
                {
                    this.th1 = new Thread(new ThreadStart(delegate () { this.exportar(); })) {IsBackground = true};
                    this.th1.Start();
                }
                
            }
            else
            {
                if(this.th2 != null && this.th2.IsAlive)
                {
                    genericDefinitions.dangerInfo("Se esta importando la Base de Datos!");
                }
                else
                {
                    this.importpass = new securityPassword();
                    this.importpass.ShowDialog();

                    if (this.importpass.result)
                    {
                        if (!Directory.Exists(genericDefinitions.RESPALDOSEGURIDADDBPATH))
                        {
                            Directory.CreateDirectory(genericDefinitions.RESPALDOSEGURIDADDBPATH);
                        }

                        if (this.db.dumpDB(genericDefinitions.RESPALDOSEGURIDADDBPATH+"\\RESPALDO("+genericDefinitions.getDate().Replace("/","")+genericDefinitions.getHours() + genericDefinitions.getMinutes()+ genericDefinitions.getSeconds() +").sql"))
                        {
                            this.th2 = new Thread(new ThreadStart(delegate () { this.importar(); })) { IsBackground = true };
                            this.th2.Start();
                        }
                        else
                        {
                            genericDefinitions.error("Error al Realizar respaldo de seguridad, imposible continuar");
                        }

                    }
                }
            }

            this.txtor1.Text = "";
        }

        private void exportar() //PROCESO DE EXPORTACION
        {
            genericDefinitions.closeAccess = false;

            this.Invoke(new MethodInvoker(delegate () {
                this.aviso = new inneradvice("Exportando Base de Datos") { MdiParent = this.central};
                this.Visible = false;                
                this.aviso.Show();
            }));

            if (this.db.dumpDB(this.sfd.FileName))
            {
                
                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate () {
                        this.aviso.Close();
                        genericDefinitions.ok("La base de datos ha sido exportada satisfactoriamente");
                        this.Visible = true;                        
                    }));
                }
                else
                {
                    this.aviso.Close();
                    genericDefinitions.ok("La base de datos ha sido exportada satisfactoriamente");
                    this.Visible = true;                  
                }

            }
            else
            {
                

                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        this.aviso.Close();
                        this.Visible = true;                        
                        genericDefinitions.error("Error al exportar base de datos.");

                    }));
                }
                else
                {
                    this.aviso.Close();
                    this.Visible = true;                    
                    genericDefinitions.ok("Error al exportar base de datos.");
                }
            }

            genericDefinitions.closeAccess = true;
        }

        private void importar() //PROCESO DE IMPORTACION
        {
            genericDefinitions.closeAccess = false;

            this.Invoke(new MethodInvoker(delegate () { 
                this.aviso = new inneradvice("Importando Base de Datos") {MdiParent = this.central};
                this.Visible = false;
                this.aviso.Show();               
            }));
            
            if (this.db.restoreDB(this.ofd.FileName))
            {
                
                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        this.aviso.Close();                        
                        genericDefinitions.ok("La base de datos ha sido importada satisfactoriamente!");
                        this.Visible = true;

                    }));
                }
                else
                {                    
                    genericDefinitions.ok("La base de datos ha sido importada satisfactoriamente!");
                    this.Visible = true;
                }
            }
            else
            {
                
                if (this.InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        this.aviso.Close();
                        genericDefinitions.error("Error al importar base de datos.");
                        this.Visible = true;

                    }));
                }
                else
                {
                    this.aviso.Close();
                    genericDefinitions.ok("Error al importar base de datos.");
                    this.Visible = true;
                }
            }

            genericDefinitions.closeAccess = true;

        }
  
        private void exportdb_Load(object sender, EventArgs e)
        {
            this.ActiveControl = this.panel1;
        }

        private void btnloadimage_Click(object sender, EventArgs e)
        {
            if(this.cbopt.SelectedItem.ToString() == "Exportar")
            {

                if (this.sfd.ShowDialog() == DialogResult.OK)
                {
                    this.txtor1.Text = this.sfd.FileName;
                }
            }
            else
            {
                if (this.ofd.ShowDialog() == DialogResult.OK)
                {
                    this.txtor1.Text = this.ofd.FileName;
                }
            }
        }

        private void btnaction_Click(object sender, EventArgs e)
        {
            this.make();
        }
    }
}
