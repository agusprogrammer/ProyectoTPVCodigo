//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace proyecto.tpv.agustin.Modelo
{
    using System;
    using System.Collections.Generic;
    
    public partial class ofertas
    {
        public ofertas()
        {
            this.productos = new HashSet<productos>();
            this.clientes = new HashSet<clientes>();
        }
    
        public int cod_oferta { get; set; }
        public string descripcion { get; set; }
        public Nullable<System.DateTime> periodo_oferta { get; set; }
        public string fichero { get; set; }
    
        public virtual ICollection<productos> productos { get; set; }
        public virtual ICollection<clientes> clientes { get; set; }
    }
}
