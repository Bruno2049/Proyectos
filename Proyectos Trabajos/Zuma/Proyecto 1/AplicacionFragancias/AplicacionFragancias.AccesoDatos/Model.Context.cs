﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AplicacionFragancias.AccesoDatos
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class FraganciasEntities : DbContext
    {
        public FraganciasEntities()
            : base("name=FraganciasEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ALM_CAT_ALMECENES> ALM_CAT_ALMECENES { get; set; }
        public virtual DbSet<COM_CAT_TIPO_OPERACION> COM_CAT_TIPO_OPERACION { get; set; }
        public virtual DbSet<COM_ESTATUS_COMPRA> COM_ESTATUS_COMPRA { get; set; }
        public virtual DbSet<COM_ORDENCOMPRA> COM_ORDENCOMPRA { get; set; }
        public virtual DbSet<COM_PRODUCTOS> COM_PRODUCTOS { get; set; }
        public virtual DbSet<LOG_COM_OPERACIONES> LOG_COM_OPERACIONES { get; set; }
        public virtual DbSet<PER_PERSONA> PER_PERSONA { get; set; }
        public virtual DbSet<SIS_MENUARBOL> SIS_MENUARBOL { get; set; }
        public virtual DbSet<SIS_PERFILES_MENU> SIS_PERFILES_MENU { get; set; }
        public virtual DbSet<US_CAT_PERFILES> US_CAT_PERFILES { get; set; }
        public virtual DbSet<US_PERFILES> US_PERFILES { get; set; }
        public virtual DbSet<US_USUARIOS> US_USUARIOS { get; set; }
    }
}
