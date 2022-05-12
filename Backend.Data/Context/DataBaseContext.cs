
using System.Collections.Generic;
using Backend.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data.Context
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options):base(options)
        {
        }
        public DbSet<UserModel> UserModels { get; set; }
        public DbSet<PermissionModel> PermissionModels { get; set; }
        public DbSet<OrderModel> OrderModel { get; set; }
        public DbSet<ProductModel> ProductModel { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>()
                .HasOne(x => x.Permission)
                .WithMany(y=>y.Users)
                .HasForeignKey(x => x.PermissionId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<PermissionModel>().HasData(new PermissionModel() {Id = 1,PermissionName = "Admin", Users = new List<UserModel>()}, new PermissionModel() { Id = 2,PermissionName = "User", Users = new List<UserModel>() });
        }
    }
}