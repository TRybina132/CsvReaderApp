namespace CsvReaderApp.Entities;

public class VendorEntity
{
    public string Id { get; set; }
    public DateTimeOffset TpepPickupDatetime { get; set; }
    public DateTimeOffset TpepDropoffDatetime { get; set; }
    public int? PassengerCount { get; set; }
    public double TripDistance { get; set; }
    public string StoreAndFwdFlag { get; set; }
    public int PULocationID { get; set; }
    public int DOLocationID { get; set; }
    public double FareAmount { get; set; }
    public double TipAmount { get; set; }
}