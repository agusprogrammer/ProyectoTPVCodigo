using MahApps.Metro.Controls;
using proyecto.tpv.agustin.Modelo;
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
    /// Lógica de interacción para LoginDialog.xaml
    /// </summary>
    public partial class LoginDialog : MetroWindow
    {

        /// <summary>
        /// Entities del proyecto, contexto de la base de datos
        /// </summary>
        private proyecto_tpvEntities tpvEnt;

        /// <summary>
        /// Servicio de usuario, accede a las 
        /// selects del programa (las consultas sobre usuario)
        /// Variable que nos permite tabajarar con la tabla 
        /// usuarios de nuestra base de datos
        /// </summary>
        private UsuariosServicio usuServ;

        //Variable que recoge el usuario desde el servicio
        private usuarios usuLogueado;

        /// <summary>
        /// Constructor del login dialog, inicializa el login dialog
        /// </summary>
        public LoginDialog()
        {
            InitializeComponent();

            //Inicializar variables
            tpvEnt = new proyecto_tpvEntities(); //inicializa el contexto de la base de datos
            usuServ = new UsuariosServicio(tpvEnt); //Inicializamos el servicio de usuario
            usuLogueado = new usuarios(); //Inicializamos a new el usuario a loguearse

        }

        /// <summary>
        /// Evento para entrar dentro del programa mediante un usuario
        /// y una contrasenya
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEntrar_Click(object sender, RoutedEventArgs e)
        {
            String usu = txtLogin.Text;
            String pass = passLogin.Password;

            if (usu.Equals("") || pass.Equals(""))
            {
                txtLogin.BorderBrush = Brushes.Red;
                passLogin.BorderBrush = Brushes.Red;
                MessageBox.Show("El usuario y/o contraseña estan vacios", "LOGIN", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                //Comprobar usuario que sea correcto
                if (usuServ.login(usu, pass))
                {
                    //invEnt, usuServ.usuLogin
                    //Coger el usuario desde el servicio de usuario
                    usuLogueado = usuServ.usuLogin;
                    MainWindow ventanaPrincipal = new MainWindow(tpvEnt, usuLogueado);
                    ventanaPrincipal.Show();
                    this.Close();
                }
                else
                {
                    txtLogin.BorderBrush = Brushes.Red;
                    passLogin.BorderBrush = Brushes.Red;
                    MessageBox.Show("Error en el usuario y/o contraseña incorrectos", "LOGIN", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

        }

        /// <summary>
        /// Evento para salir del programa con los botones de salida
        /// Botones de salida:
        /// x:Name="btnSalir"
        /// x:Name="btnSalirSuperior"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
    }
}
