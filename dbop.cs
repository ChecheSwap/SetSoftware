using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MundoMusical.CUSTOM_CONTROLS;
using System.Globalization;
using MundoMusical.TICKET;
using MySql.Data.MySqlClient;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace MundoMusical.DB
{
    public struct xelements
    {
        public List<string> genericStringList;

        public List<producto> listaproductos;
        public string[] clientesNames;

        public string[] categoriaNames;

        public string[] modopagoNames;

        public List<cliente> listcliente;
        public cliente Tcliente { get; set; }
        public factura Tfactura { get; set; }
        public modo_pago Tmodopago { get; set; }
        public categoria Tcategoria { get; set; }
        public producto Tproducto { get; set; }
        public detalle Tdetalle { get; set; }
        public tienda Ttienda { get; set; }
        public usuario Tusuario { get; set; }
        public venta Tventa { get; set; }
        public ticket Tticket { get; set; }
        public configuracion Tconfiguracion { get; set; }

        public List<detallesTicket> xdetallesticket;
        public BindingSource bsource;
        public List<detalle> listdetalle;

        public DataTable tabla;

        public xelements(bool x) : this()
        {
            if (x)
            {
                this.modopagoNames = new string[] { "Id", "Nombre", "Descripción" };
                this.clientesNames = new string[] { "Id", "Nombre", "Apellido", "Calle", "Fecha de nacimiento", "Telefono", "Email", "Colonia", "Ciudad", "C.U.R.P", "R.F.C" };
                this.categoriaNames = new string[] { "Id", "Nombre", "Descripción" };
                this.Setelements();
            }

        }

        public void Setelements()
        {
            this.Tcliente = null;
            this.Tfactura = null;
            this.Tmodopago = null;
            this.Tcategoria = null;
            this.Tproducto = null;
            this.Tdetalle = null;
            this.Ttienda = null;
            this.listcliente = null;
            this.genericStringList = null;
            this.Tusuario = null;
            this.listaproductos = null;
            this.Tventa = null;
            this.xdetallesticket = null;
            this.xdetallesticket = null;
            this.Tticket = null;
            this.Tconfiguracion = null;
            this.bsource = null;
            this.listdetalle = null;
            this.tabla = null;
        }
    }

    public class dbop
    {
        xelements central;
        
        bool flag;
        public dbop()
        {
            this.central = new xelements(true);            
        }

        //->SEC CONFIGURACION

        public string getprinterglobalname()
        {
            this.central.Tconfiguracion = null;

            using (dbmodel db = new dbmodel())
            {
                try
                {
                    var res = from f in db.configuracion select f;

                    if (res.Any())
                    {
                        this.central.Tconfiguracion = res.First();
                    }
                }
                catch (Exception ex)
                {
                    genericDefinitions.error(ex.ToString(), "->getprinterglobalname()");
                }
            }

            if (this.central.Tconfiguracion != null)
            {
                return this.central.Tconfiguracion.globalprintname;
            }
            else
            {
                return null;
            }
        }

        public bool setprinterglobalname(string name)
        {
            this.flag = false;
            using (dbmodel db = new dbmodel())
            {
                try
                {
                    var get = from t in db.configuracion select t;

                    if (get.Any())
                    {
                        this.central.Tconfiguracion = get.First();
                        this.central.Tconfiguracion.globalprintname = name;
                    }
                    else
                    {
                        this.central.Tconfiguracion = new configuracion();
                        this.central.Tconfiguracion.globalprintname = name;
                        db.configuracion.Add(this.central.Tconfiguracion);
                    }

                    db.SaveChanges();
                    this.flag = true;
                }
                catch (Exception ex)
                {
                    genericDefinitions.error(ex.ToString(), "->setglobalprintname(string name)");
                }

            }
            return this.flag;
        }

        public string getprinteretiquetaname()
        {
            this.central.Tconfiguracion = null;

            using (dbmodel db = new dbmodel())
            {
                try
                {
                    var res = from f in db.configuracion select f;

                    if (res.Any())
                    {
                        this.central.Tconfiguracion = res.First();
                    }
                }
                catch (Exception ex)
                {
                    genericDefinitions.error(ex.ToString(), "->getprinteretiquetaname()");
                }
            }

            if (this.central.Tconfiguracion != null)
            {
                return this.central.Tconfiguracion.etiquetaprintname;
            }
            else
            {
                return null;
            }
        }

        public bool setprinteretiquetaname(string name)
        {
            this.flag = false;
            using (dbmodel db = new dbmodel())
            {
                try
                {
                    var get = from t in db.configuracion select t;

                    if (get.Any())
                    {
                        this.central.Tconfiguracion = get.First();
                        this.central.Tconfiguracion.etiquetaprintname = name;
                    }
                    else
                    {
                        this.central.Tconfiguracion = new configuracion();
                        this.central.Tconfiguracion.etiquetaprintname = name;
                        db.configuracion.Add(this.central.Tconfiguracion);
                    }

                    db.SaveChanges();
                    this.flag = true;
                }
                catch (Exception ex)
                {
                    genericDefinitions.error(ex.ToString(), "->setprinteretiquetaname(string name)");
                }

            }
            return this.flag;
        }

        public string getprinterticketname()
        {
            this.central.Tconfiguracion = null;

            using (dbmodel db = new dbmodel())
            {
                try
                {
                    var res = from f in db.configuracion select f;

                    if (res.Any())
                    {
                        this.central.Tconfiguracion = res.First();
                    }
                }
                catch(Exception ex)
                {
                    genericDefinitions.error(ex.ToString(), "->getprinterticketname()");
                }                                
            }

            if (this.central.Tconfiguracion != null)
            {
                return this.central.Tconfiguracion.ticketprintname;
            }
            else
            {
                return null;
            }
        }

        public bool setprinterticketname(string name)
        {
            this.flag = false;
            using (dbmodel db = new dbmodel())
            {
                try
                {
                    var get = from t in db.configuracion select t;

                    if (get.Any())
                    {
                        this.central.Tconfiguracion = get.First();
                        this.central.Tconfiguracion.ticketprintname = name;                        
                    }
                    else
                    {
                        this.central.Tconfiguracion = new configuracion();
                        this.central.Tconfiguracion.ticketprintname = name;
                        db.configuracion.Add(this.central.Tconfiguracion);
                    }

                    db.SaveChanges();
                    this.flag = true;
                }
                catch(Exception ex)
                {
                    genericDefinitions.error(ex.ToString(), "->setticketprintername(string name)");
                }
                
            }
            return this.flag;
        }

        //->SEC DETALLE

        public void filldetalledgv(int idventa, ref DGVCustom dgvcustom)
        {            

            using (dbmodel db = new dbmodel())
            {
                try
                {
                    var res = from t in db.detalles where t.idventa == idventa select new {a = t.pkproducto, e = db.productos.Where(x => x.pkproducto == t.pkproducto).FirstOrDefault().nombre, b = t.cantidad, c=t.precio, d=t.descuento} ;

                    if (res.Any())
                    {
                        dgvcustom.DataSource = res.ToList();
                        dgvcustom.Columns[0].HeaderText = "Clave Primaria Producto";
                        dgvcustom.Columns[1].HeaderText = "Nombre";
                        dgvcustom.Columns[2].HeaderText = "Cantidad";
                        dgvcustom.Columns[3].HeaderText = "Precio";
                        dgvcustom.Columns[4].HeaderText = "Descuento";
                    }
                }
                catch(Exception ex)
                {
                    genericDefinitions.error(ex.ToString(), "getdetalles()");
                }
            }         
        }

        public bool restablecerdetalles(int idventa)
        {
            this.flag = false;

            using (dbmodel db = new dbmodel())
            {
                try
                {
                    var res = from g in db.detalles where g.idventa == idventa select g;

                    if (res.Any())
                    {
                        foreach(detalle x in res)
                        {
                            if(this.getidproducto(x.pkproducto)!= -1)
                            {
                                this.surtirproducto(this.getidproducto(x.pkproducto), x.cantidad);
                            }                            
                        }
                        this.flag = true;                        
                    }
                }
                catch(Exception ex)
                {
                    genericDefinitions.error(ex.ToString(), "->restablecerdetalles(int)");
                }
            }

            return this.flag;
        }

        public DataTable getdetallesticketasTable(int idventa)
        {
            this.central.tabla = new DataTable();

            this.central.tabla.Columns.Add("Cantidad");
            this.central.tabla.Columns.Add("Descripcion");
            this.central.tabla.Columns.Add("Unitario");
            this.central.tabla.Columns.Add("Importe");

            var lista = this.getdetallesticket(idventa);
                        
            foreach(var x in lista)
            {
                DataRow dr = this.central.tabla.NewRow();

                dr[0] = x.cantidad.ToString();
                dr[1] = x.descripcion;
                dr[2] = x.precio.ToString();
                dr[3] = x.total.ToString();

                this.central.tabla.Rows.Add(dr);
            }            
            return this.central.tabla;                    
        }

        public List<detallesTicket> getdetallesticket(int idventa) //USANDO STORED PROCEDURE, METODO ADO.NET
        {
            this.central.xdetallesticket = new List<detallesTicket>();

            using (dbmodel db = new dbmodel())
            {
                using (var command = db.Database.Connection.CreateCommand())
                {
                    var mysqlparam = new MySqlParameter("@scalar", idventa);
                    //command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "call spgetdetallesticket(@scalar);";
                    command.Parameters.Add(mysqlparam);               
                    try
                    {
                        if (!(db.Database.Connection.State == System.Data.ConnectionState.Open))
                        {
                            db.Database.Connection.Open();
                        }
                        using (var res = command.ExecuteReader())
                        {
                            if (res.HasRows)
                            {
                                while (res.Read())
                                {
                                    detallesTicket tmp = new detallesTicket();
                                    tmp.cantidad = int.Parse(res[0].ToString());
                                    tmp.descripcion = res[1].ToString();
                                    tmp.precio = double.Parse(res[2].ToString(), CultureInfo.InvariantCulture);
                                    tmp.total = double.Parse(res[3].ToString(), CultureInfo.InvariantCulture);
                                    this.central.xdetallesticket.Add(tmp);                                                                        
                                }
                            }
                        }
                        db.Database.Connection.Close();
                    }
                    catch (Exception ex)
                    {
                        genericDefinitions.error(ex.ToString(), "->getdetallesticket(int)");
                    }
                }
            }
            return this.central.xdetallesticket;
        }

        /*
        public List<detallesTicket> getdetallesTicket(int idventa) //PARTICIONA LOS DETALLES POR PRIMARY KEYS, POSTERIORMENTE DE DICHAS PARTICIONES SE REALIZA UNA PARTICION POR DESCUENTO PARA CREAR UN NUEVO DETALLE DE TICKET (AGRUPACION MANUAL)
        {
            detalle[] detalles;
            detalle[] tmpdetalles;
            detalle[] detallefinal;

            int[] descuentos;
            int[] pks;

            this.central.xdetallesticket = new List<detallesTicket>();
            detallesTicket tmp;

            try
            {
                using (dbmodel db = new dbmodel())
                {
                    detalles = db.detalles.Where(x => x.idventa == idventa).ToArray();
                }

                pks = detalles.Select(x => x.pkproducto).Distinct().ToArray();//SELECCIONO LAS PRIMARY KEYS DIFERENTES

                foreach (int pk in pks)
                {
                    tmpdetalles = detalles.Where(x => x.pkproducto == pk).ToArray(); //SELECCIONO TODOS LOS DETALLES CON EL MISMO PRIMARY KEY

                    descuentos = tmpdetalles.Select(x => x.descuento).Distinct().ToArray(); // OBTENGO LOS DESCUENTOS DE LOS DETALLES CON MISMO PRIMARY KEY

                    foreach (int desc in descuentos)
                    {
                        detallefinal = tmpdetalles.Where(x => x.descuento == desc).ToArray();//DETALLES CON MISMO PRIMARY KEY Y MISMO DESCUENTO, NUEVO DETALLE

                        tmp = new detallesTicket();
                        tmp.cantidad = detallefinal.Sum(x => x.cantidad);
                        tmp.precio = detallefinal[0].precio;

                        using (dbmodel db = new dbmodel())
                        {
                            tmp.descripcion = db.productos.Where(x => x.pkproducto == pk).First().nombre;
                        }

                        tmp.total = tmp.cantidad * tmp.precio - ((tmp.precio / 100) * desc);

                        this.central.xdetallesticket.Add(tmp);
                    }
                }

            }
            catch (Exception ex)
            {
                genericDefinitions.error(ex.ToString(), "Error procedimiento 'getdetallesTicket(int&)'");
            }

            return this.central.xdetallesticket;
        }
        */

        public bool insertarDetalles(DGVCustom grilla,int venta)
        {
            flag = true;

            using(dbmodel db = new dbmodel())
            {
                for(int x=0; x<grilla.RowCount; ++x)
                {
                    if (!flag)
                    {
                        break;
                    }
                    this.central.Tdetalle = new detalle();
                    this.central.Tdetalle.pkproducto = this.getpkproducto(int.Parse(grilla.Rows[x].Cells[0].Value.ToString()));
                    this.central.Tdetalle.idventa = venta;
                    this.central.Tdetalle.cantidad = int.Parse(grilla.Rows[x].Cells[2].Value.ToString());
                    this.central.Tdetalle.precio = Double.Parse(grilla.Rows[x].Cells[3].Value.ToString(), CultureInfo.InvariantCulture);
                    this.central.Tdetalle.descuento = int.Parse(grilla.Rows[x].Cells[4].Value.ToString());

                    try
                    {
                        this.central.Tproducto = db.productos.SingleOrDefault(obj => obj.pkproducto == this.central.Tdetalle.pkproducto);
                        this.central.Tproducto.stock -= this.central.Tdetalle.cantidad;
                        db.SaveChanges();                        

                        db.detalles.Add(this.central.Tdetalle);
                        db.SaveChanges();                        
                    }
                    catch(Exception ex)
                    {
                        genericDefinitions.error(ex.ToString(), "Error Data Base");                        
                        flag = false;
                        break;
                    }
                }
            }
            return flag;
        }

        //->SEC VENTA 

        public int getIdventa(int ticket)
        {
            int res = -1;

            using (dbmodel db = new dbmodel())
            {
                try
                {
                    var gets = from t in db.tickets where t.id == ticket select t.idventa;

                    if (gets.Any())
                    {
                        res = gets.First();
                    }
                }
                catch (Exception ex)
                {
                    genericDefinitions.error(ex.ToString());
                }               
            }

            return res;
        }
        private void setcolumnsdgvconsultasventas(ref DGVCustom dgv)
        {
            dgv.Columns[0].HeaderText = "No. Venta";
            dgv.Columns[1].HeaderText = "Fecha";
            dgv.Columns[2].HeaderText = "No. Articulos";
            dgv.Columns[3].HeaderText = "Modo Pago";
            dgv.Columns[4].HeaderText = "Total";
            dgv.Columns[5].HeaderText = "Status";
            dgv.Columns[6].HeaderText = "Id Cliente";

           // dgv.Columns[1].ValueType = typeof(string); //CONVIERTE EL DATETIME A STRING PARA VISUALIZAR CORRECTAMENTE EN EL REPORTE RPT
        }

        public bool getventasdgvconsultas(ref DGVCustom dgv)
        {
            this.flag = false;

            try
            {
                using (dbmodel db = new dbmodel())
                {
                    dgv.DataSource = db.ventas.Select(x => new{
                        IDVENTA = x.idventa.ToString(),
                        FECHA = x.fecha,
                        ARTICULOS = x.articulos.ToString(),
                        MODOPAGOO = (from t in db.modo_pago where t.idmodopago == x.idmodopago select t.nombre).FirstOrDefault(),
                        TOTAL = "$" + x.total.ToString(),
                        STATUS = x.status,
                        IDCLIENTE = x.idcliente
                    }).ToList();
                }

                this.setcolumnsdgvconsultasventas(ref dgv);
                this.flag = true;
            }
            catch(Exception ex)
            {
                genericDefinitions.error(ex.ToString());
            }

            return this.flag;
        }
        public bool getventasdgvconsultas(ref DGVCustom dgv, string xcase, string value)
        {
            this.flag = false;
            try
            {
                using (dbmodel db =  new dbmodel())
                {
                    switch (xcase)
                    {
                        case ("idventa"):
                            {
                                var res = from t in db.ventas where t.idventa.ToString() == value select t;

                                if (res.Any())
                                {
                                    dgv.DataSource = db.ventas.Where(x => x.idventa.ToString() == value).Select(x => new
                                    {
                                        IDVENTA = x.idventa.ToString(),
                                        FECHA = x.fecha,
                                        ARTICULOS = x.articulos.ToString(),
                                        MODOPAGOO = (from t in db.modo_pago where t.idmodopago == x.idmodopago select t.nombre).FirstOrDefault(),
                                        TOTAL = "$" + x.total.ToString(),
                                        STATUS = x.status,
                                        IDCLIENTE = x.idcliente
                                    }).ToList();
                                }
                                else
                                {
                                    dgv.DataSource = db.ventas.Where(x => x.idventa.ToString().Contains(value)).Select(x => new
                                    {
                                        IDVENTA = x.idventa.ToString(),
                                        FECHA = x.fecha,
                                        ARTICULOS = x.articulos.ToString(),
                                        MODOPAGOO = (from t in db.modo_pago where t.idmodopago == x.idmodopago select t.nombre).FirstOrDefault(),
                                        TOTAL = "$" + x.total.ToString(),
                                        STATUS = x.status,
                                        IDCLIENTE = x.idcliente
                                    }).ToList();
                                }

                                break;
                            }
                        case ("fecha"):
                            {
                                
                                var xvalue = DateTime.Parse(value);

                                dgv.DataSource = db.ventas.Where(x => x.fecha.ToString() == xvalue.ToString()).Select(x => new {
                                    IDVENTA = x.idventa.ToString(),
                                    FECHA = x.fecha,
                                    ARTICULOS = x.articulos.ToString(),
                                    MODOPAGOO = (from t in db.modo_pago where t.idmodopago == x.idmodopago select t.nombre).FirstOrDefault(),
                                    TOTAL = "$" + x.total.ToString(),
                                    STATUS = x.status,
                                    IDCLIENTE = x.idcliente
                                }).ToList();
                                break;
                            }
                        case ("status"):
                            {
                                dgv.DataSource = db.ventas.Where(x => x.status.Trim() == value).Select(x => new {
                                    IDVENTA = x.idventa.ToString(),
                                    FECHA = x.fecha,
                                    ARTICULOS = x.articulos.ToString(),
                                    MODOPAGOO = (from t in db.modo_pago where t.idmodopago == x.idmodopago select t.nombre).FirstOrDefault(),
                                    TOTAL = "$" + x.total.ToString(),
                                    STATUS = x.status,
                                    IDCLIENTE = x.idcliente
                                }).ToList();
                                break;
                            }
                        case ("idcliente"):
                            {                                
                                dgv.DataSource = db.ventas.Where(x => x.idcliente.ToString() == value).Select(x => new {
                                    IDVENTA = x.idventa.ToString(),
                                    FECHA = x.fecha,
                                    ARTICULOS = x.articulos.ToString(),
                                    MODOPAGOO = (from t in db.modo_pago where t.idmodopago == x.idmodopago select t.nombre).FirstOrDefault(),
                                    TOTAL = "$" + x.total.ToString(),
                                    STATUS = x.status,
                                    IDCLIENTE = x.idcliente
                                }).ToList();
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }

                this.setcolumnsdgvconsultasventas(ref dgv);
                this.flag = true;
            }
            catch(Exception ex)
            {
                genericDefinitions.error(ex.ToString());
            }

            return this.flag;
        }
        public bool existventa(int numero)
        {
            this.flag = false;

            using (dbmodel db = new dbmodel())
            {
                try
                {
                    var res = from r in db.ventas where r.idventa == numero select r;
                    if (res.Any())
                    {
                        this.flag = true;
                    }
                }
                catch(Exception ex)
                {
                    genericDefinitions.error(ex.ToString(), "existventa(int)");
                }
            }
            return this.flag;            
        }

        public bool cancelarventa(int id)
        {
            this.flag = false;
            using (dbmodel db = new dbmodel())
            {
                try
                {
                    var res = from t in db.ventas where t.idventa == id select t;
                    if (res.Any())
                    {
                        res.First().status = statusVenta.CANCELADA;
                        db.SaveChanges();
                        this.flag = true;
                    }
                }
                catch (Exception ex)
                {
                    genericDefinitions.error(ex.ToString(), "->cancelarventa(int)");
                }
            }
            return this.flag;
        }
        
        public bool insertarVenta(venta ventaactual)
        {
            flag = false;
            using (dbmodel db = new dbmodel()){
                try
                {
                    db.ventas.Add(ventaactual);
                    db.SaveChanges();
                    flag = true;
                }
                catch(Exception ex)
                {
                    genericDefinitions.error(ex.ToString(), "Error Managment Data Base");
                }
            }
            return flag;
        }

        public int getnumerodeventaactual()
        {
            int id = -1;
            using(dbmodel db = new dbmodel())
            {
                var res = from t in db.ventas select t;

                if (res.Any())
                {
                    try
                    {
                        id = res.OrderByDescending(x => x.idventa).First().idventa;
                    }
                    catch(Exception ex)
                    {
                        genericDefinitions.ok(ex.ToString());
                    }                    
                }
            }
            
            return ++id;
        }
       
        public venta getventa(int id)
        {
            this.central.Tventa = null;
            using (dbmodel db = new dbmodel())
            {
                try
                {
                    this.central.Tventa = db.ventas.FirstOrDefault(x => x.idventa == id);
                }
                catch(Exception ex)
                {
                    genericDefinitions.error(ex.ToString(), "Error: getventa(int)");
                }
                
            }
            return this.central.Tventa;
        }

        public double getiva(double monto)
        {
            return (monto/100) * 16;
        }
               
        //->SEC MODO DE PAGO

        public List<string> getModoPagosNombres()
        {
            this.central.genericStringList = new List<string>();

            using (dbmodel db = new dbmodel())
            {
                var f = from x in db.modo_pago select new {Nombre = x.nombre};
                if (f.Any())
                {                    
                    foreach(var t in f)
                    {
                        this.central.genericStringList.Add(t.Nombre);
                    }
                }
            }

            return this.central.genericStringList;
        }
        
        public bool getcustommodopagoid(ref DGVCustom gv, string comodin)//MODO DE PAGO LIKE CUSTOM POR ID
        {
            bool flag = false;

            int n =-1;

            Int32.TryParse(comodin, out n);

            using (dbmodel db = new dbmodel())
            {
                var res = from y in db.modo_pago where y.idmodopago.ToString().Contains(comodin) || y.idmodopago == n select y;
                if (res.Any())
                {
                    gv.DataSource = res.ToList();
                    gv.Columns.RemoveAt(gv.ColumnCount - 1);
                    for (int x = 0; x < gv.ColumnCount; ++x)
                    {
                        gv.Columns[x].HeaderText = this.central.modopagoNames[x];
                    }
                    flag = true;
                }
            }

            return flag;
        }
        public bool getcustommodopagodes(ref DGVCustom gv, string comodin) //MODO DE PAGO LIKE CUSTOM POR OTROS DETALLES
        {
            bool flag = false;

            using (dbmodel db = new dbmodel())
            {
                var res = from y in db.modo_pago where y.otros_detalles.Contains(comodin) select y;
                if (res.Any())
                {
                    gv.DataSource = res.ToList();
                    gv.Columns.RemoveAt(gv.ColumnCount - 1);
                    for (int x = 0; x < gv.ColumnCount; ++x)
                    {
                        gv.Columns[x].HeaderText = this.central.modopagoNames[x];
                    }
                    flag = true;
                }
            }

            return flag;
        }

        public bool getcustommodopagonombre(ref DGVCustom gv, string comodin)
        {
            bool flag = false;

            using(dbmodel db = new dbmodel())
            {
                var res = from y in db.modo_pago where y.nombre.Contains(comodin) select y;
                if (res.Any())
                {
                    gv.DataSource = res.ToList();
                    gv.Columns.RemoveAt(gv.ColumnCount - 1);
                    for(int x=0; x<gv.ColumnCount; ++x)
                    {
                        gv.Columns[x].HeaderText = this.central.modopagoNames[x];
                    }
                    flag = true;
                }
            }

            return flag;
        }

        public bool getmodopagoDGV(ref DGVCustom gv)
        {
            bool flag = false;

            using (dbmodel db = new dbmodel())
            {
                if (db.modo_pago.Any())
                {
                    try
                    {
                        gv.DataSource = db.modo_pago.ToList();
                        gv.Columns.RemoveAt(gv.Columns.Count - 1);
                        for(int x=0; x<gv.Columns.Count; ++x)
                        {
                            gv.Columns[x].HeaderText = this.central.modopagoNames[x];
                        }
                        flag = true;
                    }
                    catch(Exception ex)
                    {
                        genericDefinitions.error(ex.ToString(), "Error");
                    }
                }
            }
            return flag;

        }

        public bool deletemodopago(int id)
        {
            bool ok = false;

            using(dbmodel db = new dbmodel())
            {
                try
                {
                    this.central.Tmodopago = db.modo_pago.Where(x => x.idmodopago == id).First();
                    db.modo_pago.Remove(this.central.Tmodopago);
                    db.SaveChanges();
                    ok = true;                    
                }
                catch(Exception ex)
                {
                    genericDefinitions.error(ex.ToString(), "Error");
                }
                finally
                {
                    this.central.Tmodopago = null;
                }                

            }

            return ok;
        }
        public bool modopagoinventas(int id)
        {
            bool ok = false;

            using(dbmodel db= new dbmodel())
            {
                var res = from t in db.ventas where t.idmodopago == id select t;
                if (res.Any())
                    ok = true;
            }

            return ok;
        }
        public int getidmodopago(string name)
        {
            int res = -1;

            using (dbmodel db= new dbmodel())
            {
                try
                {
                    res = db.modo_pago.Where(x => x.nombre == name).FirstOrDefault().idmodopago;
                }
                catch(Exception ex)
                {
                    genericDefinitions.error(ex.ToString(), "Error");
                }
                
            }

            return res;
        }

        public bool updatemodopago(modo_pago modo)
        {
            bool flag = false;

            using (dbmodel db = new dbmodel())
            {
                try
                {
                    this.central.Tmodopago = db.modo_pago.Where(x => x.idmodopago == modo.idmodopago).FirstOrDefault();
                    this.central.Tmodopago.nombre = modo.nombre;
                    this.central.Tmodopago.otros_detalles = modo.otros_detalles;
                    db.SaveChanges();
                    flag = true;
                }catch(Exception ex)
                {
                    genericDefinitions.error(ex.ToString(), "Error");
                }
            }
            return flag;
        }
        
        public modo_pago getmodopago(string name)
        {
            using (dbmodel db = new dbmodel())
            {
                this.central.Tmodopago = null;

                try
                {
                    this.central.Tmodopago = db.modo_pago.Where(x => x.nombre == name).FirstOrDefault();
                }
                catch(Exception ex)
                {
                    genericDefinitions.error(ex.ToString(), "Error");
                }
                
            }
            return this.central.Tmodopago;
        }
        public bool insertmodopago(modo_pago modo)
        {
            bool flag = false;

            using (dbmodel db = new dbmodel())
            {
                try
                {
                    db.modo_pago.Add(modo);
                    db.SaveChanges();
                    flag = true;

                }catch(Exception ex)
                {
                    genericDefinitions.error(ex.ToString(), "Error");
                }
            }

            return flag;
        }

        public bool existmodopago(string name)
        {
            bool flag = false;
            using (dbmodel db= new dbmodel())
            {
                var res = from t in db.modo_pago where t.nombre == name select t;
                if (res.Any())
                {
                    flag = true;
                }
            }
            return flag;
        }
        //->SEC CATEGORIA

        public bool getcustomcatnumero(ref DGVCustom gv, string comodin)
        {
            bool flag = false;            

            using (dbmodel db = new dbmodel())
            {
                int localnum = -1;

                Int32.TryParse(comodin, out localnum);

                var res = from x in db.categorias where x.idcategoria == localnum || x.idcategoria.ToString().Contains(comodin) select x;

                if (res.Any())
                {                    
                    gv.DataSource = res.ToList();
                    gv.Columns.RemoveAt(gv.ColumnCount-1);


                    for(int g=0; g<gv.ColumnCount; ++g)
                    {
                        gv.Columns[g].HeaderText = this.central.categoriaNames[g];
                    }
                }
            }

            return flag;
        }

        public bool getcustomcatdescripcion(ref DGVCustom gv, string comodin)
        {
            bool flag = false;

            using (dbmodel db = new dbmodel())
            {
                var res = from t in db.categorias where t.descripcion.Contains(comodin) select t;
                if (res.Any())
                {
                    flag = true;
                    gv.DataSource = res.ToList();
                    gv.Columns.RemoveAt(gv.ColumnCount-1); 

                    for(int x=0; x<gv.ColumnCount; ++x)
                    {
                        gv.Columns[x].HeaderText = this.central.categoriaNames[x];
                    }

                }
            }

            return flag;
        }

        public bool getcustomcatName(ref DGVCustom gv, string comodin)
        {
            bool flag = false;
            using (dbmodel db = new dbmodel())
            {
                var res = from t in db.categorias where t.nombre.Contains(comodin) select t;

                if (res.Any())
                {
                    flag = true;
                    gv.DataSource = res.ToList();
                    gv.Columns.RemoveAt(gv.ColumnCount - 1);

                    int i = 0;

                    foreach (DataGridViewColumn x in gv.Columns)
                    {
                        x.HeaderText = this.central.categoriaNames.ElementAt(i);
                        ++i;
                    }
                }
            }

            return flag;
        }
        public bool getallcategoriesDGV(ref DGVCustom asx)
        {
            bool flag = false;
            using (dbmodel db = new dbmodel())
            {
                try
                {
                    asx.DataSource = db.categorias.Select(x => new { Id = x.idcategoria, Nombre = x.nombre, Descripcion = x.descripcion }).ToList();
                    flag = true;
                }
                catch(Exception ex)
                {
                    genericDefinitions.error(ex.ToString(), "Error");
                }
            }
            return flag;
        }
        public bool existcategoriaproductos(int id)
        {
            bool flag = false;

            using (dbmodel db= new dbmodel())
            {
                var t = from g in db.productos where g.idcategoria == id select g;
                if (t.Any())
                {
                    flag = true;
                }
            }
            return flag;
        }
        public bool deletecategoria(int id)
        {
            bool flag = false;
            using (dbmodel db = new dbmodel())
            {
                var t = from g in db.categorias where g.idcategoria == id select g;
                if (t.Any())
                {
                    try
                    {
                        db.categorias.Remove(t.First());
                        db.SaveChanges();
                        flag = true;
                    }
                    catch (Exception ex)
                    {
                        genericDefinitions.error(ex.ToString(), "Error");
                    }
                }
            }
            return flag;
        }
        public int getcategoriaid(string name)
        {
            int result = -1;
            using (dbmodel db = new dbmodel())
            {
                var res = from t in db.categorias where t.nombre == name select t;

                if (res.Any())
                {
                    result = res.First().idcategoria;
                }
            }
            return result;
        }
        public categoria getcategoria(int idcategoria)
        {
            this.central.Tcategoria = null;

            using (dbmodel sb = new dbmodel())
            {
                try
                {
                    this.central.Tcategoria = new categoria();
                    this.central.Tcategoria = sb.categorias.Where(x => x.idcategoria == idcategoria).FirstOrDefault();
                }
                catch(Exception ex)
                {
                    genericDefinitions.error(ex.ToString(), "Error");
                }
                
            }

            return this.central.Tcategoria;
        }

        public bool addcategoria(ref categoria cat)
        {
            bool flag = false;

            using (dbmodel db = new dbmodel())
            {
                try
                {
                    db.categorias.Add(cat);
                    db.SaveChanges();
                    flag = true;
                }
                catch(Exception ex)
                {
                    genericDefinitions.error(ex.ToString(), "Error al Insertar Categoria");
                }
                
            }

            return flag;
        }

        public bool existcategoria(string name)
        {
            bool flag = false;

            using (dbmodel db = new dbmodel())
            {
                var res = from t in db.categorias where t.nombre == name select t;
                if (res.Any())
                    flag = true;

            }
            return flag;
        }

        public bool updatecategoria(categoria cat)
        {
            bool flag = false;

            using (dbmodel db = new dbmodel())
            {
                var dec = from t in db.categorias where t.idcategoria == cat.idcategoria select t;

                if (dec.Any())
                {
                    dec.First().nombre = cat.nombre;
                    dec.First().descripcion = cat.descripcion;
                    db.SaveChanges();
                    flag = true;
                }
            }

            return flag;
        }

        //->SEC PRODUCTOS


        public List<int> getallProductId()
        {
            List<int> send = null;

            try
            {
                using (dbmodel db = new dbmodel())
                {
                    var res = from t in db.productos group t by t.idproducto into g select g;

                    if (res.Any())
                    {
                        send = new List<int>();
                        
                        foreach(var x in res)
                        {
                            send.Add(x.Key);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                genericDefinitions.error(ex.ToString());
            }

            return send;
        }
        public int getProductosCount()
        {
            int tot = 0;

            try
            {
                using (dbmodel db = new dbmodel())
                {
                    var res = from t in db.productos group t by t.idproducto into g select g;

                    if (res.Any())
                    {
                        tot = res.Count();
                    }
                }

            }
            catch(Exception ex)
            {
                genericDefinitions.error(ex.ToString());
            }

            return tot;
        }
        private void titlesdgvproductos(ref DGVCustom dgv)
        {
            dgv.Columns[0].HeaderText = "Id Producto";
            dgv.Columns[1].HeaderText = "Nombre";
            dgv.Columns[2].HeaderText = "Precio Base";
            dgv.Columns[3].HeaderText = "Precio Venta";
            dgv.Columns[4].HeaderText = "Stock";
        }

        public bool getallproductosDGV(ref DGVCustom dgv)
        {
            this.flag = false;

            using (dbmodel db = new dbmodel())
            {
                try
                {
                    dgv.DataSource = db.productos.Select(x => new {
                        IDPRODUCTO = x.idproducto,
                        NOMBRE = x.nombre,
                        PRECIOB = "$" + x.preciocosto.ToString(),
                        PRECIO = "$ "+ x.precio.ToString(),
                        STOCK = x.stock,
                    }).ToList();

                    this.titlesdgvproductos(ref dgv);

                    this.flag = true;

                }
                catch(Exception ex)
                {
                    genericDefinitions.error(ex.ToString());
                }
            }

            return this.flag;
        }

        public bool getallproductosDGV(DGVCustom dgv, string alias, string type)
        {
            this.flag = false;

            using (dbmodel db = new dbmodel())
            {
                try
                {
                    string clase = type.ToUpper();
                    
                    switch(clase)
                    {
                        case ("ID"):
                            {
                                dgv.DataSource = db.productos.Where(x => x.idproducto.ToString().Equals(alias))
                                   .Select(x => new
                                   {
                                       IDPRODUCTO = x.idproducto,
                                       NOMBRE = x.nombre,
                                       PRECIOB = "$" + x.preciocosto.ToString(),
                                       PRECIO = "$ " + x.precio.ToString(),
                                       STOCK = x.stock,
                                   }).ToList();
                                
                                this.flag = true;
                                break;
                            }
                        case ("NOMBRE"):
                            {
                                dgv.DataSource = db.productos.Where(x => x.nombre.Contains(alias))
                                   .Select(x => new
                                   {
                                       IDPRODUCTO = x.idproducto,
                                       NOMBRE = x.nombre,
                                       PRECIOB = "$" + x.preciocosto.ToString(),
                                       PRECIO = "$ " + x.precio.ToString(),
                                       STOCK = x.stock,
                                   }).ToList();
                                
                                this.flag = true;
                                break;                                 
                            }
                        case ("CATEGORIA"):
                            {
                                
                                var res = from t in db.categorias where t.nombre.ToUpper() == alias.ToUpper() select t.idcategoria;
                                dgv.DataSource = db.productos.Where(x => x.idcategoria.ToString() == res.FirstOrDefault().ToString())
                                   .Select(x => new
                                   {
                                       IDPRODUCTO = x.idproducto,
                                       NOMBRE = x.nombre,
                                       PRECIOB = "$" + x.preciocosto.ToString(),
                                       PRECIO = "$ " + x.precio.ToString(),
                                       STOCK = x.stock,
                                   }).ToList();
                                
                                this.flag = true;
                                break;
                            }
                            
                        default:
                            break;
                    }

                    this.titlesdgvproductos(ref dgv);
                }
                catch (Exception ex)
                {
                    genericDefinitions.error(ex.ToString());
                }
            }

            return this.flag;
        }

        public bool bajaproducto(int id, int cantidad)
        {
            using (dbmodel db = new dbmodel())
            {
                this.flag = false;

                try
                {
                    var res = from t in db.productos where t.idproducto == id select t;

                    if (res.Any())
                    {
                        if (res.First().stock >= cantidad)
                        {
                            res.First().stock -= cantidad;
                            db.SaveChanges();
                            this.flag = true;
                        }
                    }
                }
                catch(Exception ex)
                {
                    genericDefinitions.error(ex.ToString());
                }
            }

            return this.flag;
        }
        public List<producto> getallProductos()
        {
            List<producto> send = null;

            using (dbmodel db = new dbmodel())
            {
                var res = from t in db.productos select t;

                if (res.Any())
                {
                    send = res.ToList();
                }
            }

            return send;
        }
        public int getidproducto(int pk)
        {
            int id = -1;

            using (dbmodel db = new dbmodel())
            {
                try
                {
                    var res = from t in db.productos where t.pkproducto == pk select t;

                    if (res.Any())
                    {
                        id = res.First().idproducto;
                    }
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }

            return id;

        }

        public bool surtirproducto(int idproducto, int cantidad)
        {
            this.flag = false;

            using (dbmodel db = new dbmodel())
            {
                try
                {
                    var res = from t in db.productos where t.idproducto == idproducto select t;
                    if (res.Any())
                    {
                        res.First().stock += cantidad;
                        db.SaveChanges();
                        this.flag = true;
                    }
                }
                catch (Exception ex)
                {
                    genericDefinitions.error(ex.ToString(), "->surtirproducto(int,int)");
                }

            }
            return this.flag;
        }

        public List<producto> getinventarioproductos()
        {
            using (dbmodel db = new dbmodel())
            {
                try
                {
                    var res = from t in db.productos select t;
                        
                    if (res.Any())
                    {
                        this.central.listaproductos = res.ToList();
                    }
                    
                }
                catch(Exception ex)
                {
                    genericDefinitions.error(ex.ToString(), "getiventarioproductos()");
                }
            }
            return this.central.listaproductos;
        }
        public int getpkproducto(int idproducto)
        {
            int pk=-1;

            using (dbmodel db = new dbmodel())
            {
                var res = db.productos.Where(x => x.idproducto == idproducto).SingleOrDefault();

                if (!(res == null))
                {
                    pk = res.pkproducto;
                }
            }
            return pk;
        }
        public int maxcantidadproducto(int id) //CANTIDAD DE PRODUCTO EN STOCK
        {
            int c = 0;
            using (dbmodel db = new dbmodel())
            {
                var res = from t in db.productos where t.idproducto == id select new { cantidad = t.stock };
                if (res.Any())
                {
                    c = res.First().cantidad;
                }
            }
            return c;
        }
        public bool productisondetalles(int id) //PRODUCTO EN DETALLES
        {
            int pk = this.getpkproducto(id);

            bool flag = false;

            using (dbmodel db = new dbmodel())
            {
                var t = from g in db.detalles where g.pkproducto == pk select g;

                if (t.Any())
                   flag = true;
            }

            return flag;
        }
        public bool deleteproduct(producto prod)
        {
            bool flag = false;

            using (dbmodel db = new dbmodel())
            {
                var res = from t in db.productos where t.idproducto == prod.idproducto select t;
                if (res.Any())
                {
                    try
                    {
                        db.productos.Remove(res.First());
                        db.SaveChanges();
                        flag = true;

                    }catch(Exception ex)
                    {
                        genericDefinitions.error(ex.ToString(), "Error en Operacion");                        

                    }
                }
            }

            return flag;
        }

        public bool updateproducto(producto prod)
        {

            bool flag = false;

            using (dbmodel db = new dbmodel())
            {
                this.central.Tproducto = db.productos.Where(x => x.idproducto == prod.idproducto).FirstOrDefault();

                if (this.central.Tproducto != null)
                {
                    this.central.Tproducto.nombre = prod.nombre;
                    this.central.Tproducto.precio = prod.precio;
                    this.central.Tproducto.stock = prod.stock;
                    this.central.Tproducto.idcategoria = prod.idcategoria;
                    this.central.Tproducto.preciocosto = prod.preciocosto;

                    try
                    {
                        db.SaveChanges();
                        flag = true;

                    }catch(Exception ex)
                    {
                        genericDefinitions.error(ex.ToString(),"Error");
                    }
                    finally
                    {
                        this.central.Tproducto = null;
                    }
                    
                }
            }

            return flag;
        }

        public producto getdataproduct(int id)
        {
            using (dbmodel db = new dbmodel())
            {
                var type = from dt in db.productos where dt.idproducto == id select dt;

                if (type.Any())
                {
                    this.central.Tproducto = type.FirstOrDefault();
                }
                else
                {
                    this.central.Tproducto = null;
                }
                
            }

            return this.central.Tproducto;
                 
        }
        public bool existproduct(int id)
        {
            bool flag = false;

            using (dbmodel db = new dbmodel())
            {
                var loc = from k in db.productos where k.idproducto == id select k;

                if (loc.Any())
                {
                    flag = true;
                }

            }

           return flag;
        }
        public bool deleteproduct(int id)
        {

            bool flag = false;

            using (dbmodel db = new dbmodel())
            {
                var loc = from x in db.productos where x.idproducto == id select x;                

                if (loc.Any())
                {
                    try
                    {
                        db.productos.Remove(loc.FirstOrDefault());
                        flag = true;

                    }catch(Exception ex)
                    {
                        genericDefinitions.error(ex.ToString(), "Error Delete");
                    }
                }
            }
            
            return flag;
        }
        public bool insertproduct(producto cat)
        {
            bool flag = false;
            using (dbmodel db = new dbmodel())
            {
                db.productos.Add(cat);

                try
                {
                    db.SaveChanges();                    
                    flag = true;
                }
                catch (Exception ex)
                {
                    genericDefinitions.error(ex.ToString(), "Error");

                    throw ex;
                }
                
            }
            return flag;
        }

        public int getcurrentidproductos()
        {
            int id = 100;

            using (dbmodel db= new dbmodel())
            {
                var occ = from t in db.productos select t;

                if (occ.Any())
                {
                    id = occ.OrderByDescending(x => x.pkproducto).FirstOrDefault().pkproducto;
                    ++id;
                    while (!(this.isavailableIDproducto(id))){
                        ++id;
                    }                        
                }                    
            }
            return id;
        }

        public bool isavailableIDproducto(int id)
        {
            bool flag = true;

            using (dbmodel db = new dbmodel())
            {
                var t = from g in db.productos where g.idproducto == id select g;
                if (t.Any())
                    flag = false;
            }
            return flag;
        }
        //->SEC CATEGORIAS

        public string getcategorianame(int id)
        {
            string name = null;

            using (dbmodel db = new dbmodel())
            {
                var catname = from t in db.categorias where t.idcategoria == id select t;
                if (catname.Any())
                {
                    name = catname.First().nombre;
                }
            }
            return name;
        }

        public List<categoria> getCategorias()
        {
            List<categoria> categorias = null;
            try
            {               
                using (dbmodel t = new dbmodel())
                {
                    var cat = from x in t.categorias select x;

                    if (cat.Any())
                        categorias = cat.ToList();

                    cat = null;
                }
            }
            catch(Exception ex)
            {
                genericDefinitions.error(ex.ToString());
            }

            return categorias;
        }

        public int getidcategoria(string name)
        {
            int xid = -1;
            using (dbmodel db = new dbmodel())
            {
                var id = from a in db.categorias where a.nombre == name select a;
                if (id.Any())
                    xid = id.FirstOrDefault().idcategoria;

                id = null;
            }
            return xid;
        }

        //->SEC CLIENTES
        
        private void dgvclientesTitles(ref DGVCustom dgv)
        {
            dgv.Columns[0].HeaderText = "Id";
            dgv.Columns[1].HeaderText = "Nombre";
            dgv.Columns[2].HeaderText = "Dirección";
            dgv.Columns[3].HeaderText = "Ciudad/Estado";
            dgv.Columns[4].HeaderText = "Código postal";
            dgv.Columns[5].HeaderText = "Fecha de Nacimiento";
            dgv.Columns[6].HeaderText = "Numero de contacto";
            dgv.Columns[7].HeaderText = "Email";
            dgv.Columns[8].HeaderText = "C.U.R.P.";
            dgv.Columns[9].HeaderText = "R.F.C.";
        }

        public bool getclientesventasdgvconsultas(ref DGVCustom dgv, string [] lista)
        {
            this.flag = false;

            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            try
            {
                using (dbmodel db = new dbmodel())
                {
                    var resquery = (from xcliente in db.clientes
                                    join xventa in db.ventas
                                    on xcliente.id_cliente equals xventa.idcliente
                                    where lista.Contains(xcliente.id_cliente.ToString())
                                    select new
                                    {
                                        IDCLIENTE = xcliente.id_cliente.ToString(),
                                        NOMBRECLIENTE = xcliente.nombre + " " + xcliente.apellido,
                                        IDVENTA = xventa.idventa.ToString(),
                                        FECHAVENTA = xventa.fecha,
                                        MONTO = "$" + xventa.total.ToString()
                                    }).ToList();

                    dgv.DataSource = resquery;

                    dgv.Columns[0].HeaderText = "ID Cliente";
                    dgv.Columns[1].HeaderText = "Nombre Cliente";
                    dgv.Columns[2].HeaderText = "ID Venta";
                    dgv.Columns[3].HeaderText = "Fecha Venta";
                    dgv.Columns[4].HeaderText = "Monto venta";

                    this.flag = true;
                }
            }
            catch(Exception ex)
            {
                genericDefinitions.error(ex.ToString());
            }

            return this.flag;
        }

        public bool getclientesdgvconsultas(ref DGVCustom dgv, string incidencia, string caso)
        {
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            using (dbmodel db = new dbmodel())
            {
                try
                {
                    switch (caso)
                    {
                        case ("idcliente"):
                            {
                                var res = from t in db.clientes where t.id_cliente.ToString() == incidencia select t;

                                if (!res.Any())
                                {
                                    dgv.DataSource = db.clientes.Where(x => x.id_cliente.ToString().Contains(incidencia)).Select(x => new
                                    {
                                        IDCLIENTE = x.id_cliente,
                                        NOMBRE = x.nombre + " " + x.apellido,
                                        DIRECCION = x.calle + " " + x.colonia + " ",
                                        CIUDADEDO = x.ciudad + " " + x.estado,
                                        CP = x.cp,
                                        FNAC = x.fecha_nacimiento,
                                        CONTACTO = x.telefono,
                                        EMAIL = x.email,
                                        CURP = x.curp,
                                        RFC = x.rfc

                                    }).ToList();
                                }
                                else
                                {
                                    dgv.DataSource = db.clientes.Where(x => x.id_cliente.ToString() == incidencia).Select(x => new
                                    {
                                        IDCLIENTE = x.id_cliente,
                                        NOMBRE = x.nombre + " " + x.apellido,
                                        DIRECCION = x.calle + " " + x.colonia + " ",
                                        CIUDADEDO = x.ciudad + " " + x.estado,
                                        CP = x.cp,
                                        FNAC = x.fecha_nacimiento,
                                        CONTACTO = x.telefono,
                                        EMAIL = x.email,
                                        CURP = x.curp,
                                        RFC = x.rfc
                                    }).ToList();
                                }

                                break;
                            }
                        case ("nombreapellido"):
                            {
                                
                                incidencia = incidencia.ToUpper().Trim();

                                using (var command = db.Database.Connection.CreateCommand())
                                {
                                    var mysqlparam = new MySqlParameter("@scalar", incidencia);
                                    command.CommandText = "call spgetclientesnombreapellido(@scalar);";
                                    command.Parameters.Add(mysqlparam);

                                    if (!(db.Database.Connection.State == System.Data.ConnectionState.Open))
                                    {
                                        db.Database.Connection.Open();
                                    }

                                    using (var tabla = new DataTable())
                                    {
                                        tabla.Load(command.ExecuteReader());
                                        dgv.DataSource = tabla;
                                    }

                                    db.Database.Connection.Close();
                                }                                

                                break;
                            }
                        case ("fechanacimiento"):
                            {                                

                                var res = from t in db.clientes where t.fecha_nacimiento == incidencia select t;
                                
                                if (res.Any())
                                {
                                    dgv.DataSource = db.clientes.Where(x => x.fecha_nacimiento.ToString() == incidencia).Select(x => new {
                                        IDCLIENTE = x.id_cliente,
                                        NOMBRE = x.nombre + " " + x.apellido,
                                        DIRECCION = x.calle + " " + x.colonia + " ",
                                        CIUDADEDO = x.ciudad + " " + x.estado,
                                        CP = x.cp,
                                        FNAC = x.fecha_nacimiento,
                                        CONTACTO = x.telefono,
                                        EMAIL = x.email,
                                        CURP = x.curp,
                                        RFC = x.rfc
                                    }).ToList();
                                }
                                else
                                {
                                    dgv.DataSource = db.clientes.Where(x => x.fecha_nacimiento.ToString().Contains(incidencia)).Select(x => new {
                                        IDCLIENTE = x.id_cliente,
                                        NOMBRE = x.nombre + " " + x.apellido,
                                        DIRECCION = x.calle + " " + x.colonia + " ",
                                        CIUDADEDO = x.ciudad + " " + x.estado,
                                        CP = x.cp,
                                        FNAC = x.fecha_nacimiento,
                                        CONTACTO = x.telefono,
                                        EMAIL = x.email,
                                        CURP = x.curp,
                                        RFC = x.rfc
                                    }).ToList();
                                }
                                
                                break;
                            }
                        case ("telefono"):
                            {
                                var res = from t in db.clientes where t.telefono == incidencia select t;

                                if (res.Any())
                                {
                                    dgv.DataSource = db.clientes.Where(x => x.telefono == incidencia).Select(x => new {
                                        IDCLIENTE = x.id_cliente,
                                        NOMBRE = x.nombre + " " + x.apellido,
                                        DIRECCION = x.calle + " " + x.colonia + " ",
                                        CIUDADEDO = x.ciudad + " " + x.estado,
                                        CP = x.cp,
                                        FNAC = x.fecha_nacimiento,
                                        CONTACTO = x.telefono,
                                        EMAIL = x.email,
                                        CURP = x.curp,
                                        RFC = x.rfc
                                    }).ToList();
                                }
                                else
                                {
                                    dgv.DataSource = db.clientes.Where(x => x.telefono.Contains(incidencia)).Select(x => new {
                                        IDCLIENTE = x.id_cliente,
                                        NOMBRE = x.nombre + " " + x.apellido,
                                        DIRECCION = x.calle + " " + x.colonia + " ",
                                        CIUDADEDO = x.ciudad + " " + x.estado,
                                        CP = x.cp,
                                        FNAC = x.fecha_nacimiento,
                                        CONTACTO = x.telefono,
                                        EMAIL = x.email,
                                        CURP = x.curp,
                                        RFC = x.rfc
                                    }).ToList();
                                }
                                break;
                            }
                        case ("codigopostal"):
                            {
                                var res = from t in db.clientes where t.cp.ToString() == incidencia select t;

                                if (res.Any())
                                {
                                    dgv.DataSource = db.clientes.Where(x => x.cp.ToString() == incidencia).Select(x => new {
                                        IDCLIENTE = x.id_cliente,
                                        NOMBRE = x.nombre + " " + x.apellido,
                                        DIRECCION = x.calle + " " + x.colonia + " ",
                                        CIUDADEDO = x.ciudad + " " + x.estado,
                                        CP = x.cp,
                                        FNAC = x.fecha_nacimiento,
                                        CONTACTO = x.telefono,
                                        EMAIL = x.email,
                                        CURP = x.curp,
                                        RFC = x.rfc
                                    }).ToList();
                                }
                                else
                                {
                                    dgv.DataSource = db.clientes.Where(x => x.cp.ToString().Contains(incidencia)).Select(x => new {
                                        IDCLIENTE = x.id_cliente,
                                        NOMBRE = x.nombre + " " + x.apellido,
                                        DIRECCION = x.calle + " " + x.colonia + " ",
                                        CIUDADEDO = x.ciudad + " " + x.estado,
                                        CP = x.cp,
                                        FNAC = x.fecha_nacimiento,
                                        CONTACTO = x.telefono,
                                        EMAIL = x.email,
                                        CURP = x.curp,
                                        RFC = x.rfc
                                    }).ToList();
                                }
                                break;
                            }
                        case ("curp"):
                            {
                                var res = from t in db.clientes where t.curp == incidencia select t;

                                if (res.Any())
                                {
                                    dgv.DataSource = db.clientes.Where(x => x.curp == incidencia).Select(x => new {
                                        IDCLIENTE = x.id_cliente,
                                        NOMBRE = x.nombre + " " + x.apellido,
                                        DIRECCION = x.calle + " " + x.colonia + " ",
                                        CIUDADEDO = x.ciudad + " " + x.estado,
                                        CP = x.cp,
                                        FNAC = x.fecha_nacimiento,
                                        CONTACTO = x.telefono,
                                        EMAIL = x.email,
                                        CURP = x.curp,
                                        RFC = x.rfc
                                    }).ToList();
                                }
                                else
                                {
                                    dgv.DataSource = db.clientes.Where(x => x.curp.Contains(incidencia)).Select(x => new {
                                        IDCLIENTE = x.id_cliente,
                                        NOMBRE = x.nombre + " " + x.apellido,
                                        DIRECCION = x.calle + " " + x.colonia + " ",
                                        CIUDADEDO = x.ciudad + " " + x.estado,
                                        CP = x.cp,
                                        FNAC = x.fecha_nacimiento,
                                        CONTACTO = x.telefono,
                                        EMAIL = x.email,
                                        CURP = x.curp,
                                        RFC = x.rfc
                                    }).ToList();
                                }
                                break;
                            }
                        case ("rfc"):
                            {
                                var res = from t in db.clientes where t.rfc == incidencia select t;

                                if (res.Any())
                                {
                                    dgv.DataSource = db.clientes.Where(x => x.rfc == incidencia).Select(x => new {
                                        IDCLIENTE = x.id_cliente,
                                        NOMBRE = x.nombre + " " + x.apellido,
                                        DIRECCION = x.calle + " " + x.colonia + " ",
                                        CIUDADEDO = x.ciudad + " " + x.estado,
                                        CP = x.cp,
                                        FNAC = x.fecha_nacimiento,
                                        CONTACTO = x.telefono,
                                        EMAIL = x.email,
                                        CURP = x.curp,
                                        RFC = x.rfc
                                    }).ToList();
                                }
                                else
                                {
                                    dgv.DataSource = db.clientes.Where(x => x.rfc.Contains(incidencia)).Select(x => new {
                                        IDCLIENTE = x.id_cliente,
                                        NOMBRE = x.nombre + " " + x.apellido,
                                        DIRECCION = x.calle + " " + x.colonia + " ",
                                        CIUDADEDO = x.ciudad + " " + x.estado,
                                        CP = x.cp,
                                        FNAC = x.fecha_nacimiento,
                                        CONTACTO = x.telefono,
                                        EMAIL = x.email,
                                        CURP = x.curp,
                                        RFC = x.rfc
                                    }).ToList();
                                }
                                break;
                            }
                        default:
                            {
                                genericDefinitions.dangerInfo("Detalle de consulta no especificado.");
                                break;
                            }
                    }

                    this.dgvclientesTitles(ref dgv);

                    this.flag = true;
                            
                }
                catch(Exception ex)
                {
                    genericDefinitions.error(ex.ToString());
                }
            }

            return this.flag;
        }

        public bool getclientesdgvconsultas(ref DGVCustom dgv)
        {
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            using (dbmodel db = new dbmodel())
            {
                try
                {
                    dgv.DataSource = db.clientes.Select(x => new
                    {
                        IDCLIENTE = x.id_cliente,
                        NOMBRE = x.nombre + " " + x.apellido,
                        DIRECCION = x.calle + " " + x.colonia + " ",
                        CIUDADEDO = x.ciudad + " " + x.estado,
                        CP = x.cp,
                        FNAC = x.fecha_nacimiento,
                        CONTACTO = x.telefono,
                        EMAIL = x.email,
                        CURP = x.curp,
                        RFC = x.rfc
                    }).ToList();

                    this.dgvclientesTitles(ref dgv);

                    this.flag = true;
                }
                catch (Exception ex)
                {
                    genericDefinitions.error(ex.ToString());
                }                                
            }
            return this.flag;
        }      

       public cliente getclienteGenerico()
       {
            this.central.Tcliente = null;

            using (dbmodel db = new dbmodel())
            {
                try
                {
                    var res = from t in db.clientes where t.nombre == "PUBLICO" select t;

                    if (res.Any())
                    {
                        this.central.Tcliente = res.First();
                    }
                    else
                    {
                        this.central.Tcliente = new cliente();

                        this.central.Tcliente.nombre = "PUBLICO";
                        this.central.Tcliente.apellido = "PUBLICO";
                        this.central.Tcliente.calle = "PUBLICO";
                        this.central.Tcliente.colonia = "PUBLICO";
                        this.central.Tcliente.ciudad = "PUBLICO";
                        this.central.Tcliente.estado = "CHIHUAHUA";
                        this.central.Tcliente.fecha_nacimiento = "1/1/2000";
                        this.central.Tcliente.cp = 33800;
                        this.central.Tcliente.telefono = "0000000000";
                        this.central.Tcliente.email = "PUBLICO";
                        this.central.Tcliente.curp = "000000000000000000";
                        this.central.Tcliente.rfc = "0000000000000";

                        if (!this.insertClient(this.central.Tcliente))
                        {
                            this.central.Tcliente = null;
                        }
                    }
                }
                catch(Exception ex)
                {
                    genericDefinitions.error(ex.ToString(), "->getclienteGenerico()");
                }
                
            }
            return this.central.Tcliente;
       }

       public bool getSeleccionarclienteventaDGV(ref DGVCustom asx, string alias)
       {
            bool flag = false;
            using (dbmodel local = new dbmodel())
            {
                try
                {

                    var res = local.clientes.Where(x => x.nombre.Contains(alias) || x.apellido.Contains(alias)).Select(t => new { t.id_cliente, t.nombre, t.apellido, t.curp, t.rfc });
                    if (res.Any())
                    {
                        flag = !flag;
                        asx.DataSource = res.ToList();                        
                    }
                }
                catch (Exception ex)
                {
                    genericDefinitions.error(ex.ToString(), "Error Critico.");
                }

            }
            return flag;
        }

        public struct cliente_idnombre
        {
            public int id { get; set; }
            public string nombre {get; set;}

        }
        public cliente_idnombre []  getallclientesnameidasarray()
        {
            this.flag = false;

            cliente_idnombre[] arr = null;

            try
            {
                using (dbmodel db = new dbmodel())
                {                    
                    var res = (from x in db.clientes select new {ID = x.id_cliente,  Nombre = x.nombre + " " + x.apellido}).ToArray(); 
                    
                    if(res.Any())
                    {
                        arr = new cliente_idnombre[res.Length];
                        
                        for(int x=0; x<arr.Length; ++x)
                        {
                            arr[x].id = res[x].ID;
                            arr[x].nombre = res[x].Nombre;
                        }
                    }
                                      
                }
            }
            catch(Exception ex)
            {
                genericDefinitions.error(ex.ToString());
            }

            return arr;
        }

        public void getallClientesDGV(ref DGVCustom asx)
        {
            using (dbmodel local = new dbmodel())
            {

                try
                {

                    asx.DataSource = local.clientes.Select(x => new
                    {
                        Nombre = x.nombre,
                        Apellido = x.apellido,
                        Calle = x.calle,
                        Nacimiento = x.fecha_nacimiento,
                        Telefono = x.telefono
                    ,
                        Email = x.email,
                        Colonia = x.colonia,
                        Ciudad = x.ciudad,
                        CURP = x.curp,
                        RFC = x.rfc                    
                    }).ToList();

                }
                catch (Exception ex)
                {
                    genericDefinitions.error(ex.ToString(), "Error Critico.");
                }

            }

        }

        public cliente getclientAsobject(string curp, string rfc)
        {
            bool flag = false;

            using (dbmodel db = new dbmodel())
            {
                if (this.existcliente(curp, rfc))
                {
                    var cliente = from temp in db.clientes where temp.curp == curp && temp.rfc == rfc select temp;
                    this.central.Setelements();
                    this.central.Tcliente = cliente.First();
                }
                else
                {
                    genericDefinitions.error("C.U.R.P / R.F.C No existen el sistema", "Cliente No encontrado");
                    flag = true;
                }
            }

            if (flag)
                return null;

            return this.central.Tcliente;

        }


        public cliente getclientAsobject(int id)
        {
            cliente local = null;
            try
            {
                using (dbmodel db = new dbmodel())
                {
                    if (this.existcliente(id))
                    {
                        local = new cliente();
                        var cliente = from temp in db.clientes where temp.id_cliente == id select temp;
                        local = cliente.First();
                    }
                }
            }
            catch(Exception ex)
            {
                genericDefinitions.error(ex.ToString());
            }

            return local;
        }

        public string[] getclientAsarray(int id)
        {
            string[] values = new string[12];
            this.central.Tcliente = null;
            this.central.Tcliente = this.getclientAsobject(id);

            if (this.central.Tcliente != null)
            {
                values[0] = this.central.Tcliente.nombre;
                values[1] = this.central.Tcliente.apellido;
                values[2] = this.central.Tcliente.fecha_nacimiento;
                values[3] = this.central.Tcliente.telefono;
                values[4] = this.central.Tcliente.email;
                values[5] = this.central.Tcliente.calle;
                values[6] = this.central.Tcliente.colonia;
                values[7] = this.central.Tcliente.ciudad;
                values[8] = this.central.Tcliente.curp;
                values[9] = this.central.Tcliente.rfc;
                values[10] = this.central.Tcliente.cp.ToString();
                values[11] = this.central.Tcliente.estado;
            }
            else
                return null;

            return values;

        }

        public bool insertClient(ref string[] record)
        {
            bool flag = false;

            using (dbmodel db = new dbmodel())
            {
                cliente tcliente = new cliente();
                this.fillClient(record, ref tcliente);

                if (!(this.existrfc(record[9]) || this.existcurp(record[8])))
                {
                    try
                    {
                        db.clientes.Add(tcliente);
                        db.SaveChanges();
                        genericDefinitions.ok("Se ha dado de alta exitosamente: " + record[0] + " " + record[1], "Proceso Exitoso");
                    }
                    catch (Exception ex)
                    {
                        genericDefinitions.error(ex.ToString(), "Error Critico");
                        flag = true;
                    }
                    finally
                    {
                        tcliente = null;
                    }
                }
                else
                {
                    genericDefinitions.error("R.F.C/Curp ya existe en el sistema", "Integridad de Datos");
                    flag = true;
                }
            }

            if (flag)
                return false;
            return true;
        }

        public bool insertClient(cliente cl)
        {
            this.flag = false;
            using (dbmodel db = new dbmodel())
            {
                try
                {
                    db.clientes.Add(cl);
                    db.SaveChanges();
                    this.flag = true;
                }
                catch(Exception ex)
                {
                    genericDefinitions.error(ex.ToString(), "inserClient(cliente#)");
                }
            }
            return this.flag;
        }

        public bool updateClient(string[] data, string rfc, string curp)
        {
            bool flag = false;
            int id = this.getIdcliente(curp, rfc);

            using (dbmodel db = new dbmodel())
            {
                cliente current = db.clientes.SingleOrDefault(x => x.id_cliente == id);
                this.fillClient(data, ref current);
                try
                {
                    db.SaveChanges();
                    genericDefinitions.ok("Se ha actualizado el cliente con id: " + current.id_cliente.ToString(), "Echo");
                }
                catch (Exception e)
                {
                    genericDefinitions.error(e.ToString(), "Error critico");
                    flag = true;
                }
                finally
                {
                    current = null;
                }

            }
            if (flag)
                return false;
            return true;
        }


        public bool updateClient(string[] data, int id)
        {
            bool flag = false;

            using (dbmodel db = new dbmodel())
            {
                cliente current = db.clientes.SingleOrDefault(x => x.id_cliente == id);
                this.fillClient(data, ref current);
                try
                {
                    db.SaveChanges();
                    genericDefinitions.ok("Se ha actualizado el cliente con id: " + current.id_cliente.ToString(), "Echo");
                }
                catch (Exception e)
                {
                    genericDefinitions.error(e.ToString(), "Error critico");
                    flag = true;
                }
                finally
                {
                    current = null;
                }

            }
            if (flag)
                return false;
            return true;
        }


        public bool deleteClient(int id)
        {
            bool flag = false;
            using (dbmodel db = new dbmodel())
            {

                try
                {
                    var obj = (from d in db.clientes where d.id_cliente == id select d).First();
                    db.clientes.Remove(obj);
                    db.SaveChanges();
                    genericDefinitions.ok("Cliente eliminado", "Echo");
                }
                catch (Exception ex)
                {
                    genericDefinitions.error(ex.ToString(), "Error");
                    flag = true;
                }

            }
            if (flag)
                return false;
            return true;
        }
        public bool isInFacturas(int id)
        {
            this.flag = false;
            
            using (dbmodel db = new dbmodel())
            {
                var res = from t1 in db.ventas
                          join t2 in db.facturas

                          on t1.idventa equals t2.idventa

                          where t1.idcliente == id

                          select new { t1.idcliente, t2.idventa };
                if (res.Any())
                {
                    this.flag = true;
                }
            }
            return this.flag;
        }
        public int getIdcliente(string curp, string rfc)
        {
            int id = -1;
            using (dbmodel model = new dbmodel())
            {
                cliente get = model.clientes.SingleOrDefault(x => x.curp == curp && x.rfc == rfc);
                id = get.id_cliente;
            }

            return id;
        }
        public int getIdcliente(string curp)
        {
            int id = -1;
            using (dbmodel model = new dbmodel())
            {
                cliente get = model.clientes.SingleOrDefault(x => x.curp == curp);
                id = get.id_cliente;
            }

            return id;
        }


        public bool existcurp(string curp)
        {
            using (dbmodel local = new dbmodel())
            {
                var inc = from data in local.clientes where data.curp == curp select data;

                if (inc.Any())
                    return true;

            }
            return false;
        }


        public bool existrfc(string rfc)
        {
            using (dbmodel local = new dbmodel())
            {
                var inc = from data in local.clientes where data.rfc == rfc select data;

                if (inc.Any())
                    return true;

            }
            return false;
        }

        public bool existcliente(string curp, string rfc)
        {
            using (dbmodel db = new dbmodel())
            {
                var get = from ac in db.clientes where ac.curp == curp && ac.rfc == rfc select ac;
                if (get.Any())
                    return true;
            }
            return false;
        }


        public bool existcliente(int id)
        {
            using (dbmodel db = new dbmodel())
            {
                var get = from ac in db.clientes where ac.id_cliente == id select ac;
                if (get.Any())
                    return true;
            }
            return false;
        }
        public bool isinVentas(int id)
        {
            bool flag = false;

            using (dbmodel x = new dbmodel())
            {
                var any = from df in x.ventas where df.idcliente == id select df;
                if (any.Any())
                    flag = true;
                any = null;
            }
            if (flag)
                return true;
            return false;
        }

        private void fillClient(string[] data, ref cliente obj)
        {
            obj.nombre = data[0];
            obj.apellido = data[1];
            obj.fecha_nacimiento = data[2];
            obj.telefono = data[3];
            obj.email = data[4];
            obj.calle = data[5];
            obj.colonia = data[6];
            obj.ciudad = data[7];
            obj.curp = data[8];
            obj.rfc = data[9];
            obj.cp = int.Parse(data[10]);
            obj.estado = data[11];
            
        }

        //->SEC USUARIO 

        public List<string> getusuariosNames()
        {
            List<string> send = new List<string>();

            try
            {
                using (dbmodel db = new dbmodel())
                {
                    var res = db.usuarios.Select(x => new {NAME = x.nombre + " " + x.apellido}).ToList();

                    foreach(var x in res)
                    {
                        send.Add(x.NAME);
                    }
                }
            }
            catch(Exception ex)
            {
                genericDefinitions.error(ex.ToString());
            }

            return send;
        }

        public usuario usrget(int id)
        {
            this.central.Tusuario = null;

            using (dbmodel db = new dbmodel())
            {
                var res = from t in db.usuarios where t.id_usuario == id select t;

                if (res.Any())
                {
                    this.central.Tusuario = res.First();
                }

            }

            return this.central.Tusuario;
        }

        public usuario usrget(string alias)
        {
            this.central.Tusuario = null;

            using (dbmodel db = new dbmodel())
            {
                var res = from t in db.usuarios where (t.alias == alias || (t.nombre+t.apellido).Replace(" ","")==alias) select t;

                if (res.Any())
                {                    
                    this.central.Tusuario = res.First();
                }
                
            }

            return this.central.Tusuario;
        }
        public bool usrexist(string alias)
        {
            this.flag = false;
            using (dbmodel db = new dbmodel())
            {
                var res = from t in db.usuarios where t.alias == alias  ||  (t.nombre+t.apellido).Replace(" ","") == alias select t;
                try
                {
                    if (res.Any())
                    {
                        this.flag = true;
                    }
                }
                catch(Exception ex)
                {
                    genericDefinitions.error(ex.ToString(),"usrexist()->dbop");
                }

            }

            return this.flag;
        }

        public bool usrpasscorrecto(string alias, string pass)
        {
            this.flag = false;

            using (dbmodel db = new dbmodel())
            {
                var res = from t in db.usuarios where (t.alias == alias || (t.nombre + t.apellido).Replace(" ","") == alias) && t.contraseña == pass select t;

                if (res.Any())
                {
                    this.flag = true;
                }
            }

            return this.flag;
        }

        //->SEC LOGIN

        public void setTitledgvsesiones(ref DGVCustom dgv)
        {
            dgv.Columns[0].HeaderText = "Alias";
            dgv.Columns[1].HeaderText = "Nombre";
            dgv.Columns[2].HeaderText = "Hora Inicio";
            dgv.Columns[3].HeaderText = "Hora Cierre";
            dgv.Columns[4].HeaderText = "Fecha Inicio";
            dgv.Columns[5].HeaderText = "Fecha Cierre";            
        }
        public bool getinicios(ref DGVCustom dgv, string caso, string valor)
        {
            this.flag = true;

            
            using (dbmodel db = new dbmodel()) {

                try
                {
                    switch (caso)
                    {
                        case ("all"):
                            {
                                dgv.DataSource = db.logins.Select(x => new {
                                      ALIASUSUARIO = (from t in db.usuarios where t.id_usuario == x.idusuario select t).FirstOrDefault().alias,
                                      NOMBREUSER = (from t in db.usuarios where t.id_usuario == x.idusuario select t).FirstOrDefault().nombre + " " + (from t in db.usuarios where t.id_usuario == x.idusuario select t).FirstOrDefault().apellido,
                                      INICIO = x.hora_inicio,
                                      CIERRE = x.hora_cierre,
                                      FECHA_INICIO = x.fecha_inicio,
                                      FECHA_CIERRE = x.fecha_cierre
                                }).ToList();
                                break;
                            }
                        case ("inicio"):
                            {
                                DateTime fecha = DateTime.Parse(valor);

                                dgv.DataSource = db.logins.Where(x => x.fecha_inicio.ToString() == fecha.ToString()).Select(x => new {
                                    ALIASUSUARIO = (from t in db.usuarios where t.id_usuario == x.idusuario select t).FirstOrDefault().alias,
                                    NOMBREUSER = (from t in db.usuarios where t.id_usuario == x.idusuario select t).FirstOrDefault().nombre + " " + (from t in db.usuarios where t.id_usuario == x.idusuario select t).FirstOrDefault().apellido,
                                    INICIO = x.hora_inicio,
                                    CIERRE = x.hora_cierre,
                                    FECHA_INICIO = x.fecha_inicio,
                                    FECHA_CIERRE = x.fecha_cierre
                                }).ToList();
                                break;
                            }
                        case ("cierre"):
                            {
                                DateTime fecha = DateTime.Parse(valor);

                                dgv.DataSource = db.logins.Where(x => x.fecha_cierre.Value.ToString() == fecha.ToString()).Select(x => new {
                                    ALIASUSUARIO = (from t in db.usuarios where t.id_usuario == x.idusuario select t).FirstOrDefault().alias,
                                    NOMBREUSER = (from t in db.usuarios where t.id_usuario == x.idusuario select t).FirstOrDefault().nombre + " " + (from t in db.usuarios where t.id_usuario == x.idusuario select t).FirstOrDefault().apellido,
                                    INICIO = x.hora_inicio,
                                    CIERRE = x.hora_cierre,
                                    FECHA_INICIO = x.fecha_inicio,
                                    FECHA_CIERRE = x.fecha_cierre
                                }).ToList();
                                break;
                            }
                        case ("usuario"):
                            {
                                var ids = from t in db.usuarios where (t.nombre + t.apellido).Replace(" ","").Contains(valor.Replace(" ", "")) select t.id_usuario;

                                dgv.DataSource = (from xlogin in db.logins
                                            join xusuario in db.usuarios on xlogin.idusuario equals xusuario.id_usuario
                                            where ids.Contains(xusuario.id_usuario)
                                            select new
                                            {
                                                ALIASUSUARIO = xusuario.alias,
                                                NOMBREUSER = xusuario.nombre + " " + xusuario.apellido,
                                                INICIO = xlogin.hora_inicio,
                                                CIERRE = xlogin.hora_cierre,
                                                FECHA_INICIO = xlogin.fecha_inicio,
                                                FECHA_CIERRE = xlogin.fecha_cierre
                                            }).ToList();                  
                                break;
                            }
                    }

                    this.setTitledgvsesiones(ref dgv);

                    this.flag = true;
                }
                catch(Exception ex)
                {
                    genericDefinitions.error(ex.ToString());
                }
            }

            return this.flag;
        }
        public bool insertlogin(ref login log)
        {
            this.flag = false;

            using(dbmodel db = new dbmodel())
            {
                try
                {
                    db.logins.Add(log);
                    db.SaveChanges();
                    this.flag = true;
                }
                catch(Exception ex)
                {
                    genericDefinitions.error(ex.ToString(), "Error Login-DB");
                }
            }

            return this.flag;
        }

        public bool cierrelogin(login log)
        {
            this.flag = false;

            using (dbmodel db =new dbmodel())
            {
                try
                {
                    var res = from t in db.logins where t.idlogin == log.idlogin select t;

                    if (res.Any())
                    {                        
                        res.First().hora_cierre = log.hora_cierre;
                        res.First().fecha_cierre = log.fecha_cierre;

                        db.SaveChanges();

                        this.flag = true;
                    }
                 }
                catch(Exception ex)
                {
                    genericDefinitions.error(ex.ToString());
                }
            }

            return this.flag;
        }

        public bool existlogin(int idlogin)
        {
            this.flag = false;

            using (dbmodel db = new dbmodel())
            {
                try
                {
                    var res = from t in db.logins where t.idlogin == idlogin select t;
                    if (res.Any())
                    {
                        this.flag = true;
                    }
                }
                catch(Exception ex)
                {
                    genericDefinitions.error(ex.ToString());
                }
            }
            return this.flag;
        }

        //->SEC TIENDA

        public bool updateTienda(tienda latienda)
        {
            this.flag = false;

            using (dbmodel db = new dbmodel())
            {
                try
                {
                    var res = (from t in db.tiendas where t.idtienda == latienda.idtienda select t);

                    if (res.Any())
                    {
                        var upd = res.First();    
                                      
                        upd.calle = latienda.calle;
                        upd.ciudad = latienda.ciudad;
                        upd.colonia = latienda.colonia;
                        upd.cp = latienda.cp;
                        upd.estado = latienda.estado;
                        upd.logotienda = latienda.logotienda;
                        upd.nombre = latienda.nombre;
                        upd.numero = latienda.numero;
                        upd.pais = latienda.pais;
                        upd.razonsocial = latienda.razonsocial;
                        upd.rfc = latienda.rfc;
                        upd.telefono = latienda.telefono;
                       
                        db.SaveChanges();

                        this.flag = true;
                    }
                }
                catch(Exception ex)
                {
                    genericDefinitions.error(ex.ToString());
                }
            }

            return this.flag;
        }

        public bool deleteTienda(int id)
        {
            this.flag = false;

            using (dbmodel db = new dbmodel())
            {
                try
                {
                    var res = from t in db.tiendas where t.idtienda == id select t;

                    if (res.Any())
                    {
                        db.tiendas.Remove(res.First());
                        db.SaveChanges();
                        this.flag = true;
                    }                   
                }
                catch(Exception ex)
                {
                    genericDefinitions.error(ex.ToString());
                }
            }

            return this.flag;
        }
        public bool insertTienda(tienda objTienda)
        {
            this.flag = false;

            using (dbmodel db = new dbmodel())
            {
                if (!this.existTienda())
                {
                    try
                    {
                        db.tiendas.Add(objTienda);
                        db.SaveChanges();
                        flag = true;
                    }
                    catch(Exception ex)
                    {
                        genericDefinitions.error(ex.ToString());
                    }
                }
            }
            return this.flag;
        }

        public tienda getTienda()
        {
            this.central.Ttienda = null;            
            using(dbmodel db = new dbmodel())
            {
                this.central.Ttienda = db.tiendas.FirstOrDefault();
            }
            return this.central.Ttienda;
        }

        public bool existTienda()
        {
            this.flag = false;
            using (dbmodel db = new dbmodel())
            {
                var res = from t in db.tiendas select t;

                if (res.Any())
                {
                    this.flag = true;
                }
            }

            return this.flag;        
        }
        public int getIdTienda()
        {
            int id = -1;

            using (dbmodel db = new dbmodel())
            {
                var res = from t in db.tiendas select t;

                if (res.Any())
                {
                    id = res.First().idtienda;
                }
            }

            return id;
        }

        //SEC TICKET

        public bool existTicket(int no)
        {
            this.flag = false;

            using (dbmodel db = new dbmodel())
            {
                try
                {
                    var res = from t in db.tickets where t.id == no select t;

                    if (res.Any())
                    {
                        flag = !flag;
                    }
                }
                catch(Exception ex)
                {
                    genericDefinitions.error(ex.ToString());
                }
            }

            return this.flag;
        }
        public int getticketid(int venta)
        {
            int id = -1;
            using (dbmodel db = new dbmodel())
            {
                var res = from t1 in db.ventas

                          join t2 in db.tickets

                          on t1.idventa equals t2.idventa

                          where t1.idventa == venta

                          select new { t2.id };

                if (res.Any())
                {
                    id = res.First().id;
                }
            }
            return id;
        }

        public int getticketcurrent()
        {
            int x = 0;
            using(dbmodel db = new dbmodel())
            {
                var res = db.tickets.OrderByDescending(k => k.id);

                if (res.Any())
                {
                    x = res.First().id++;
                }                
            }
            return x;
        }

        public bool addticket(int venta)
        {
            this.flag = false;

            using (dbmodel db = new dbmodel())
            {
                try
                {
                    this.central.Tticket = new ticket() { idventa = venta };
                    db.tickets.Add(this.central.Tticket);
                    db.SaveChanges();
                    this.flag = true;
                }
                catch(Exception ex)
                {
                    genericDefinitions.error(ex.ToString(), "Error Runtime addticket(int)");
                }                
            }
            return this.flag;
        }

        //->SEC DATA BASE IMPLEMENTATION

        public bool restoreDB(string path)
        {
            this.flag = false;

            try
            {
                using (dbmodel db = new dbmodel())
                {
                    using (MySqlConnection conn = new MySqlConnection(db.Database.Connection.ConnectionString))
                    {
                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            using (MySqlBackup mb = new MySqlBackup(cmd))
                            {
                                cmd.Connection = conn;

                                if (conn.State != ConnectionState.Open)
                                {
                                    conn.Open();
                                }                               
                                mb.ImportFromFile(path);
                                conn.Close();

                                this.flag = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                genericDefinitions.error(ex.ToString());
            }

            return this.flag;
        }           
        public bool dumpDB(string path)
        {
            this.flag = false;

            try
            {
                using (dbmodel db = new dbmodel())
                {
                    using (MySqlConnection con = new MySqlConnection(db.Database.Connection.ConnectionString))
                    {
                        using (MySqlCommand command = new MySqlCommand())
                        {
                            using (MySqlBackup backup = new MySqlBackup(command))
                            {
                                if (con.State != ConnectionState.Open)
                                {
                                    con.Open();
                                }

                                command.Connection = con;

                                command.CommandText = "SET SESSION SQL_MODE = TRADITIONAL";

                                command.ExecuteNonQuery();

                                backup.ExportInfo.ExportFunctions = false;
                                backup.ExportInfo.ExportProcedures = false;
                                backup.ExportInfo.ExportEvents = false;
                                backup.ExportInfo.ExportViews = false;
                                backup.ExportToFile(path);

                                con.Close();

                                this.flag = true;

                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                genericDefinitions.error(ex.ToString());
            }

            return this.flag;
        }
        
        public bool checkDB()
        {
            bool flag = false;

            using (dbmodel db = new dbmodel())
            {
                try
                {
                    if (db.Database.Exists())
                    {
                        flag = true;
                    }
                }
                catch (Exception ex)
                {
                    genericDefinitions.error(ex.ToString(), "Error con Base de datos");
                }
            }

            return flag;
        }
    }    
}
