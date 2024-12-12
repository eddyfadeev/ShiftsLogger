using System.Linq.Dynamic.Core;
using Entities.Models;
using Repository.Utility;
using Shared.RequestFeatures;

namespace Repository.Extensions;

public static class ShiftRepositoryExtensions
{
    public static IQueryable<Shift> Filter(this IQueryable<Shift> shifts, ShiftParameters queryParameters) =>
        shifts.Where(s =>
            s.StartTime >= queryParameters.FromDate &&
            s.EndTime <= queryParameters.ToDate &&
            s.HoursWorked >= queryParameters.MinWorkedHours &&
            s.HoursWorked <= queryParameters.MaxWorkedHours);

    public static IQueryable<Shift> Sort(this IQueryable<Shift> shifts, ShiftParameters queryParameters)
    {
        if (string.IsNullOrWhiteSpace(queryParameters.OrderBy))
        {
            return shifts.OrderBy(s => s.StartTime);
        }

        var orderQuery = QueryBuilder.CreateOrderQuery<Shift>(queryParameters.OrderBy);

        return string.IsNullOrWhiteSpace(orderQuery) 
            ? shifts.OrderBy(s => s.StartTime) 
            : shifts.OrderBy(orderQuery);
    }
}