using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CsvReaderApp.Entities.Configurations;

public class VendorEntityConfiguration : IEntityTypeConfiguration<VendorEntity>
{
    public void Configure(EntityTypeBuilder<VendorEntity> builder)
    {
        builder.HasKey(v => v.Id);

        builder.HasIndex(v => v.PULocationID);
        builder.HasIndex(v => v.TripDistance);


        // For conversion of Y and N values
        var flagConverter = new ValueConverter<string, string>(
            v => v == "Y" ? "Yes" : "No",
            v => v);
        
        // For converting datetime properties to UTC
        var utcTimeConverter = new ValueConverter<DateTime, DateTime>(
            v => v.ToUniversalTime(),
            v => v);

        builder.Property(v => v.StoreAndFwdFlag)
            .HasConversion(flagConverter);

        builder.Property(v => v.TpepDropoffDatetime)
            .HasConversion(utcTimeConverter);
        
        builder.Property(v => v.TpepPickupDatetime)
            .HasConversion(utcTimeConverter);
    }
}