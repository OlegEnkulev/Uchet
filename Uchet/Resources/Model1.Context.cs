﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Uchet.Resources
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class UchetDBEntities : DbContext
    {
        public UchetDBEntities()
            : base("name=UchetDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Grades> Grades { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Subjects> Subjects { get; set; }
        public virtual DbSet<Users> Users { get; set; }
    }
}
