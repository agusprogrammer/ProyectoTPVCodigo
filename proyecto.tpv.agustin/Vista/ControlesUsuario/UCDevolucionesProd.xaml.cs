using NLog;
using proyecto.tpv.agustin.Modelo;
using proyecto.tpv.agustin.MVVM;
using proyecto.tpv.agustin.Servicios;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace proyecto.tpv.agustin.Vista.ControlesUsuario
{
    /// <summary>
    /// Lógica de interacción para UCDevolucionesProd.xaml
    /// </summary>
    public partial class UCDevolucionesProd : UserControl
    {
        private proyecto_tpvEntities tpvEnt;            //Conexion con la base de datos
        private MVVentasDeProductos mVVentaProd;        //MV de ventas de productos
        private ServicioGenerico<ventas> ventaServ;     //Servicio de ventas
        private Predicate<object> predicadoNombreProd;  //Predicado del nombre para buscar por el nombre de producto

        private static Logger log = LogManager.GetCurrentClassLogger();     //Variable del logger para recoger los errores e informacion del programa

        /// <summary>
        /// Constructor UCDevolucionesProd
        /// </summary>
        /// <param name="ent">conexion que le pasamos como parametro</param>
        public UCDevolucionesProd(proyecto_tpvEntities ent)
        {
            InitializeComponent();
            tpvEnt = ent;
            mVVentaProd = new MVVentasDeProductos(tpvEnt);
            ventaServ = new ServicioGenerico<ventas>(tpvEnt);

            DataContext = mVVentaProd;

            predicadoNombreProd = new Predicate<object>(filtroNombreProd);

        }
        

        /// <summary>
        /// Evento del boton que resta un producto a una venta de producto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRestaProd_Click(object sender, RoutedEventArgs e)
        {
            if (datagridDevolProds.SelectedItem != null)
            {
                MessageBoxResult resultado = MessageBox.Show("¿Quieres restar productos a la venta del producto?", "GESTION DE DEVOLUCIONES", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (resultado == MessageBoxResult.Yes)
                {
                    ventas_de_productos ventasProd = (ventas_de_productos)(datagridDevolProds.SelectedItem);
                    
                    //Borrar la venta de producto
                    //if (mVVentaProd.quitarProductoVentaProd(ventasProd))
                    if (quitarProductoVentaProd(ventasProd))
                    {
                        mVVentaProd.listaVentasProdListColletion.Remove((ventas_de_productos)(datagridDevolProds.SelectedItem)); //el refresh es para el list collestion view, no las listas normales
                        MessageBox.Show("Elemento borrado", "GESTION DE DEVOLUCIONES", MessageBoxButton.OK, MessageBoxImage.Information);
                        
                        datagridDevolProds.Items.Refresh();

                    }
                    else
                    {
                        MessageBox.Show("Error al borrar la venta", "GESTION DE DEVOLUCIONES", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }


            }
        }
        

        /// <summary>
        /// Evento que quita una venta de producto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRestaVentaProd_Click(object sender, RoutedEventArgs e)
        {
            if (datagridDevolProds.SelectedItem != null)
            {
                MessageBoxResult resultado = MessageBox.Show("¿Quieres borrar la venta del producto?", "GESTION DE DEVOLUCIONES", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (resultado == MessageBoxResult.Yes)
                {
                    ventas_de_productos ventasProd = (ventas_de_productos)(datagridDevolProds.SelectedItem);

                    //Borrar la venta de producto
                    //if (mVVentaProd.borrarVentaProdSelect(ventasProd))
                    if (ventaProdSelect(ventasProd))
                    {
                        mVVentaProd.listaVentasProdListColletion.Remove((ventas_de_productos)(datagridDevolProds.SelectedItem)); //el refresh es para el list collestion view, no las listas normales
                        MessageBox.Show("Elemento borrado", "GESTION DE DEVOLUCIONES", MessageBoxButton.OK, MessageBoxImage.Information);
                        datagridDevolProds.Items.Refresh();

                    }
                    else
                    {
                        MessageBox.Show("Error al borrar la venta del producto", "GESTION DE DEVOLUCIONES", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    
                }

            }
        }

        //Metodos para realizar los calculos cuando se ------------------ 
        //restan los componentes

        /// <summary>
        /// Metodo que resta un producto a una venta de producto
        /// </summary>
        /// <param name="ventProd">venta de producto a la cual se resta</param>
        /// <returns></returns>
        private bool quitarProductoVentaProd(ventas_de_productos ventProd)
        {
            bool correcto = true;

            ventas vent = new ventas();
            productos prod = new productos();
            //ventas_de_productos ventaProd = new ventas_de_productos();

            //Coger objetos
            prod = ventProd.productos;
            vent = ventProd.ventas;

            //Coger datos
            int cantidadVentaProd = (int)ventProd.cantidad_vendida;
            double precioUnidadesVentProd = (double)ventProd.precio_total_unidades;

            double precioProducto = (double)prod.precio;

            //Si es mayor que 1
            if (cantidadVentaProd > 1)
            {
                //restar la cantidad
                int cantidadRestada = cantidadVentaProd - 1;

                double precioUnidadesVentProdNuevo = precioProducto * cantidadRestada;

                double diferenciaResta = precioUnidadesVentProd - precioUnidadesVentProdNuevo ;

                double precioTotalVenta = (double)vent.total;
                double precioVentaTotalNuevo =  precioTotalVenta - diferenciaResta;

                double iva = (double) vent.iva;
                double totalMasIvaAct = 0;
                //sacar el IVA
                if (iva > 0)
                {
                    double compraIva = (precioVentaTotalNuevo * iva) / 100;
                    totalMasIvaAct = precioVentaTotalNuevo + compraIva;
                    
                }

                vent.total = precioVentaTotalNuevo;
                vent.total_mas_iva = totalMasIvaAct;
                ventProd.precio_total_unidades = precioUnidadesVentProdNuevo;
                ventProd.cantidad_vendida = cantidadRestada;

                //Editar ventas de producto y ventas
                correcto = mVVentaProd.editarVentaProd(ventProd);

                correcto = metodoEditarVenta(vent);
            }
            else
            {
                mVVentaProd.borrarVentaProdSelect(ventProd);
            }

            return correcto;
        }
        

        /// <summary>
        /// Metodo que quita una venta de producto
        /// </summary>
        /// <param name="ventProd"></param>
        /// <returns></returns>
        private bool ventaProdSelect(ventas_de_productos ventProd)
        {
            bool correcto = true;
            ventas vent = new ventas();
            vent = ventProd.ventas;

            //Obtener datos de la venta y ventas de productos
            double totalVenta = (double)vent.total;
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
            
            //Borrar el objeto de la venta de producto
            correcto = mVVentaProd.borrarVentaProd(ventProd);

            //modificar el objeto de la venta en la base de datos
            
            correcto = metodoEditarVenta(vent);

            return correcto;
        }
        
        /// <summary>
        /// Metodo de editar una venta
        /// </summary>
        /// <param name="vent">Recibe una venta</param>
        /// <returns>Valor booleano que indica 
        /// si la operacion ha ido bien</returns>
        private bool metodoEditarVenta(ventas vent)
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
                    //Console.Write(ex);
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

        //Filtros ----------------------------------------

        /// <summary>
        /// Evento del filtro buscar por nombre
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtBuscarProd_TextChanged(object sender, TextChangedEventArgs e)
        {
            mVVentaProd.listaVentasProdsDatagridListCollect.Filter = predicadoNombreProd;
        }

        /// <summary>
        /// Metodo del filtro del nombre
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Retorna los elementos filtrados</returns>
        private bool filtroNombreProd(object obj)
        {
            bool correcto = true;
            productos prod;

            if (obj != null)
            {
                ventas_de_productos ventaProd = (ventas_de_productos) obj;
                
                prod = ventaProd.productos; //Nota: en el if se pasa todo a mayusculas para que cuando busque, no distinga mayusculas de minusculas
                if (prod.descripcion == null || !prod.descripcion.ToUpper().StartsWith(txtBuscarProd.Text.ToUpper()))
                {
                    correcto = false;
                }

            }
            else
            {
                correcto = false;
            }

            return correcto;

        }

        /// <summary>
        /// Evento del filtro del boton de fechas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFiltroFecha_Click(object sender, RoutedEventArgs e)
        {
            if (mVVentaProd.fechaFinal >= mVVentaProd.fechaInicial)
            {
                datagridDevolProds.Items.Filter = new Predicate<object>(mVVentaProd.filtroFechas);
            }
            else
            {
                MessageBox.Show("La fecha inicial no puede ser mayor que la final", "GESTION DE DEVOLUCIONES", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Evento que quita todos los filtros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuitarFiltros_Click(object sender, RoutedEventArgs e)
        {
            mVVentaProd.listaVentasProdsDatagridListCollect.Filter = null;
        }
    }
}
