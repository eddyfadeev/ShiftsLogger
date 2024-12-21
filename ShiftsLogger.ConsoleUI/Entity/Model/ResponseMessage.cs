namespace Entity.Model;

public record ResponseMessage
{
    public int StatusCode { get; init; }
    public string Message { get; init; }
};