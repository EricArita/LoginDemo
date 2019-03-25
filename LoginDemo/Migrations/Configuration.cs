using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace LoginDemo.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<LoginDemo.Context.LoginContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "LoginDemo.Context.LoginContext";
        }

        protected override void Seed(LoginDemo.Context.LoginContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
