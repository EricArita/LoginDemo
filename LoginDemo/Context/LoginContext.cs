using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using LoginDemo.Models;
using LoginDemo.Migrations;

namespace LoginDemo.Context
{
    public partial class LoginContext : DbContext
    {
        public LoginContext()
            : base("name=LoginContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<LoginContext, Configuration>());
        }

        public virtual DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .Property(e => e.UserName)
                .IsUnicode(false);
                
            modelBuilder.Entity<Account>()
                .Property(e => e.Password)
                .IsUnicode(false);
        }
    }
}
