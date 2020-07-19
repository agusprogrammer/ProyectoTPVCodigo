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
    /// MVVM de tipos de Productos
    /// </summary>
    public class MVTiposProductos : MVBase
    {
        private proyecto_tpvEntities tpvEnt;                //Conexion con la base de datos
        private Tipos_De_ProductosServicio tipoProdServ;    //Servicio de tipos de productos

        /// <summary>
        /// Constructor MVVM de Tipos de Productos
        /// </summary>
        /// <param name="ent">Conexion con la base de datos pasada por parametro</param>
        public MVTiposProductos(proyecto_tpvEntities ent)
        {
            tpvEnt = ent;
            tipoProdServ = new Tipos_De_ProductosServicio(tpvEnt);
        }

        /// <summary>
        /// Varaible que obtiene la lista de tipos de productos
        /// usada en los botones
        /// </summary>
        public List<tipos_de_productos> listaTiposProds
        {
            get
            {
                return tipoProdServ.getAll().ToList();
            }
        }

    }
}
