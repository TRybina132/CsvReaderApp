// See https://aka.ms/new-console-template for more information

using CsvReaderApp;
using CsvReaderApp.Entities;

const string csvFilePath = @"C:\Users\Tanya\Downloads\sample-cab-data.csv";

VendorService vendorService = new VendorService(new AppDbContext(), new VendorsCsvReader());

Console.WriteLine(await vendorService.LoadFromCsvAndUploadAsync(csvFilePath));
