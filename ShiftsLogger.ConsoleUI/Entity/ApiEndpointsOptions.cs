using System.Diagnostics.CodeAnalysis;

namespace Entity;

public class ApiEndpointsOptions
{
    public const string ApiEndpoints = "ApiEndpoints";

    private string _baseUrl;

    public required string BaseUrl
    {
        get => _baseUrl;
        [MemberNotNull(nameof(_baseUrl))]
        set
        {
            if (!value.EndsWith('/'))
            {
                value += '/';
            }
            
            _baseUrl = value;
        }
    }

    public required string Shifts { get; set; }
    public required string Users { get; set; }
    public required string Locations { get; set; }
    public required string ShiftTypes { get; set; }
}