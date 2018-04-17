using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MundoMusical.DB;
using System.Threading;
using MundoMusical.EFFECTS;

namespace MundoMusical.ATOM
{
    public partial class initForm : BaseForm
    {
        private dbop DB;
        
        public initForm()
        {
            InitializeComponent();
            this.DB = new dbop();     
            this.Shown += (a, b) => { this.effectOpen(); };
        }

        private void effectOpen()
        {
            xfe.blendOpen(this,1000);
        }

        private void effectClose()
        {
            xfe.blend(this,1000);
        }
        private void initForm_Load(object sender, EventArgs e)
        {
            this.timer.Enabled = true;
        }

        private void timer_Tick(object sender, EventArgs e)
        {            
            if(this.progressBar1.Value == 7)
            {
                if (!this.DB.checkDB())
                {
                    this.timer.Enabled = false;
                    genericDefinitions.error("Error con conexión a la base de datos del sistema.\n           >Nota:'Contacte al desarrollador'", "Interops Service");
                    this.Close();
                }

                if(Screen.PrimaryScreen.WorkingArea.Height < 1040)
                {
                    this.timer.Enabled = false;
                    genericDefinitions.dangerInfo("-La resolución de pantalla recomendada es 1920 x 1080 Pixeles.\n-Nota:El sistema puede presentar inconvenientes en resoluciones bajas.");
                    this.timer.Enabled = true;
                }                
            }
            
            if(this.progressBar1.Value == 15)
            {
                this.effectClose();                     
                this.Close();
                Thread nov = new Thread(this.new_PROCESSWNDPROC);
                nov.SetApartmentState(ApartmentState.STA);
                nov.Start();

            }
            else
            {
                this.progressBar1.Value += 1;
            }
            
        }
        private void new_PROCESSWNDPROC()
        {
            Application.Run(new Login1());
        }

    }
}
