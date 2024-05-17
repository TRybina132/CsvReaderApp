using CsvReaderApp.Entities;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;

namespace CsvReaderApp;

public class VendorService
{
    private readonly AppDbContext _dbContext;
    private readonly VendorsCsvReader _csvReader;

    public VendorService(AppDbContext dbContext, VendorsCsvReader csvReader)
    {
        _dbContext = dbContext;
        _csvReader = csvReader;
    }

    public async Task<int> LoadFromCsvAndUploadAsync(string filePath)
    {
        try
        {
            if (await _dbContext.Database.EnsureCreatedAsync())
            {
                await _dbContext.Database.MigrateAsync();
            }
        
            var entities = await _csvReader.ReadVendorsFromCsvAsync(filePath);
        
            await _dbContext.BulkInsertAsync(entities);

            await _dbContext.BulkSaveChangesAsync();
        
            return entities.Count();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return 0;
        }
    }

    public async Task QueryTestAsync()
    {
        var highestAverageTipLocation = await _dbContext.Vendors
            .GroupBy(v => v.PULocationID)
            .Select(g => new { PULocationID = g.Key, AverageTip = g.Average(v => v.TipAmount) })
            .OrderByDescending(g => g.AverageTip)
            .FirstOrDefaultAsync();
        
        var longestFaresByDistance =  await _dbContext.Vendors
            .OrderByDescending(v => v.TripDistance)
            .Take(100)
            .ToListAsync();
        
        string sqlQuery = "SELECT TOP 100 \n    *,\n    DATEDIFF(MINUTE, TpepPickupDatetime, TpepDropoffDatetime) AS TravelTime\nFROM \n    Vendors\nORDER BY \n    TravelTime DESC";
        
        var longestFaresByTime = await _dbContext.Vendors
            .FromSqlRaw(sqlQuery)
            .ToListAsync();

        Console.WriteLine(highestAverageTipLocation);
        Console.WriteLine(longestFaresByDistance.Count());
        Console.WriteLine(longestFaresByTime.Count());
    }
}