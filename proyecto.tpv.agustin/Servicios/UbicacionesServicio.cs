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
    /// Servicio de ubicaciones
    /// </summary>
    public class UbicacionesServicio : ServicioGenerico<ubicaciones>
    {
        private DbContext contexto;     //contexto de la base de datos

        /// <summary>
        /// Constructor de UbicacionesServicio
        /// </summary>
        /// <param name="context">contexto de la base de datos por parametro</param>
        public UbicacionesServicio(DbContext context) : base(context)
        {
            contexto = context;
        }
    }
}
