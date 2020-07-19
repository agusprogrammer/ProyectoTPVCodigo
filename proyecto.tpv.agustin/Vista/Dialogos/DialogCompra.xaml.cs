using MahApps.Metro.Controls;
using proyecto.tpv.agustin.Modelo;
using proyecto.tpv.agustin.MVVM;
using proyecto.tpv.agustin.Servicios;
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
using System.Windows.Shapes;

namespace proyecto.tpv.agustin.Vista.Dialogos
{
    /// <summary>
    /// Lógica de interacción para DialogCompra.xaml
    /// </summary>
    public partial class DialogCompra : MetroWindow
    {
        private proyecto_tpvEntities tpvEnt;    //Conexion con la base de datos
        private usuarios usuLogueado;           //Usuario que ha iniciado sesion
        private MVVentas mVVentas;              //MV de ventas

        private List<double> listaIva;          //Lista de valores del IVA
        private double totalCompra;     //Total de la compra
        private double totalCompraIva;  //Total de la compra + iva
        private double iva;             //Valor del IVA seleccionado
        
        private List<ventas_de_productos> listaVentasProdCompra;    //Lista de los productos

        /// <summary>
        /// Constructor de DialogCompra
        /// </summary>
        /// <param name="ent">Conexion con la base de datos por parametro</param>
        /// <param name="usu">Usuario que ha iniciado sesion</param>
        /// <param name="totalC">Le pasamos el total de la compra que ha sido calcualdo anteriormente</param>
        /// <param name="listaVentasProdC">Le pasamos la lista de ventas de productos</param>
        public DialogCompra(proyecto_tpvEntities ent, usuarios usu, double totalC, List<ventas_de_productos> listaVentasProdC)
        {
            InitializeComponent();
            tpvEnt = ent;

            mVVentas = new MVVentas(tpvEnt);
            DataContext = mVVentas;

            usuLogueado = new usuarios();
            usuLogueado = usu;
            comboUsuarioCompra.SelectedItem = usuLogueado;

            listaVentasProdCompra = listaVentasProdC;
            totalCompra = totalC;

            txtTotal.Text = totalCompra.ToString();
            txtTotalIva.Text = totalCompra.ToString();
            iva = 0;
            
            inicializarListaIva();
            
            //Deshabilitar boton de guardar para la validacion
            this.AddHandler(Validation.ErrorEvent, new RoutedEventHandler(mVVentas.OnErrorEvent));
            mVVentas.btnGuardar = btnComprarCompra; //Ver si es asi

        }

        /// <summary>
        /// Metodo para inicializar la lista de IVAs del combo
        /// </summary>
        private void inicializarListaIva()
        {
            listaIva = new List<double>() {0,4,5,6,10,12,15,20,21,25,30};
            
            txtComboIVACompra.ItemsSource = listaIva;
            txtComboIVACompra.SelectedIndex = 0;
            txtComboIVACompra.SelectedItem = listaIva[0];
        }
        
        /// <summary>
        /// Evento para salir del formulario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelarCompra_Click(object sender, RoutedEventArgs e)
        {
            
            this.Close();
        }

        /// <summary>
        /// Evento para realizar una compra
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnComprarCompra_Click(object sender, RoutedEventArgs e)
        {
            
            mVVentas.pasarTotalesVenta(totalCompra, totalCompraIva, iva);

            if (mVVentas.guardaVenta())
            {
                //MessageBox.Show("Insercción de la venta ralizada correctamente", "GESTION DE VENTAS", MessageBoxButton.OK, MessageBoxImage.Information);

                mVVentas.setListaProductos(listaVentasProdCompra);

                if (mVVentas.cargarVentasDeProd())
                {
                    MessageBox.Show("Insercción de la venta ralizada correctamente", "GESTION DE VENTAS", MessageBoxButton.OK, MessageBoxImage.Information);
                    
                    DialogMostrarFactura diagFactura = new DialogMostrarFactura(tpvEnt);
                    diagFactura.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error a insertar las ventas de productos", "GESTION DE VENTAS", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
            else
            {
                MessageBox.Show("Error al realizar la insercción de la venta", "GESTION DE VENTAS", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            
        }
        
        /// <summary>
        /// Metodo para seleccionar un IVA del combo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtComboIVACompra_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            String numIvaString = "";
            numIvaString = txtComboIVACompra.SelectedItem.ToString();

            double numIva = Convert.ToDouble(numIvaString);
            sacarIva(numIva);

            iva = numIva;
            
        }
        

        /// <summary>
        /// Metodo para sacar el iva
        /// </summary>
        /// <param name="numIva"></param>
        private void sacarIva(double numIva)
        {
            
            totalCompraIva = totalCompra;

            if (numIva > 0)
            {

                double compraIva = (totalCompra * numIva) / 100;
                totalCompraIva = totalCompraIva + compraIva;

                //Truncar
                totalCompraIva = Math.Truncate(totalCompraIva * 100) / 100;
            }

            txtTotalIva.Text = totalCompraIva.ToString();
            
        }
        


    }
}
