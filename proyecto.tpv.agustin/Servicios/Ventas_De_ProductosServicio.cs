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
    /// Servicio de ventas de productos
    /// </summary>
    public class Ventas_De_ProductosServicio : ServicioGenerico<ventas_de_productos>
    {
        private DbContext contexto;     //contexto de la base de datos

        /// <summary>
        /// Constructor de Ventas_De_ProductosServicio
        /// </summary>
        /// <param name="context">contexto de la base de datos por parametro</param>
        public Ventas_De_ProductosServicio(DbContext context) : base(context)
        {
            contexto = context;
        }
        
    }
}
