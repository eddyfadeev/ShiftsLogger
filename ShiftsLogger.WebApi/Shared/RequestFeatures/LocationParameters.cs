namespace Shared.RequestFeatures;

public class LocationParameters : RequestParameters
{
    private string _filterByName = string.Empty;
    
    public LocationParameters() =>
        OrderBy = "name asc";

    public string FilterByName
    {
        get => _filterByName;
        set => _filterByName = string.IsNullOrWhiteSpace(value) ? _filterByName : value;
    }
}