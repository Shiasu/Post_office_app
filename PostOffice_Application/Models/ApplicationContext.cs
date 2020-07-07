using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PostOffice_Application.Models
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            //Database.EnsureDeleted(); // Позволяет пересобрать базу данных без миграций и удаляет все данные, кроме тех что в конструкторе контроллера
            Database.EnsureCreated();
        }

        // Условия заполнения базы данных при создании каждого нового объекта
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Parcel>()
                .Property(p => p.Date)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Parcel>()
                .Property(p => p.ParcelNumber)
                .HasComputedColumnSql("[Id] + 943800000");

            modelBuilder.Entity<Parcel>()
                .Property(p => p.Status)
                .HasDefaultValueSql("0");
        }

        public DbSet<Parcel> Parcels { get; set; }
    }
}
