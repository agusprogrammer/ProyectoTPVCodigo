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
    /// Servicio de productos y ubicaciones
    /// </summary>
    public class Productos_Y_UbicacionesServicio : ServicioGenerico<productos_y_ubicaciones>
    {
        private DbContext contexto;     //contexto de la base de datos

        /// <summary>
        /// Constructor de Productos_Y_UbicacionesServicio
        /// </summary>
        /// <param name="context">contexto de la base de datos por parametro</param>
        public Productos_Y_UbicacionesServicio(DbContext context) : base(context)
        {
            contexto = context;
        }
    }
}
