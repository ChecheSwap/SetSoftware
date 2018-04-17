using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MundoMusical.INVENTARIO;
namespace MundoMusical.PRODUCT
{
    public partial class bajaproducto : altaproducto
    {
        public bajaproducto(inventarioBase IB) :base(IB)
        {
            InitializeComponent();
            this.Size = base.Size;
            this.btnagregar.Text = "Remover de Inventario";
        }

        private void bajaproducto_Load(object sender, EventArgs e)
        {}

        protected override void agregar()
        {
            if (genericDefinitions.yesno("¿Confirma Baja?", "Baja"))
            {
                if (this.db.bajaproducto(this.prod.idproducto, Decimal.ToInt32(this.nupd1.Value)))
                {
                    genericDefinitions.ok("Cantidad Dada de Baja.");
                    this.cargaproducto();
                    this.nupd1.Value = 1;
                }
                else
                {
                    genericDefinitions.error("Error al remover cantidad, Intente de nuevo");
                }
            }
        }
    }
}
