using proyecto.tpv.agustin.Modelo;
using proyecto.tpv.agustin.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto.tpv.agustin.MVVM
{
    /// <summary>
    /// MVVM de Usuarios
    /// </summary>
    public class MVUsuarios : MVBase
    {

        private proyecto_tpvEntities tpvEnt;    //Conexion con la base de datos
        private usuarios usuLogueado;           //Usuario que tiene la sesion iniciada en este momento
        private UsuariosServicio usuServ;       //Servicio de usuarios

        /// <summary>
        /// Constructor MVVM de Usuarios
        /// </summary>
        /// <param name="ent">Conexion con la base de datos pasada por parametro</param>
        public MVUsuarios(proyecto_tpvEntities ent)
        {
            tpvEnt = ent;
            inicializa();
        }

        /// <summary>
        /// Metodo para inicializar los objetos 
        /// y variables del MVVM del MV de usuarios
        /// </summary>
        private void inicializa()
        {
            usuLogueado = new usuarios();
            usuServ = new UsuariosServicio(tpvEnt);
        }
        
        /// <summary>
        /// Variable que nos permite devolver o poner el usuario 
        /// que ha iniciado sesion
        /// </summary>
        public usuarios usuLogueadoSeleccionado
        {
            get
            {
                return usuLogueado;
            }
            set
            {
                usuLogueado = value;
                OnPropertyChanged("usuLogueadoSeleccionado");
            }
        }


    }
}
