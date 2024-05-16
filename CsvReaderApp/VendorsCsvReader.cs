using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvReaderApp.Entities;

namespace CsvReaderApp;

public class VendorsCsvReader
{
    public async Task<IEnumerable<VendorEntity>> ReadVendorsFromCsvAsync(string filePath)
    {
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        csv.Context.RegisterClassMap<VendorEntityMap>();
        
        var records = csv.GetRecordsAsync<VendorEntity>();

        var entities = new List<VendorEntity>();

        await foreach (var r in records)
        {
            r.Id = Guid.NewGuid().ToString();
            entities.Add(r);
        }
        
        var duplicates = entities
            .GroupBy(v => new { v.TpepPickupDatetime, v.TpepDropoffDatetime, v.PassengerCount })
            .Where(g => g.Count() > 1)
            .SelectMany(g => g.Skip(1))
            .ToList();

        // Remove duplicates from the list of vendors to update
        entities = entities.Except(duplicates).ToList();

        await WriteToCsvAsync(duplicates);
        
        return entities;
    }
    
    public sealed class VendorEntityMap : ClassMap<VendorEntity>
    {
        public VendorEntityMap()
        {
            Map(m => m.Id).Name("VendorID");
            Map(m => m.TpepPickupDatetime).Name("tpep_pickup_datetime");
            Map(m => m.TpepDropoffDatetime).Name("tpep_dropoff_datetime");
            Map(m => m.PassengerCount).Name("passenger_count");
            Map(m => m.TripDistance).Name("trip_distance");
            Map(m => m.StoreAndFwdFlag).Name("store_and_fwd_flag");
            Map(m => m.PULocationID).Name("PULocationID");
            Map(m => m.DOLocationID).Name("DOLocationID");
            Map(m => m.FareAmount).Name("fare_amount");
            Map(m => m.TipAmount).Name("tip_amount");
        }
    }

    private async Task WriteToCsvAsync(List<VendorEntity> vendors)
    {
        // Write duplicates to a CSV file
        await using var writer = new StreamWriter("duplicates.csv");
        await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        
        await csv.WriteRecordsAsync(vendors);
    }
}