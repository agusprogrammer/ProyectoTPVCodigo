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
    /// Servicio de roles
    /// </summary>
    public class RolesServicio : ServicioGenerico<roles>
    {
        private DbContext contexto; //contexto de la base de datos

        /// <summary>
        /// Constructor de RolesServicio
        /// </summary>
        /// <param name="context">contexto de la base de datos por parametro</param>
        public RolesServicio(DbContext context) : base(context)
        {
            contexto = context;
        }
    }
}
