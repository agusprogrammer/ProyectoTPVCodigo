using proyecto.tpv.agustin.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace proyecto.tpv.agustin.Componentes
{
    /// <summary>
    /// Clase del componente boton personalizado que extiende de Button
    /// para hacer un boton personalizado de un producto
    /// </summary>
    public class BtnProducto : Button
    {
        /// <summary>
        /// Constructor del boton personalizado de producto
        /// </summary>
        public BtnProducto()
        {

        }

        /// <summary>
        /// Variable publica que tiene un objeto producto
        /// para hacer un boton de producto
        /// </summary>
        public productos prodBtn { get; set; }
    }
}
