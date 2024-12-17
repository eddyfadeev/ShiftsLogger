namespace Entity;

public class ApiEndpointsOptions
{
    public const string ApiEndpoints = "ApiEndpoints";
    
    public required string BaseUrl { get; set; }
    
    public required string Shifts { get; set; }
    public required string Users { get; set; }
    public required string Locations { get; set; }
    public required string ShiftTypes { get; set; }
}