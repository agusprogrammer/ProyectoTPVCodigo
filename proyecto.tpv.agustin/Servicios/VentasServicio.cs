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
    /// Servicio de ventas
    /// </summary>
    public class VentasServicio : ServicioGenerico<ventas>
    {
        private DbContext contexto;     //contexto de la base de datos

        /// <summary>
        /// Constructor de VentasServicio
        /// </summary>
        /// <param name="context">contexto de la base de datos por parametro</param>
        public VentasServicio(DbContext context) : base(context)
        {
            contexto = context;
        }

        /// <summary>
        /// Sentencia lambda que obtiene a 
        /// partir de la ultima venta el codigo de la venta
        /// </summary>
        /// <returns>Devuelve el codigo de la ultima venta</returns>
        public int getLastId()
        {
            ventas ven = contexto.Set<ventas>().OrderByDescending(v => v.cod_venta).FirstOrDefault();
            return ven.cod_venta;
        }

        /// <summary>
        /// Sentencia lambda que obtiene ultima venta
        /// </summary>
        /// <returns>Devuelve la ultima venta</returns>
        public ventas getLastVenta()
        {
            ventas ven = contexto.Set<ventas>().OrderByDescending(v => v.cod_venta).FirstOrDefault();
            return ven;
        }

    }
}
