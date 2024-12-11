using Domain.Entities;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Contexts
{
    public class SqlContext : DbContext
    {
        public DbSet<Reservation> Reservations { get; set; }

        public SqlContext(DbContextOptions<SqlContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ReservationConfiguration());
        }
    }
}
