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
    /// Servicio de permisos
    /// </summary>
    public class PermisosServicio : ServicioGenerico<permisos>
    {
        private DbContext contexto;     //contexto de la base de datos

        /// <summary>
        /// Constructor de PermisosServicio
        /// </summary>
        /// <param name="context">contexto de la base de datos por parametro</param>
        public PermisosServicio(DbContext context) : base(context)
        {
            contexto = context;
        }
    }
}
