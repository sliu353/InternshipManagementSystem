﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InternshipManagementSystem.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Internship_Management_SystemEntities : DbContext
    {
        public Internship_Management_SystemEntities()
            : base("name=Internship_Management_SystemEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Class_> Class_ { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Contract_> Contract_ { get; set; }
        public virtual DbSet<HomePageContent> HomePageContents { get; set; }
        public virtual DbSet<InternType> InternTypes { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<database_firewall_rules> database_firewall_rules { get; set; }
    }
}