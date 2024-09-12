using Microsoft.Extensions.DependencyInjection;

namespace ShiftsLogger.ConsoleApp;

static class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection();
        
        services.ConfigureServices();
    }
}