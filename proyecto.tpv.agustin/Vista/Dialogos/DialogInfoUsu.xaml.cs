using MahApps.Metro.Controls;
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
using System.Windows.Shapes;

namespace proyecto.tpv.agustin.Vista.Dialogos
{
    /// <summary>
    /// Lógica de interacción para DialogInfoUsu.xaml
    /// </summary>
    public partial class DialogInfoUsu : MetroWindow
    {
        private proyecto_tpvEntities tpvEnt;    //Conexion con la base de datos

        private usuarios usuLogueado;       //Usuario que ha iniciado la sesion
        private MVUsuarios mvUsu;           //MV de usuarios

        /// <summary>
        /// Constructor de DialogInfoUsu
        /// </summary>
        /// <param name="ent">Conexion con la base de datos por parametro</param>
        /// <param name="usu">Usuario que ha iniciado la sesion por parametro</param>
        public DialogInfoUsu(proyecto_tpvEntities ent, usuarios usu)
        {
            InitializeComponent();
            tpvEnt = ent;
            usuLogueado = usu;
            mvUsu = new MVUsuarios(tpvEnt);
            DataContext = mvUsu;
            
            mvUsu.usuLogueadoSeleccionado = usu;
        }

        /// <summary>
        /// Evento para salir del dialogo de usuario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSalirSuperior_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
