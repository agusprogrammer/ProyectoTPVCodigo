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
    /// Servicio de productos
    /// </summary>
    public class ProductosServicio : ServicioGenerico<productos>
    {
        private DbContext contexto;     //contexto de la base de datos

        /// <summary>
        /// Constructor de ProductosServicio
        /// </summary>
        /// <param name="context">contexto de la base de datos por parametro</param>
        public ProductosServicio(DbContext context) : base(context)
        {
            contexto = context;
        }

        /// <summary>
        /// Sentencia lambda que obtiene la 
        /// lista de productos a partir de
        /// un codigo de categoria
        /// </summary>
        /// <param name="cod">Codigo de categoria</param>
        /// <returns>Devuelve una lista de productos</returns>
        public List<productos> listaProductosPorCategoria(int cod)
        {
            return contexto.Set<productos>().Where(c => c.cod_categoria == cod).ToList();
        }

    }
}
