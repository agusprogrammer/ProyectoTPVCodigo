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
    /// Servicio de tipos de producto
    /// </summary>
    public class Tipos_De_ProductosServicio : ServicioGenerico<tipos_de_productos>
    {
        private DbContext contexto;     //contexto de la base de datos

        /// <summary>
        /// Constructor de ProductosServicio
        /// </summary>
        /// <param name="context">contexto de la base de datos por parametro</param>
        public Tipos_De_ProductosServicio(DbContext context) : base(context)
        {
            contexto = context;
        }
    }
}
