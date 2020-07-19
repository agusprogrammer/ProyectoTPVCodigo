using MahApps.Metro.Controls;
using proyecto.tpv.agustin.Modelo;
using proyecto.tpv.agustin.Vista.ControlesUsuario;
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
    /// Lógica de interacción para DialogAdminProg.xaml
    /// </summary>
    public partial class DialogAdminProg : MetroWindow
    {
        private proyecto_tpvEntities tpvEnt; //Conexion con la base de datos

        /// <summary>
        /// Constructor DialogAdminProg
        /// </summary>
        /// <param name="ent">Conexion con la base de datos por parametro</param>
        public DialogAdminProg(proyecto_tpvEntities ent)
        {
            InitializeComponent();
            tpvEnt = ent;
        }

        /// <summary>
        /// evento para el boton de devoluciones de ventas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDevolucion_Click(object sender, RoutedEventArgs e)
        {
            UCDevoluciones ucDevoluciones = new UCDevoluciones(tpvEnt);
            gridAdministracion.Children.Clear();
            gridAdministracion.Children.Add(ucDevoluciones);
        }

        /// <summary>
        /// boton para salir del dialogo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSalirAdmin_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// boton para devolver productos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDevolucionProd_Click(object sender, RoutedEventArgs e)
        {
            UCDevolucionesProd uCDevolucionesProd = new UCDevolucionesProd(tpvEnt);
            gridAdministracion.Children.Clear();
            gridAdministracion.Children.Add(uCDevolucionesProd);
        }

        /// <summary>
        /// Boton para mostrar el grafico por fechas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChartVentas_Click(object sender, RoutedEventArgs e)
        {
            UCGraficoVentas uCGraf = new UCGraficoVentas(tpvEnt);
            gridAdministracion.Children.Clear();
            gridAdministracion.Children.Add(uCGraf);
        }
    }
}
