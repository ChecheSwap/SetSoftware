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
using MundoMusical.EFFECTS;

namespace MundoMusical
{
    public enum exitState { normal = 0, highest = 1 };

    public partial class BaseForm : Form
    {
        public exitState exitFlag = exitState.normal;

        private dbop db;

        public BaseForm()
        {
            InitializeComponent();

            this.db = new dbop();

            this.Text = "Mundo Musical";
            this.StartPosition = FormStartPosition.CenterScreen;            
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.BackColor = SystemColors.ButtonHighlight;
            this.escexitproperty();
            this.KeyPreview = true;            

            this.Load += (sender, args) =>
            {
                foreach (Control x in this.Controls)
                {
                    x.TabStop = false;
                    foreach (Control y in x.Controls)
                    {
                        y.TabStop = false;

                        foreach (Control z in y.Controls)
                        {
                            z.TabStop = false;

                            foreach(Control inter in z.Controls)
                            {
                                inter.TabStop = false;
                            }
                        }
                            
                    }
                }
            };            
        }
        
        protected void stopBounds()
        {
            this.Move += (sender, args) =>
            {
                if (this.Top < genericDefinitions.WND_TOP)
                {
                    this.Top = genericDefinitions.WND_TOP;
                }

                if (this.Left < genericDefinitions.WND_LEFT)
                {
                    this.Left = genericDefinitions.WND_LEFT;
                }

                if (this.Right > genericDefinitions.WND_RIGHT)
                {
                    this.Left = genericDefinitions.WND_RIGHT - this.Width;
                }

                if (this.Bottom > genericDefinitions.WND_BOTTOM)
                {
                    this.Top = genericDefinitions.WND_BOTTOM - Height;
                }
                this.Refresh();
            };            
        }
        

        protected void setBounds()
        {
            genericDefinitions.WND_RIGHT = this.Right;
            genericDefinitions.WND_BOTTOM = this.Bottom;
            genericDefinitions.WND_LEFT = this.Left;
            genericDefinitions.WND_TOP = this.Top;
        }

        private void BaseForm_Load(object sender, EventArgs e)
        {}

        public virtual void escexitproperty()
        {
            this.KeyDown += (x, y) => {

                if (y.KeyCode == Keys.Escape) this.Close();
            };
        }

        private void cierresesion()
        {
            if (genericDefinitions.globalsesion != null)
            {
                genericDefinitions.globalsesion.fecha_cierre = DateTime.Parse(genericDefinitions.getDate());
                genericDefinitions.globalsesion.hora_cierre = TimeSpan.Parse(genericDefinitions.getTimeExact());

                if (!this.db.existlogin(genericDefinitions.globalsesion.idlogin))
                {
                    if (!this.db.insertlogin(ref genericDefinitions.globalsesion))
                    {
                        genericDefinitions.dangerInfo("Problema al registrar sesión de usuario, se descarta la operación.");
                        return;
                    };
                }

                if (!this.db.cierrelogin(genericDefinitions.globalsesion))
                {
                    genericDefinitions.dangerInfo("Imposible crear cierre de sesión, consulte al desarrollador");
                }
            }
        }

        
        public virtual void onClose(object sender, FormClosingEventArgs e)
        {
            if (exitFlag == exitState.highest)//ESTATUS DE SALIDA DIRECTO
            {                
                return;
            }
            else
            {
                if (genericDefinitions.closeAccess)//FLAG DE ACCESO A CIERRE
                {
                    if (!genericDefinitions.ifclose())//USUARIO DENIEGA CIERRE
                    {
                        e.Cancel = true;
                        return;
                    }

                    this.cierresesion();
                    
                    try
                    {                        
                        Environment.Exit(0);
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                    }
                }
                else
                {
                    genericDefinitions.error("Acceso de cierre denegado.");
                    e.Cancel = true;
                }
            }            
        }

    }
}
