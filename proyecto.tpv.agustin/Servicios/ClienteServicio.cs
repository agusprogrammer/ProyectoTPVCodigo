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
    /// Servicio de clientes
    /// </summary>
    public class ClienteServicio : ServicioGenerico<clientes>
    {
        private DbContext contexto;     //contexto de la base de datos

        /// <summary>
        /// Constructor de ClienteServicio
        /// </summary>
        /// <param name="context">contexto de la base de datos por parametro</param>
        public ClienteServicio(DbContext context) : base(context)
        {
            contexto = context;
        }

    }
}
