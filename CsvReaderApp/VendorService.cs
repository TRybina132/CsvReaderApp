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
        if (await _dbContext.Database.EnsureCreatedAsync())
        {
            await _dbContext.Database.MigrateAsync();
        }
        
        var entities = await _csvReader.ReadVendorsFromCsvAsync(filePath);
        
        await _dbContext.BulkInsertAsync(entities);

        await _dbContext.BulkSaveChangesAsync();
        return 0;
    }
}