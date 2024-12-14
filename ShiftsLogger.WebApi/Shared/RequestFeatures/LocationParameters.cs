namespace Shared.RequestFeatures;

public class LocationParameters : RequestParameters
{
    public LocationParameters() =>
        OrderBy = "name";
}