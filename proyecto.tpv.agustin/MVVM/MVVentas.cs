using NLog;
using proyecto.tpv.agustin.Modelo;
using proyecto.tpv.agustin.Servicios;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace proyecto.tpv.agustin.MVVM
{
    /// <summary>
    /// MVVM de Ventas
    /// </summary>
    public class MVVentas : MVBase
    {
        private proyecto_tpvEntities tpvEnt;    //Conexion con la base de datos

        //Servicios
        private UsuariosServicio usuServ;       //Servicio de usuarios
        private ClienteServicio cliServ;        //Servicio de clientes
        private Ventas_De_ProductosServicio ventaProdServ;      //Servicio de ventas de productos
        private VentasServicio  ventaServ;      //Servicio de ventas
        

        private ventas objVenta;                            //Objeto de ventas para insertar mediante formulario
        private ventas_de_productos objVentasProductos;     //Objeto de ventas de productos insertar mediante formulario
        
        private List<ventas_de_productos> listaVentasProdCompra;    //Lista de ventas de productos del datagrid

        //Lista de ventas datagrid
        private ListCollectionView listaCollecVentasDataGrid;       //ListCollectionView de productos del datagrid

        
        //fechas para el filtro de busqueda de fechas
        private DateTime fechaIni;      //Variable para la fecha de inicio, filtro de fechas de ventas
        private DateTime fechaFin;      //Variable para la fecha de fin, filtro de fechas de ventas

        private static Logger log = LogManager.GetCurrentClassLogger();     //Variable del logger para recoger los errores e informacion del programa

        /// <summary>
        /// Constructor MVVM de Ventas
        /// </summary>
        /// <param name="ent">Conexion con la base de datos pasada por parametro</param>
        public MVVentas(proyecto_tpvEntities ent)
        {
            tpvEnt = ent;
            inicializa();
        }

        /// <summary>
        /// Metodo para inicializar los objetos 
        /// y variables del MVVM del MV de ventas
        /// </summary>
        private void inicializa()
        {
            ventaServ = new VentasServicio(tpvEnt);

            usuServ = new UsuariosServicio(tpvEnt);
            cliServ = new ClienteServicio(tpvEnt);
            ventaProdServ = new Ventas_De_ProductosServicio(tpvEnt);
            
            
            objVenta = new ventas();
            objVenta.fecha = DateTime.Now;

            objVentasProductos = new ventas_de_productos();

            listaVentasProdCompra = new List<ventas_de_productos>();

            listaCollecVentasDataGrid = new ListCollectionView(ventaServ.getAll().ToList());

            fechaIni = DateTime.Now;
            fechaFin = DateTime.Now;
        }

        
        /// <summary>
        /// //ListCollectionView de productos del datagrid
        /// </summary>
        public ListCollectionView listaVentasListColletion
        {
            get
            {
                return listaCollecVentasDataGrid;
            }
        }

        
        /// <summary>
        /// lista de usuarios para el combobox de usuarios del formulario
        /// </summary>
        public List<usuarios> listaUsuarios
        {
            get
            {
                return usuServ.getAll().ToList();
            }
        }

        
        /// <summary>
        /// lista de clientes para el combobox de usuarios del formulario
        /// </summary>
        public List<clientes> listaClientes
        {
            get
            {
                return cliServ.getAll().ToList();
            }
        }

        
        /// <summary>
        /// Metodo para Pasar la lista desde la interfaz
        /// desde DialogVentaDeProd al mv de ventas de productos
        /// </summary>
        /// <param name="listaVentasProductos">le pasamos la lista de ventas de productos</param>
        public void setListaProductos(List<ventas_de_productos> listaVentasProductos)
        {
            listaVentasProdCompra = listaVentasProductos;
        }
        
        /// <summary>
        /// Variable para pasar la lista al datagrid 
        /// de DialogVentaDeProd para mostrarla
        /// </summary>        
        public List<ventas_de_productos> listaVentasProdsDatagrid
        {
            get
            {
                return listaVentasProdCompra;
            }
        }
        
        /// <summary>
        /// Recoge la venta nueva a insertar desde la interfaz
        /// </summary>        
        public ventas ventaNueva
        {
            get
            {
                return objVenta;
            }
            set
            {
                objVenta = value;
                OnPropertyChanged("ventaNueva");
            }
        }

        
        /// <summary>
        /// Metodo para ponerle los totales calculados al objeto 
        /// de venta a insertar
        /// </summary>
        /// <param name="total">Total de la venta sin IVA</param>
        /// <param name="totalIva">Total de la venta con el IVA</param>
        /// <param name="iva">IVA de la venta</param>
        public void pasarTotalesVenta(double total, double totalIva, double iva)
        {
            objVenta.total = total;
            objVenta.total_mas_iva = totalIva;
            objVenta.iva = iva;
        } 

        
        /// <summary>
        /// Variable que permite pasar y obtener el objeto de 
        /// ventas de productos
        /// </summary>
        public ventas_de_productos ventasDeProdNueva
        {
            get
            {
                return objVentasProductos;
            }
            set
            {
                objVentasProductos = value;
                OnPropertyChanged("ventasDeProdNueva");
            }
        }

        
        /// <summary>
        /// Metodo de guardar venta del MV
        /// </summary>
        /// <returns>retorna un valor booleano para 
        /// saber si se ha introducido correctamente</returns>
        public bool guardaVenta()
        {
            bool correcto = true;

            if (objVenta.iva == null)
            {
                objVenta.iva = 0;
            }
            
            ventaServ.add(objVenta);

            try
            {
                ventaServ.save();
                log.Info("\nInsertar venta ... Todo correcto\n");
            }
            catch (DbUpdateException dbex)
            {
                correcto = false;
                log.Error("\nInsertar venta ... Error\n"
                   + dbex.InnerException.Message + "\n" + dbex.StackTrace);
            }

            return correcto;
        }

        
        /// <summary>
        /// Metodo que le pone la lista de productos a la venta
        /// </summary>
        /// <returns>retorna un valor booleano para 
        /// saber si se ha introducido correctamente</returns>
        public bool cargarVentasDeProd()
        {
            //Si esta correcto o no
            bool correcto = false;

            //Obtener ultima venta
            int codVent = 0;
            codVent = ventaServ.getLastId();

            if (codVent > 0)
            {
                //valor booleano que hace que salte 
                //si el registro es erroneo
                correcto = false;

                if (listaVentasProdCompra.Count != 0 && listaVentasProdCompra != null)
                { 

                    //Poner venta por cada venta de producto
                    for (int i = 0; i < listaVentasProdCompra.Count; i++)
                    {

                        ventas_de_productos venProd = listaVentasProdCompra[i];
                        venProd.cod_venta = codVent;
                        
                        //Grabar venta de producto
                        ventaProdServ.add(venProd);

                    }

                    //Comfirmar ventas de productos y las 
                    //guarda en la base de datos
                    correcto = guardaVentaProd();

                }
            }
            else
            {
                correcto = false;
            }

            listaVentasProdCompra.Clear();

            return correcto;

        }


        /// <summary>
        /// Metodo que graba varias ventas 
        /// de productos agregadas en el metodo
        /// anterior cargarVentasDeProd()
        /// </summary>
        /// <returns>retorna un valor booleano para 
        /// saber si se ha introducido correctamente</returns>
        public bool guardaVentaProd()
        {

            bool correcto = true;
            
            try
            {
                ventaProdServ.save();
                log.Info("\nInserta venta de producto ... Todo correcto\n");
            }
            catch (DbUpdateException dbex)
            {
                correcto = false;
                log.Error("\nInserta venta de producto ... Error\n"
                   + dbex.InnerException.Message + "\n" + dbex.StackTrace);
            }

            return correcto;

        }


        //Filtros ------------------------------

        /// <summary>
        /// Filtro de ventas por fecha
        /// </summary>
        /// <param name="obj">Le pasamos un objeto</param>
        /// <returns>retorna un valor booleano para 
        /// hacer el filtrado</returns>
        public bool filtroFechas(object obj)
        {
            bool filtro = false;
            ventas ven = obj as ventas;
            if (ven.fecha != null)
            {
                if (ven.fecha > fechaIni && ven.fecha < fechaFin)
                {
                    filtro = true;
                }
            }
            return filtro;
        }
        
        /// <summary>
        /// Variable de fecha inicial para el filtro de fechas
        /// </summary>
        public DateTime fechaInicial
        {
            get
            {
                return fechaIni;
            }
            set
            {
                fechaIni = value;
                OnPropertyChanged("fechaInicial");
            }
        }
        
        /// <summary>
        /// Variable de fecha final para el filtro de fechas
        /// </summary>
        public DateTime fechaFinal
        {
            get
            {
                return fechaFin;
            }
            set
            {
                fechaFin = value;
                OnPropertyChanged("fechaFinal");
            }
        }

        //Metodos para quitar ventas y ventas de productos ------------------------

        /// <summary>
        /// Metodo para Quitar venta
        /// </summary>
        /// <param name="vent">Objeto de venta a quitar</param>
        /// <returns>retorna un valor booleano para 
        /// saber si se ha introducido correctamente</returns>
        public bool borrarVenta(ventas vent)
        {
            bool correcto = true;
            ventaServ.delete(vent);
            try
            {
                try
                {
                    ventaServ.save();
                    log.Info("\nBorrar venta ... Todo correcto\n");
                }
                catch (Exception ex)
                {
                    correcto = false;
                    log.Error("\nBorrar venta ... Error\n" + "\n" + ex.StackTrace);
                }


            }
            catch (DbUpdateException dbex)
            {
                correcto = false;
                log.Error("\nBorrar venta ... Error\n"
                   + dbex.InnerException.Message + "\n" + dbex.StackTrace);
            }

            return correcto;
        }

        /// <summary>
        /// Metodo para Quitar una venta de producto
        /// </summary>
        /// <param name="ventProd">Objeto de venta de producto a quitar</param>
        /// <returns>retorna un valor booleano para 
        /// saber si se ha introducido correctamente</returns>
        public bool borrarVentaProd(ventas_de_productos ventProd)
        {
            bool correcto = true;
            ventaProdServ.delete(ventProd);
            try
            {
                try
                {
                    ventaProdServ.save();
                    log.Info("\nBorrar venta de producto ... Todo correcto\n");
                }
                catch (Exception ex)
                {
                    correcto = false;
                    log.Error("\nBorrar venta de producto ... Error\n" + "\n" + ex.StackTrace);
                }


            }
            catch (DbUpdateException dbex)
            {
                correcto = false;
                log.Error("\nBorrar venta de producto ... Error\n"
                   + dbex.InnerException.Message + "\n" + dbex.StackTrace);
            }

            return correcto;
        }


    }
}
