using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MundoMusical.TICKET
{
    public class detallesTicket
    {
        public int cantidad { get; set; }

        public string descripcion { get; set; }

        public double precio { get; set; }        

        public double total { get; set; }

        public detallesTicket(int cantidad, double precio, string desc, double tot)
        {
            this.cantidad = cantidad;
            this.precio = precio;       
            this.descripcion = desc;
            this.total = tot;           
        }

        public detallesTicket()
        {}
    }
}
