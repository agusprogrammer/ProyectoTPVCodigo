using NLog;
using proyecto.tpv.agustin.Modelo;
using proyecto.tpv.agustin.MVVM;
using System;
using System.Collections.Generic;
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
    /// Lógica de interacción para UCDevoluciones.xaml
    /// </summary>
    public partial class UCDevoluciones : UserControl
    {
        private proyecto_tpvEntities tpvEnt;    //Conexion con la base de datos
        private MVVentas mVVentas;              //MV de ventas
        
        private List<ventas_de_productos> listaVentaProd;   //lista de ventas de productos

        private static Logger log = LogManager.GetCurrentClassLogger();     //Variable del logger para recoger los errores e informacion del programa

        /// <summary>
        /// Constructor de UCDevoluciones
        /// </summary>
        /// <param name="ent">conexion que le pasamos como parametro</param>
        public UCDevoluciones(proyecto_tpvEntities ent)
        {
            InitializeComponent();
            tpvEnt = ent;
            mVVentas = new MVVentas(tpvEnt);
            DataContext = mVVentas;

            listaVentaProd = new List<ventas_de_productos>();
            
        }
        
        /// <summary>
        /// Evento del boton de quitar filtros
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuitarFiltros_Click(object sender, RoutedEventArgs e)
        {
            mVVentas.listaVentasListColletion.Filter = null;
        }

        
        /// <summary>
        /// Evento del boton para filtrar por fecha
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFiltroFecha_Click(object sender, RoutedEventArgs e)
        {
            if (mVVentas.fechaFinal >= mVVentas.fechaInicial)
            {
                datagridDevol.Items.Filter = new Predicate<object>(mVVentas.filtroFechas);
            }
            else
            {
                MessageBox.Show("La fecha inicial no puede ser mayor que la final", "GESTION DE DEVOLUCIONES", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        

        /// <summary>
        /// Evento del boton para Devolver una venta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRestaVenta_Click(object sender, RoutedEventArgs e)
        {
            if (datagridDevol.SelectedItem != null)
            {
                if (datagridDevol.SelectedItem is ventas)
                {
                    MessageBoxResult resultado = MessageBox.Show("¿Quieres borrar la venta?", "GESTION DE DEVOLUCIONES", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (resultado == MessageBoxResult.Yes)
                    {

                        ventas vent = (ventas)(datagridDevol.SelectedItem);
                        if (vent.ventas_de_productos.Count == 0)
                        {
                            if (mVVentas.borrarVenta(vent))
                            {
                                mVVentas.listaVentasListColletion.Remove((ventas)(datagridDevol.SelectedItem)); //el refresh es para el list collestion view, no las listas normales
                                MessageBox.Show("Elemento borrado", "GESTION DE DEVOLUCIONES", MessageBoxButton.OK, MessageBoxImage.Information);
                                datagridDevol.Items.Refresh();
                            }
                            else
                            {
                                MessageBox.Show("Error al borrar la venta", "GESTION DE DEVOLUCIONES", MessageBoxButton.OK, MessageBoxImage.Error);
                            }

                        }
                        else
                        {
                            
                            listaVentaProd = vent.ventas_de_productos.Cast<ventas_de_productos>().ToList();
                            bool correcto = true;

                            //borrar las ventas de productos
                            for (int i = 0; i < listaVentaProd.Count; i++)
                            {
                                ventas_de_productos ventaProd = listaVentaProd[i];
                                
                                if (mVVentas.borrarVentaProd(ventaProd) != true)
                                {
                                    correcto = false;
                                    
                                }
                                
                            }

                            //Borrar la venta
                            if (correcto == true)
                            {
                                if (mVVentas.borrarVenta(vent))
                                {
                                    mVVentas.listaVentasListColletion.Remove((ventas)(datagridDevol.SelectedItem)); //el refresh es para el list collestion view, no las listas normales
                                
                                    //Ventas de productos
                                    MessageBox.Show("Venta devuelta correctamente", "GESTION DE DEVOLUCIONES", MessageBoxButton.OK, MessageBoxImage.Information);
                                    datagridDevol.Items.Refresh();
                                }
                                else
                                {
                                    MessageBox.Show("Error al devolver la venta", "GESTION DE DEVOLUCIONES", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                                
                            }
                            else
                            {
                                MessageBox.Show("Error al quitar los productos a la venta", "GESTION DE DEVOLUCIONES", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Esto no es una venta", "GESTION DE DEVOLUCIONES", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        


    }
}
