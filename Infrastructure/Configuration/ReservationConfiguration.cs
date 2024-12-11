using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.RowVersion).IsRowVersion();
            builder.Property(r => r.HabitacionId).IsRequired();
            builder.Property(r => r.FechaInicio).IsRequired();
            builder.Property(r => r.FechaFinal).IsRequired();
        }
    }
}
