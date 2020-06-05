using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EF.Configuration
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses");

            builder
                .HasKey(e => e.Id);

            builder
                .Property(e => e.AddressCode)
                .HasColumnType("varchar(500)");

            builder
                .Property(e => e.AddressStreet)
                .HasColumnType("varchar(500)");

            builder
                .Property(e => e.District)
                .HasColumnType("varchar(500)");

            builder
                .Property(e => e.Locality)
                .HasColumnType("varchar(500)");

            builder
                .Property(e => e.FederalUnit)
                .HasColumnType("varchar(500)");
        }
    }
}
