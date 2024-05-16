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
        var converter = new ValueConverter<string, string>(
            v => v == "Y" ? "Yes" : "No",
            v => v);

        builder.Property(v => v.StoreAndFwdFlag)
            .HasConversion(converter);
    }
}