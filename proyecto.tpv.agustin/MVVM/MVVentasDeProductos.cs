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
    /// MVVM de Ventas de Productos
    /// </summary>
    public class MVVentasDeProductos : MVBase
    {
        private proyecto_tpvEntities tpvEnt;                //Conexion con la base de datos

        private Ventas_De_ProductosServicio ventaProdServ;  //Servicio de ventas de producto
        private ServicioGenerico<ventas> ventaServ;         //Servicio de ventas
        
        private ventas_de_productos ventaProdSelect;        //Objeto de ventas_de_productos Selecion en el datagrid de devolver ventas de productos

        private List<ventas_de_productos> listaVentasProdCompra;        //Lista de productos a comprar
        private ListCollectionView listaCollecVentasProdCompra;         //Lista de productos a comprar con listColletionView

        //fechas para el filtro de busqueda de fechas
        private DateTime fechaIni;      //Variable para la fecha de inicio, filtro de fechas de ventas de productos
        private DateTime fechaFin;      //Variable para la fecha de final, filtro de fechas de ventas de productos

        //Venta de la factura
        private VentasServicio ventaServFactura;                    //Servicio de ventas
        private ventas ventaFactura;                                //Objeto de la venta de la factura
        private List<ventas_de_productos> listaVentasProdFactura;   //Lista de ventas de producto de la factura

        private static Logger log = LogManager.GetCurrentClassLogger();     //Variable del logger para recoger los errores e informacion del programa

        /// <summary>
        /// Constructor MVVM de Ventas de productos
        /// </summary>
        /// <param name="ent">Conexion con la base de datos pasada por parametro</param>
        public MVVentasDeProductos(proyecto_tpvEntities ent)
        {
            tpvEnt = ent;
            inicializa();
        }

        /// <summary>
        /// Metodo para inicializar los objetos 
        /// y variables del MVVM del MV de ventas de productos
        /// </summary>
        private void inicializa()
        {
            //ventaProdServ = new ServicioGenerico<ventas_de_productos>(tpvEnt);
            ventaProdServ = new Ventas_De_ProductosServicio(tpvEnt);
            //ventaServ = new VentasServicio(tpvEnt);
            ventaServ = new ServicioGenerico<ventas>(tpvEnt);

            listaVentasProdCompra = new List<ventas_de_productos>();
            listaCollecVentasProdCompra = new ListCollectionView(ventaProdServ.getAll().ToList());
            ventaProdSelect = new ventas_de_productos();

            fechaIni = DateTime.Now;
            fechaFin = DateTime.Now;

            //Obtener ultima venta para la factura
            ventaFactura = new ventas();
            ventaServFactura = new VentasServicio(tpvEnt);
            listaVentasProdFactura = new List<ventas_de_productos>();

            ventaFactura = ventaServFactura.getLastVenta();

            sacarVentasDeProdFactura(ventaFactura);
        }

        /// <summary>
        /// Metodo para sacar las ventas de 
        /// productos de la factura
        /// </summary>
        /// <param name="ventaFactura">Venta que le pasamos para sacar sus ventas de producto</param>
        private void sacarVentasDeProdFactura(ventas ventaFactura)
        {
            List<ventas_de_productos> listaVentaProd = new List<ventas_de_productos>();
            listaVentaProd = ventaFactura.ventas_de_productos.Cast<ventas_de_productos>().ToList();

            foreach (ventas_de_productos ventProd in listaVentaProd)
            {
                listaVentasProdFactura.Add(ventProd);
            }

        }
        
        /// <summary>
        /// Variable para seleccionar las ventas de productos de la factura
        /// </summary>
        public List<ventas_de_productos> listaVentasProductosFactura
        {
            get
            {
                return listaVentasProdFactura;
            }
        }

        
        /// <summary>
        /// Variable para la seleccion de la venta de producto
        /// del datagrid
        /// </summary>
        public ventas_de_productos ventaProdSelectDatagrid
        {
            get
            {
                return ventaProdSelect;
            }
            set
            {
                ventaProdSelect = value;
                OnPropertyChanged("ventaProdSelectDatagrid");
            }
        }


        /// <summary>
        /// Variable para Pasar lista de ventas de productos
        /// en ListCollectionView
        /// </summary>
        public ListCollectionView listaVentasProdListColletion
        {
            get
            {
                return listaCollecVentasProdCompra;
            }
        }
        

        /// <summary>
        /// Metodo que sirve para Pasar la lista desde la interfaz, 
        /// </summary>
        /// <param name="listaVentasProductos">Le pasamos la lista de productos</param>
        public void setListaProductos(List<ventas_de_productos> listaVentasProductos)
        {
            listaVentasProdCompra = listaVentasProductos;
            listaCollecVentasProdCompra = new ListCollectionView(listaVentasProdCompra);
        }

        
        /// <summary>
        /// Metodo que seirve para mostrar la 
        /// lista de ventas de productos en el datagrid
        /// de ventas de productos
        /// </summary>
        public List<ventas_de_productos> listaVentasProdsDatagrid
        {
            get
            {
                return listaVentasProdCompra;
            }
        }
        
        /// <summary>
        /// Metodo que sirve para mostrar la lista de ventas de productos
        /// en el datagrid de las ventas de productos
        /// </summary>
        public ListCollectionView listaVentasProdsDatagridListCollect
        {
            get
            {
                return listaCollecVentasProdCompra;
            }
        }
        
        /// <summary>
        /// Metodo que permite agregar una nueva venta de producto
        /// a la lista de ventas de productos a comprar,
        /// listaVentasProdCompra
        /// </summary>
        /// <param name="venProd"></param>
        public void anyadirVentaProd(ventas_de_productos venProd)
        {
            listaVentasProdCompra.Add(venProd);
            listaCollecVentasProdCompra = new ListCollectionView(listaVentasProdCompra);
        }

        
        /// <summary>
        /// Metodo que permite vaciar la lista de productos,
        /// se usa con el boton del tpv de quitar todos los productos
        /// </summary>
        public void vaciarListaProductos()
        {
            listaVentasProdCompra.Clear();
            listaCollecVentasProdCompra = new ListCollectionView(listaVentasProdCompra);
        }

        /// <summary>
        /// Metodo para restar un producto a partir de 
        /// una venta de productos
        /// </summary>
        /// <param name="venProd"></param>
        public void quitarProducto(ventas_de_productos venProd)
        {

            //Cogemos la lista y la recorremos
            List<ventas_de_productos> listaVentasProdCompraNew = new List<ventas_de_productos>();

            listaVentasProdCompraNew = listaVentasProdCompra;

            for (int i = 0; i < listaVentasProdCompraNew.Count; i++)
            {
                ventas_de_productos venProdArray = new ventas_de_productos();
                venProdArray = listaVentasProdCompraNew[i];

                if(venProd.Equals(venProdArray))
                {
                    if (venProdArray.cantidad_vendida <= 1)
                    {
                        listaVentasProdCompraNew.Remove(venProdArray);
                    }
                    else
                    {
                        //Inicializar
                        double precioProd = 0;
                        double operandoCantPreUnidades = 0;

                        //Sacar valores
                        precioProd = (double) venProd.productos.precio;
                        operandoCantPreUnidades = (double) venProd.precio_total_unidades;
                        
                        //Restamos un producto con su precio
                        venProd.cantidad_vendida = venProd.cantidad_vendida - 1;
                        venProd.precio_total_unidades = operandoCantPreUnidades - precioProd;

                        //Guardar venta de producto en la lista
                        //Si eso borrar antes
                        listaVentasProdCompraNew[i] = venProd;

                    }
                    
                }

            }

            //Anyadir lista modificada con los productos quitados
            listaVentasProdCompra = listaVentasProdCompraNew;


        }
        

        /// <summary>
        /// Metodo para borrar una venta de los productos
        /// </summary>
        /// <param name="ventProd"></param>
        /// <returns>retorna un valor booleano para 
        /// saber si se ha introducido correctamente</returns>
        public bool borrarVentaProdSelect(ventas_de_productos ventProd)
        {
            
            bool correcto = true;
            ventas vent = new ventas();
            vent = ventProd.ventas;

            //Obtener datos de la venta y ventas de productos
            double totalVenta = (double) vent.total;
            double totalVentaAct = 0;

            double iva = (double)vent.iva;
            double totalMasIvaAct = 0; //total mas Iva actualizado 

            double totalVentaProds = (double)ventProd.precio_total_unidades;
            
            //Obtener el total de la venta nuevo, se retira la venta de productos
            totalVentaAct = totalVenta - totalVentaProds;

            //Sacar el IVA actualizado
            if (iva > 0)
            {
                double compraIva = (totalVentaAct * iva) / 100;
                totalMasIvaAct = totalVentaAct + compraIva;
            }

            //Poner el total mas el iva
            vent.total = totalVentaAct;
            vent.total_mas_iva = totalMasIvaAct;

            //modificar el objeto de la venta en la base de datos
            correcto = editarVenta(vent);

            //Borrar el objeto de la venta de producto
            correcto = borrarVentaProd(ventProd);

            return correcto;

        }
        

        /// <summary>
        /// Metodo para Modificar una venta
        /// </summary>
        /// <param name="vent">Venta que le pasamos a editar</param>
        /// <returns>retorna un valor booleano para 
        /// saber si se ha introducido correctamente</returns>
        public bool editarVenta(ventas vent)
        {
            bool correcto = true;

            ventaServ.edit(vent);
            try
            {
                try
                {
                    ventaServ.save();
                    log.Info("\nEditar venta ... Todo correcto\n");
                }
                catch (Exception ex)
                {
                    correcto = false;
                    log.Error("\nEditar venta ... Error\n" + "\n" + ex.StackTrace);
                }


            }
            catch (DbUpdateException dbex)
            {
                correcto = false;
                log.Error("\nEditar venta ... Error\n"
                   + dbex.InnerException.Message + "\n" + dbex.StackTrace);
            }

            return correcto;
            
        }
        
        /// <summary>
        /// Metodo para borrar una venta de producto
        /// </summary>
        /// <param name="ventProd">Venta de producto que le pasamos
        /// para borrar</param>
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

        /// <summary>
        /// Metodo para editar una venta de producto
        /// </summary>
        /// <param name="ventProd">Venta de producto a editar</param>
        /// <returns>retorna un valor booleano para 
        /// saber si se ha introducido correctamente</returns>
        public bool editarVentaProd(ventas_de_productos ventProd)
        {
            bool correcto = true;

            ventaProdServ.edit(ventProd);
            try
            {
                try
                {
                    ventaProdServ.save();
                    log.Info("\nEditar venta de producto ... Todo correcto\n");
                }
                catch (Exception ex)
                {
                    correcto = false;
                    log.Error("\nEditar venta de producto ... Error\n" + "\n" + ex.StackTrace);
                }


            }
            catch (DbUpdateException dbex)
            {
                correcto = false;
                log.Error("\nEditar venta de producto ... Error\n"
                   + dbex.InnerException.Message + "\n" + dbex.StackTrace);
            }

            return correcto;
        }

        //Filtro de fechas -------------------------

        /// <summary>
        /// Filtro de ventas de producto por fecha
        /// </summary>
        /// <param name="obj">Le pasamos un objeto</param>
        /// <returns>retorna un valor booleano para 
        /// hacer el filtrado</returns>
        public bool filtroFechas(object obj)
        {
            bool filtro = false;
            ventas_de_productos ventaProds = obj as ventas_de_productos;

            ventas ven = ventaProds.ventas;
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


    }
}
