// See https://aka.ms/new-console-template for more information

using CsvReaderApp;
using CsvReaderApp.Entities;

const string csvFilePath = @"sample-cab-data.csv";

VendorService vendorService = new VendorService(new AppDbContext(), new VendorsCsvReader());

var rows = await vendorService.LoadFromCsvAndUploadAsync(csvFilePath);

Console.WriteLine($"Rows inserted: {rows}");

await vendorService.QueryTestAsync();