﻿//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace TeachSys.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TeachDBEntities : DbContext
    {
        public TeachDBEntities()
            : base("name=TeachDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<BookProperties> BookProperties { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<BookTypes> BookTypes { get; set; }
        public DbSet<Classes> Classes { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<Majors> Majors { get; set; }
        public DbSet<Students> Students { get; set; }
        public DbSet<TeacherClasses> TeacherClasses { get; set; }
        public DbSet<Teachers> Teachers { get; set; }
        public DbSet<Terms> Terms { get; set; }
        public DbSet<View_TeacherClasses> View_TeacherClasses { get; set; }
        public DbSet<BookUsed> BookUsed { get; set; }
    }
}
