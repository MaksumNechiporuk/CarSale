using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CarSale.Entities
{
    public class DBContext : IdentityDbContext<AppUser>
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }
        public DbSet<Make> Makes { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<MakesAndModels> MakesAndModels { get; set; }
        public DbSet<UserCar> userCars { get; set; }

        public DbSet<FilterName> FilterNames { get; set; }
        public DbSet<FilterValue> FilterValues { get; set; }
        public DbSet<FilterNameGroup> FilterNameGroups { get; set; }
        public DbSet<Filter> Filters { get; set; }
        public DBContext()
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Filter>(filter =>
            {
                filter.HasKey(f => new { f.CarId, f.FilterValueId, f.FilterNameId });

                filter.HasOne(ur => ur.FilterNameOf)
                    .WithMany(r => r.Filtres)
                    .HasForeignKey(ur => ur.FilterNameId)
                    .IsRequired();

                filter.HasOne(ur => ur.FilterValueOf)
                    .WithMany(r => r.Filtres)
                    .HasForeignKey(ur => ur.FilterValueId)
                    .IsRequired();

                filter.HasOne(ur => ur.CarOf)
                    .WithMany(r => r.Filtres)
                    .HasForeignKey(ur => ur.CarId)
                    .IsRequired();
            });

            modelBuilder.Entity<UserCar>(users_car =>
            {
                users_car.HasKey(f => new { f.UserId, f.CarId });

                users_car.HasOne(user => user.UserOf)
                    .WithMany(r => r.UserCar)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

                users_car.HasOne(ur => ur.CarOf)
                    .WithMany(r => r.UserCar)
                    .HasForeignKey(ur => ur.CarId)
                    .IsRequired();
            });
            modelBuilder.Entity<MakesAndModels>(make_and_models =>
            {
                make_and_models.HasKey(f => new { f.FilterValueId, f.FilterMakeId });

                make_and_models.HasOne(ur => ur.FilterMakeOf)
                    .WithMany(r => r.MakesAndModels)
                    .HasForeignKey(ur => ur.FilterMakeId)
                    .IsRequired();

                make_and_models.HasOne(ur => ur.FilterValueOf)
                    .WithMany(r => r.MakesAndModels)
                    .HasForeignKey(ur => ur.FilterValueId)
                    .IsRequired();
            });

            modelBuilder.Entity<FilterNameGroup>(filterNG =>
            {
                filterNG.HasKey(f => new { f.FilterValueId, f.FilterNameId });

                filterNG.HasOne(ur => ur.FilterNameOf)
                    .WithMany(r => r.FilterNameGroups)
                    .HasForeignKey(ur => ur.FilterNameId)
                    .IsRequired();

                filterNG.HasOne(ur => ur.FilterValueOf)
                    .WithMany(r => r.FilterNameGroups)
                    .HasForeignKey(ur => ur.FilterValueId)
                    .IsRequired();
            });
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}