// See https://aka.ms/new-console-template for more information

using CsvReaderApp;
using CsvReaderApp.Entities;

VendorsCvsReader reader = new VendorsCvsReader();

var records = await reader.ReadVendorsFromCsvAsync(@"C:\Users\Tanya\Downloads\sample-cab-data.csv");

Console.WriteLine("Hello, World!");