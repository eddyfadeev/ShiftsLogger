namespace Shared.RequestFeatures;

public class ShiftTypeParameters : RequestParameters
{
    public ShiftTypeParameters() => 
        OrderBy = "name asc";
}