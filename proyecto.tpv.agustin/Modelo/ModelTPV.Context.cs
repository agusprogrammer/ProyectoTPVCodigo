﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class proyecto_tpvEntities : DbContext
    {
        public proyecto_tpvEntities()
            : base("name=proyecto_tpvEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<categorias> categorias { get; set; }
        public DbSet<clientes> clientes { get; set; }
        public DbSet<ofertas> ofertas { get; set; }
        public DbSet<permisos> permisos { get; set; }
        public DbSet<productos> productos { get; set; }
        public DbSet<productos_y_ubicaciones> productos_y_ubicaciones { get; set; }
        public DbSet<roles> roles { get; set; }
        public DbSet<tipos_de_productos> tipos_de_productos { get; set; }
        public DbSet<ubicaciones> ubicaciones { get; set; }
        public DbSet<usuarios> usuarios { get; set; }
        public DbSet<ventas> ventas { get; set; }
        public DbSet<ventas_de_productos> ventas_de_productos { get; set; }
    }
}
