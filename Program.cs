using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MundoMusical;
using MundoMusical.CUSTOMER;
using MundoMusical.PRODUCT;
using MundoMusical.CATEGORY;
using MundoMusical.ATOM;
using MundoMusical.PAYMODE;
using MundoMusical.VENTA;
using MundoMusical.CONFIGURACION_DE_IMPRESION;
using MundoMusical.INVENTARIO;
using MundoMusical.XBASE;
using MundoMusical.TIENDA;
using MundoMusical.DB.DUMPS;
using MundoMusical.CONSULTAS.CLIENTES;
using MundoMusical.CONSULTAS;
using MundoMusical.CONSULTAS.VENTAS;
using MundoMusical.TICKET.CRYSTAL_RPT_TICKET.XML_SCHEMA;
using MundoMusical.CONSULTAS.INICIOS_SESION;
using System.Threading;
using System.Globalization;
using MundoMusical.LABELS;

namespace MundoMusical
{
    static class Program
    {
        /// <summary>
        // Code By::JESUS JOSE NAVARRETE BACA 
        /// </summary>
        /// 
        [STAThread]
        static void Main()
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new initForm());
        }
    }
}
