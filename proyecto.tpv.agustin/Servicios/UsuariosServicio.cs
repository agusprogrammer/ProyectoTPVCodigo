using proyecto.tpv.agustin.Modelo;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto.tpv.agustin.Servicios
{
    /// <summary>
    /// Servicio de usuarios
    /// </summary>
    public class UsuariosServicio : ServicioGenerico<usuarios>
    {
        private DbContext contexto;     //contexto de la base de datos

        /*
         Variable con getter y setter
         Se alamcena el usuario que ha iniciado sesion
        */
        public usuarios usuLogin { get; set; }

        /// <summary>
        /// Constructor de UsuariosServicio
        /// </summary>
        /// <param name="context">contexto de la base de datos por parametro</param>
        public UsuariosServicio(DbContext context) : base(context)
        {
            contexto = context;
        }

        /// <summary>
        /// Metodo que comprueba las credenciales del usuario
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public Boolean login(String user, String pass)
        {
            Boolean correcto = true;

            try
            {
                //Obtenemos el usuario mediante una select
                usuLogin = contexto.Set<usuarios>().Where(u => u.usuario == user).FirstOrDefault();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.StackTrace);
            }

            if (usuLogin == null)
            {
                correcto = false;

            }
            else if (!usuLogin.usuario.Equals(user) || !usuLogin.contrasenya.Equals(pass))
            {
                correcto = false;
            }

            return correcto;
        }

    }
}
