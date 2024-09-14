using Newtonsoft.Json;
using ShiftsLogger.Domain.Mappers;
using ShiftsLogger.Domain.Models.Entities;

namespace ShiftsLogger.Infrastructure.Strategies.ConvertStrategy;

public class UserConverterStrategy : BaseConverterStrategy
{
    private protected override List<JsonConverter> GetJsonConverters() =>
        new () { new UserMapper(), new GenericReportMapper<User>() };
}