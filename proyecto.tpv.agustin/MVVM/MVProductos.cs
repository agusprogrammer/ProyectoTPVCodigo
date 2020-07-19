using proyecto.tpv.agustin.Modelo;
using proyecto.tpv.agustin.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace proyecto.tpv.agustin.MVVM
{
    /// <summary>
    /// MVVM de Productos
    /// </summary>
    public class MVProductos : MVBase
    {
        private proyecto_tpvEntities tpvEnt;    //Conexion con la base de datos
        private ProductosServicio prodServ;     //Servicio de productos

        //Cosas MV mostrar por tipo, coger los tipos y categorias
        private Tipos_De_ProductosServicio tipoProdserv;    //Servicio de tipos de productos
        private CategoriasServicio categServicio;           //servicio de categorias
        
        private ListCollectionView listaCollecProductosTpv;    //ListCollectionView lista de productos del tpv en formato ListCollectionView
        
        private productos productoLista;    //Producto de la lista a comprar
        
        private List<productos> listaProdsCompra;       //Lista de productos a comprar, por cada vez que se pulsa un producto en el TPV, se agrega a la lista

        /// <summary>
        /// Constructor MVVM de Productos
        /// </summary>
        /// <param name="ent">Conexion con la base de datos pasada por parametro</param>
        public MVProductos(proyecto_tpvEntities ent)
        {
            tpvEnt = ent;
            inicializa();
        }

        /// <summary>
        /// Metodo para inicializar los objetos 
        /// y variables del MVVM del MV de productos
        /// </summary>
        private void inicializa()
        {
            prodServ = new ProductosServicio(tpvEnt);
            
            //Servicios del boton por tipos de productos
            tipoProdserv = new Tipos_De_ProductosServicio(tpvEnt);
            categServicio = new CategoriasServicio(tpvEnt);
            listaCollecProductosTpv = new ListCollectionView(prodServ.getAll().ToList());
            productoLista = new productos();
            listaProdsCompra = new List<productos>();
        }

        /// <summary>
        /// Variable que obtiene el ListCollectionView con los productos
        /// </summary>
        public ListCollectionView listaProductosListColletion
        {
            get
            {
                return listaCollecProductosTpv;
            }
        }
        
        /// <summary>
        /// Varaible que obtiene la lista de tipos de productos
        /// usada en los botones de los tipos de productos
        /// </summary>
        public List<tipos_de_productos> listaTiposProductos
        {
            get
            {
                return tipoProdserv.getAll().ToList();
            }
        }

        
        /// <summary>
        /// Varaible que obtiene la lista de productos
        /// usada en los botones de los productos
        /// </summary>
        public List<productos> listaProductos
        {
            get
            {
                return prodServ.getAll().ToList();
            }
        }
        

        /// <summary>
        /// Variable que retorna el producto desde la lista de botones
        /// </summary>
        public productos prodSelectListaGet
        {
            get
            {
                return productoLista;
            }

        }

        //Producto que pasamos desde los botones de
        //comprar del tpv, la lista de productos
        public void prodSelectListaSet(productos prod)
        {
            productoLista = prod;
            listaProdsCompra.Add(productoLista);
        }
        
        /// <summary>
        /// variable para obtener la lista de la compra
        /// </summary>
        public List<productos> listaProductosListaCompra
        {
            get
            {
                return listaProdsCompra;
            }
        }
        
        /// <summary>
        /// Metodo que actualiza la lista de productos a comprar
        /// </summary>
        /// <param name="listaProd">lista de los productos que le pasamos al MV</param>
        public void listaProductosActualizar(List<productos> listaProd)
        {
            listaProdsCompra = listaProd;
        }
        
        /// <summary>
        /// Metodo que nos devulve la lista de productos a partir 
        /// de un codigo de categoria
        /// </summary>
        /// <param name="codTipoProd">codigo del tipo de producto</param>
        /// <returns>Retorna la lista de productos</returns>
        public List<productos> listaPorProductos(int codTipoProd)
        {
            return prodServ.listaProductosPorCategoria(codTipoProd);
        }


    }
}
