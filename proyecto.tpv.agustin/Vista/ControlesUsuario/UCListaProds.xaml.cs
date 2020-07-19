using proyecto.tpv.agustin.Modelo;
using proyecto.tpv.agustin.MVVM;
using proyecto.tpv.agustin.Vista.Dialogos;
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
    /// Lógica de interacción para UCListaProds.xaml
    /// </summary>
    public partial class UCListaProds : UserControl
    {
        private proyecto_tpvEntities tpvEnt;        //Conexion de la base de datos
        private MVVentasDeProductos mVVentaProd;    //MV de ventas de productos
        private List<ventas_de_productos> listaVentaProdCompra; //lista de ventas de productos
        private ventas_de_productos ventaProd;      //Objeto de ventas de productos

        private usuarios usuLogueado;       //Usuario que ha iniciado sesion

        /// <summary>
        /// Constructor UCListaProds
        /// </summary>
        /// <param name="ent">Conexion de la base de datos que le pasamos por parametro</param>
        /// <param name="mVVentaP">MV de ventas de producto que le pasamos por parametro</param>
        /// <param name="usu">Usuario que ha iniciado sesion pasado por parametro</param>
        public UCListaProds(proyecto_tpvEntities ent, MVVentasDeProductos mVVentaP, usuarios usu)
        {
            InitializeComponent();
            tpvEnt = ent;
            mVVentaProd = mVVentaP;
            DataContext = mVVentaProd;
            
            listaVentaProdCompra = new List<ventas_de_productos>();
            ventaProd = new ventas_de_productos();
            usuLogueado = new usuarios();
            usuLogueado = usu;
            obtenerTotal();
            
        }

        /// <summary>
        /// Metodo para obtener el precio total
        /// de la venta
        /// </summary>
        private void obtenerTotal()
        {
            listaVentaProdCompra = mVVentaProd.listaVentasProdsDatagrid;
            double precio_total_unidades = 0;

            for (int i = 0; i < listaVentaProdCompra.Count(); i++)
            {
                ventaProd = listaVentaProdCompra[i];
                precio_total_unidades = precio_total_unidades + ventaProd.precio_total_unidades.GetValueOrDefault();
            }

            txtTotal.Text = precio_total_unidades.ToString();
        }

        /// <summary>
        /// Evento para quitar el producto seleccionado 
        /// en el datagrid de lal ista de la venta
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuitarProd_Click(object sender, RoutedEventArgs e)
        {
            //Quitar un producto
            ventas_de_productos vent = (ventas_de_productos) dataGridVentasProd.SelectedItem;

            if (dataGridVentasProd.SelectedItem != null)
            {
                if (dataGridVentasProd.SelectedItem is ventas_de_productos)
                {
                    mVVentaProd.quitarProducto((ventas_de_productos)dataGridVentasProd.SelectedItem);
                    dataGridVentasProd.Items.Refresh();
                    obtenerTotal();
                }
                else
                {
                    MessageBox.Show("Esto no es una venta de un producto", "LISTA COMPRA", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                
            }
            else
            {
                MessageBox.Show("Tienes que seleccionar un elemento de la lista", "LISTA COMPRA", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
        }

        /// <summary>
        /// Evento para quitar todos 
        /// los productos de la lista
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuitarTodos_Click(object sender, RoutedEventArgs e)
        {
            //dataGridVentasProd.Items.Clear();
            mVVentaProd.vaciarListaProductos();
            dataGridVentasProd.Items.Refresh();

            //Poner 0 el total
            txtTotal.Text = "0";
        }
        
        /// <summary>
        /// Metodo para refrescar la lista despues de la inserccion de los productos
        /// </summary>
        public void actualizarLista()
        {
            dataGridVentasProd.Items.Refresh();
        }
        
        /// <summary>
        /// Evento del boton para comprar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnComprar_Click(object sender, RoutedEventArgs e)
        {
            String stringTotal = txtTotal.Text.ToString();

            double totalCompra = double.Parse(stringTotal);

            if (listaVentaProdCompra.Count != 0 && listaVentaProdCompra != null)
            {
                DialogCompra diagCompra = new DialogCompra(tpvEnt, usuLogueado, totalCompra, listaVentaProdCompra);
                diagCompra.ShowDialog();
            }
            else
            {
                MessageBox.Show("No puedes hacer una venta sin productos", "GESTION DE VENTAS", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

        }

        /// <summary>
        /// Evento para smar uno o mas productos por numero
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalc_1_Click(object sender, RoutedEventArgs e)
        {
            
            if (dataGridVentasProd.SelectedItem is ventas_de_productos)
            {
                ventas_de_productos venProd = (ventas_de_productos) dataGridVentasProd.SelectedItem;
                venProd.cantidad_vendida = venProd.cantidad_vendida + 1;

                productos prod = venProd.productos;
                venProd.precio_total_unidades = venProd.precio_total_unidades + prod.precio;

                dataGridVentasProd.SelectedItem = venProd;
                dataGridVentasProd.Items.Refresh();
                obtenerTotal();

            }

        }

        /// <summary>
        /// Evento para smar uno o mas productos por numero
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalc_2_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridVentasProd.SelectedItem is ventas_de_productos)
            {
                ventas_de_productos venProd = (ventas_de_productos)dataGridVentasProd.SelectedItem;
                venProd.cantidad_vendida = venProd.cantidad_vendida + 2;

                productos prod = venProd.productos;
                venProd.precio_total_unidades = venProd.precio_total_unidades + (prod.precio * 2);

                dataGridVentasProd.SelectedItem = venProd;
                dataGridVentasProd.Items.Refresh();
                obtenerTotal();

            }
        }

        /// <summary>
        /// Evento para smar uno o mas productos por numero
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalc_3_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridVentasProd.SelectedItem is ventas_de_productos)
            {
                ventas_de_productos venProd = (ventas_de_productos)dataGridVentasProd.SelectedItem;
                venProd.cantidad_vendida = venProd.cantidad_vendida + 3;

                productos prod = venProd.productos;
                venProd.precio_total_unidades = venProd.precio_total_unidades + (prod.precio * 3);

                dataGridVentasProd.SelectedItem = venProd;
                dataGridVentasProd.Items.Refresh();
                obtenerTotal();

            }
        }

        /// <summary>
        /// Evento para smar uno o mas productos por numero
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalc_4_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridVentasProd.SelectedItem is ventas_de_productos)
            {
                ventas_de_productos venProd = (ventas_de_productos)dataGridVentasProd.SelectedItem;
                venProd.cantidad_vendida = venProd.cantidad_vendida + 4;

                productos prod = venProd.productos;
                venProd.precio_total_unidades = venProd.precio_total_unidades + (prod.precio * 4);

                dataGridVentasProd.SelectedItem = venProd;
                dataGridVentasProd.Items.Refresh();
                obtenerTotal();

            }
        }

        /// <summary>
        /// Evento para smar uno o mas productos por numero
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalc_5_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridVentasProd.SelectedItem is ventas_de_productos)
            {
                ventas_de_productos venProd = (ventas_de_productos)dataGridVentasProd.SelectedItem;
                venProd.cantidad_vendida = venProd.cantidad_vendida + 5;

                productos prod = venProd.productos;
                venProd.precio_total_unidades = venProd.precio_total_unidades + (prod.precio * 5);

                dataGridVentasProd.SelectedItem = venProd;
                dataGridVentasProd.Items.Refresh();
                obtenerTotal();

            }
        }

        /// <summary>
        /// Evento para smar uno o mas productos por numero
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalc_6_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridVentasProd.SelectedItem is ventas_de_productos)
            {
                ventas_de_productos venProd = (ventas_de_productos)dataGridVentasProd.SelectedItem;
                venProd.cantidad_vendida = venProd.cantidad_vendida + 6;

                productos prod = venProd.productos;
                venProd.precio_total_unidades = venProd.precio_total_unidades + (prod.precio * 6);

                dataGridVentasProd.SelectedItem = venProd;
                dataGridVentasProd.Items.Refresh();
                obtenerTotal();

            }
        }

        /// <summary>
        /// Evento para smar uno o mas productos por numero
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalc_7_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridVentasProd.SelectedItem is ventas_de_productos)
            {
                ventas_de_productos venProd = (ventas_de_productos)dataGridVentasProd.SelectedItem;
                venProd.cantidad_vendida = venProd.cantidad_vendida + 7;

                productos prod = venProd.productos;
                venProd.precio_total_unidades = venProd.precio_total_unidades + (prod.precio * 7);

                dataGridVentasProd.SelectedItem = venProd;
                dataGridVentasProd.Items.Refresh();
                obtenerTotal();

            }
        }

        /// <summary>
        /// Evento para smar uno o mas productos por numero
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalc_8_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridVentasProd.SelectedItem is ventas_de_productos)
            {
                ventas_de_productos venProd = (ventas_de_productos)dataGridVentasProd.SelectedItem;
                venProd.cantidad_vendida = venProd.cantidad_vendida + 8;

                productos prod = venProd.productos;
                venProd.precio_total_unidades = venProd.precio_total_unidades + (prod.precio * 8);

                dataGridVentasProd.SelectedItem = venProd;
                dataGridVentasProd.Items.Refresh();
                obtenerTotal();

            }
        }

        /// <summary>
        /// Evento para smar uno o mas productos por numero
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalc_9_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridVentasProd.SelectedItem is ventas_de_productos)
            {
                ventas_de_productos venProd = (ventas_de_productos)dataGridVentasProd.SelectedItem;
                venProd.cantidad_vendida = venProd.cantidad_vendida + 9;

                productos prod = venProd.productos;
                venProd.precio_total_unidades = venProd.precio_total_unidades + (prod.precio * 9);

                dataGridVentasProd.SelectedItem = venProd;
                dataGridVentasProd.Items.Refresh();
                obtenerTotal();

            }
        }
    }
}
