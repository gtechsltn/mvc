using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Mvc.Models
{
    public class OracleDbContext : DbContext
    {
        public OracleDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Database.Log = s => Debug.WriteLine(s);
        }

        public virtual DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Entity<Customer>()
                .Property(e => e.Salary)
                .HasPrecision(18, 4);

            base.OnModelCreating(modelBuilder);
        }
    }
}