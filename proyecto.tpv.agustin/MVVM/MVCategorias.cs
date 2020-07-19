using proyecto.tpv.agustin.Modelo;
using proyecto.tpv.agustin.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto.tpv.agustin.MVVM
{
    /// <summary>
    /// MVVM de Categorias
    /// </summary>
    public class MVCategorias : MVBase
    {
        private proyecto_tpvEntities tpvEnt;    //Conexion con la base de datos
        private CategoriasServicio catServ;     //Servicio de categorias

        /// <summary>
        /// Constructor MVVM de Categorias
        /// </summary>
        /// <param name="ent">Conexion con la base de datos pasada por parametro</param>
        public MVCategorias(proyecto_tpvEntities ent)
        {
            tpvEnt = ent;
            catServ = new CategoriasServicio(tpvEnt);
        }

        /// <summary>
        /// Obtener la lista de categorias a partir de un codigo 
        /// de tipo de producto para poder hacer
        /// </summary>
        /// <param name="codTipoProd">Pasar el codigo del tipo de 
        /// producto para obtener su lista de categorias</param>
        /// <returns>retorna la lista de List<categorias></returns>
        public List<categorias> listaPorCategoria(int codTipoProd)
        {
            return catServ.listaCategoriaPorTipoProd(codTipoProd);
        }


    }
}
