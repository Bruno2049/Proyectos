﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Aplicacion.Entidades;

namespace Aplicacion.AccesoDatos
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ExamenEntities : DbContext
    {
        public ExamenEntities()
            : base("name=ExamenEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CAT_AREANEGOCIO> CAT_AREANEGOCIO { get; set; }
        public virtual DbSet<PER_PERSONAS> PER_PERSONAS { get; set; }
        public virtual DbSet<SIS_APLICACIONNES> SIS_APLICACIONNES { get; set; }
    }
}