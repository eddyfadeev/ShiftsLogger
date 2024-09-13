using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ShiftsLogger.Domain.Mappers;
using ShiftsLogger.Domain.Models;
using ShiftsLogger.Domain.Models.Entities;

namespace ShiftsLogger.ConsoleApp;

static class Program
{
    static void Main(string[] args)
    {
        var services = new ServiceCollection();
    
        services.ConfigureServices();
    
        var uri = new Uri("http://localhost:5000/api/v1/users/1/shifts");
    
        var httpClient = new HttpClient();
    
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    
        var response = httpClient.GetAsync(uri).Result;
        var jsonContent = response.Content.ReadAsStringAsync().Result;
    
        if (response.IsSuccessStatusCode)
        {
            var content = JsonConvert.DeserializeObject<GenericReportModel<User>>(
                jsonContent,
                new GenericMapper<User>()
                );
            Console.WriteLine(content);
        }
        else
        {
            Console.WriteLine($"Failed to get response. Status code: {response.StatusCode}");
        }
    }
}