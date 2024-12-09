using Microsoft.EntityFrameworkCore;

namespace Repository.Tests;

internal static class ImMemoryDbProvider
{
    public static async Task<RepositoryContext> CreateDatabaseContext()
    {
        var context = GetInMemoryDbContext();

        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        return context;
    }

    private static RepositoryContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<RepositoryContext>()
            .UseInMemoryDatabase(databaseName: $"TestDb_{Guid.NewGuid().ToString()}")
            .Options;

        return new RepositoryContext(options);
    }
}