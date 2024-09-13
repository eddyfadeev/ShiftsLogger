using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using ShiftsLogger.Application.Interfaces;
using ShiftsLogger.Domain.Enums;
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
        
        var uri = services.BuildServiceProvider().GetRequiredService<IApiEndpointMapper>()
           .GetRelativeUrl(ApiEndpoints.Users.GetAll);
    
        var httpClient = new HttpClient();
    
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    
        var response = httpClient.GetAsync(uri).Result;
        var jsonContent = response.Content.ReadAsStringAsync().Result;
    
        if (response.IsSuccessStatusCode)
        {
            var settings = new JsonSerializerSettings
            {
                Converters = { new UserMapper(), new GenericReportMapper<User>() },
                NullValueHandling = NullValueHandling.Ignore
            };
            
            var reportModel = JsonConvert.DeserializeObject<GenericReportModel<User>>(jsonContent, settings);
                
            if (reportModel != null)
            {
                Console.WriteLine($"Total Shifts: {reportModel.Entities.Count}");
                foreach (var user in reportModel.Entities)
                {
                    Console.WriteLine($"{user.FirstName} {user.LastName} - {user.Email} - {user.Role}");
                }
            }
            else
            {
                Console.WriteLine("Failed to deserialize the report model.");
            }
        }
        else
        {
            Console.WriteLine($"Failed to get response. Status code: {response.StatusCode}");
        }
    }
}