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
    /// para hacer un boton personalizado de un tipos de productos
    /// </summary>
    public class BtnTipoProd : Button
    {
        /// <summary>
        /// Constructor del boton personalizado de tipos de productos
        /// </summary>
        public BtnTipoProd()
        {

        }

        /// <summary>
        /// Variable publica que tiene un objeto tipos de productos
        /// para hacer un boton de tipos de productos
        /// </summary>
        public tipos_de_productos tipoProdBtn{ get; set; }
    }
}
