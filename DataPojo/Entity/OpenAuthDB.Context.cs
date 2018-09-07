﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataPojo.Entity
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class OpenAuthDBEntities : DbContext
    {
        public OpenAuthDBEntities()
            : base("name=OpenAuthDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Application> Application { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<CategoryType> CategoryType { get; set; }
        public virtual DbSet<FlowInstance> FlowInstance { get; set; }
        public virtual DbSet<FlowInstanceOperationHistory> FlowInstanceOperationHistory { get; set; }
        public virtual DbSet<FlowInstanceTransitionHistory> FlowInstanceTransitionHistory { get; set; }
        public virtual DbSet<FlowScheme> FlowScheme { get; set; }
        public virtual DbSet<Form> Form { get; set; }
        public virtual DbSet<Module> Module { get; set; }
        public virtual DbSet<ModuleElement> ModuleElement { get; set; }
        public virtual DbSet<Org> Org { get; set; }
        public virtual DbSet<Relevance> Relevance { get; set; }
        public virtual DbSet<Resource> Resource { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Stock> Stock { get; set; }
        public virtual DbSet<Test> Test { get; set; }
        public virtual DbSet<User> User { get; set; }
    }
}