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
    
    public partial class usuarios
    {
        public usuarios()
        {
            this.ventas = new HashSet<ventas>();
        }
    
        public int cod_usuario { get; set; }
        public string nombre_usu { get; set; }
        public string apellidos_usu { get; set; }
        public string usuario { get; set; }
        public string contrasenya { get; set; }
        public Nullable<int> cod_rol { get; set; }
    
        public virtual roles roles { get; set; }
        public virtual ICollection<ventas> ventas { get; set; }
    }
}
