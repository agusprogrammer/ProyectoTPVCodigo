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
    /// Servicio de categorias
    /// </summary>
    public class CategoriasServicio : ServicioGenerico<categorias>
    {
        private DbContext contexto;     //contexto de la base de datos

        /// <summary>
        /// Constructor de CategoriasServicio
        /// </summary>
        /// <param name="context">contexto de la base de datos por parametro</param>
        public CategoriasServicio(DbContext context) : base(context)
        {
            contexto = context;
        }

        /// <summary>
        /// Sentencia lambda que obtine una lista de 
        /// categorias a partir de un codigo de tipo de producto
        /// </summary>
        /// <param name="cod">codigo del tipo de producto</param>
        /// <returns>Devuelve una lista de categorias</returns>
        public List<categorias> listaCategoriaPorTipoProd(int cod)
        {
            return contexto.Set<categorias>().Where(t => t.cod_tipo_producto == cod).ToList();
        }
    }
}
