using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using CsvReaderApp.Entities;

namespace CsvReaderApp;

public class VendorsCvsReader
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
            entities.Add(r);
        }

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
}