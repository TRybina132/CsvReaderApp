
using CsvReaderApp;
using CsvReaderApp.Entities;

const string csvFilePath = @"sample-cab-data.csv";

VendorService vendorService = new VendorService(new AppDbContext(), new VendorsCsvReader());

var rows = await vendorService.LoadFromCsvAndUploadAsync(csvFilePath);

Console.WriteLine($"Rows inserted: {rows}");

await vendorService.QueryTestAsync();

// After running program 29 889 rows were inserted to table and 111 duplicated were found