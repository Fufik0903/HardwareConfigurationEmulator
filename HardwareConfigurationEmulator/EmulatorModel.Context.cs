﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HardwareConfigurationEmulator
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class EmulatorDatabaseEntities : DbContext
    {
        public EmulatorDatabaseEntities()
            : base("name=EmulatorDatabaseEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Generation> Generation { get; set; }
        public virtual DbSet<Router> Router { get; set; }
        public virtual DbSet<Switch> Switch { get; set; }
        public virtual DbSet<TypeOfBuffering> TypeOfBuffering { get; set; }
        public virtual DbSet<TypeOfRoutingTable> TypeOfRoutingTable { get; set; }
        public virtual DbSet<NetworkInterfaceController> NetworkInterfaceController { get; set; }
        public virtual DbSet<User> User { get; set; }
    }
}
