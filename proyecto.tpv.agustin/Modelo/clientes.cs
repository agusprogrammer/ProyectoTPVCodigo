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
    
    public partial class clientes
    {
        public clientes()
        {
            this.ventas = new HashSet<ventas>();
            this.ofertas = new HashSet<ofertas>();
        }
    
        public int cod_cliente { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string direccion { get; set; }
        public string e_mail { get; set; }
        public string poblacion { get; set; }
        public string tipo_de_direccion { get; set; }
    
        public virtual ICollection<ventas> ventas { get; set; }
        public virtual ICollection<ofertas> ofertas { get; set; }
    }
}