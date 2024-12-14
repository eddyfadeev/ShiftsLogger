namespace Shared.RequestFeatures;

public class UserParameters : RequestParameters
{
    public UserParameters() =>
        OrderBy = "firstName";
}