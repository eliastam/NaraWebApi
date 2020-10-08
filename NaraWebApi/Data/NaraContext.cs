using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NaraWebApi.Data.Entities;

namespace NaraWebApi.Data
{
    public class NaraContext : DbContext
    {
        public NaraContext(DbContextOptions options) : base(options) { }
        public DbSet<StoreItemsEntity> StoreItmes { get; set; }
        public DbSet<AddOn> AddOns { get; set; }
        public DbSet<AddOnMenu> AddOnsMenu { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Menu> Menu { get; set; }
        //public DbSet<Archive> Archive { get; set; }
        public DbSet<Table> Tables { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.HasDefaultSchema("Nara");
            //// Relationship ( Order | OrderItems )
            //modelBuilder.Entity<Order>().HasMany((e) => e.OrderItems).WithOne((e) => e.Order);

            //// Relationship ( OrderItems | AddOns )
            //modelBuilder.Entity<OrderItem>().HasMany((e) => e.AddOns).WithOne((e) => e.OrderItem);

            //// Relationship ( Table | Orders )
            //modelBuilder.Entity<Table>().HasMany((e) => e.Orders).WithOne((e) => e.Table);
            base.OnModelCreating(modelBuilder);
        }
    }
}
