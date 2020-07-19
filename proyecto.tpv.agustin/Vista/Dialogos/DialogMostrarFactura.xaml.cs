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
    /// Lógica de interacción para DialogMostrarFactura.xaml
    /// </summary>
    public partial class DialogMostrarFactura : MetroWindow
    {
        private proyecto_tpvEntities tpvEnt;    //Conexion con la base de datos

        /// <summary>
        /// Constructor de DialogMostrarFactura
        /// </summary>
        /// <param name="ent">Conexion con la base de datos por parametro</param>
        public DialogMostrarFactura(proyecto_tpvEntities ent)
        {
            InitializeComponent();
            tpvEnt = ent;
        }

        /// <summary>
        /// Evento para salir del dialogo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSalirSuperiorFact_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Evento para Emitir la factura
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEmitirFactura_Click(object sender, RoutedEventArgs e)
        {
            UCFactura ucFactInform = new UCFactura(tpvEnt);
            gridFactura.Children.Clear();
            gridFactura.Children.Add(ucFactInform);
        }
    }
}
