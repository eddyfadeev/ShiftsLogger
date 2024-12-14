using System.Linq.Dynamic.Core;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Utility;
using Shared.RequestFeatures;

namespace Repository.Extensions;

public static class ShiftRepositoryExtensions
{
    public static IQueryable<Shift>
        ApplyQueryParametersForRetrieve(this IQueryable<Shift> shifts, ShiftParameters queryParameters) =>
        shifts
            .Filter(queryParameters)
            .Search(queryParameters)
            .IncludeLocation()
            .IncludeUser()
            .IncludeShiftType()
            .Sort(queryParameters)
            .Page(queryParameters);
    
    public static IQueryable<Shift> 
        ApplyQueryParametersForCount(this IQueryable<Shift> shifts, ShiftParameters queryParameters) =>
        shifts
            .Filter(queryParameters)
            .Search(queryParameters)
            .IncludeLocation()
            .IncludeUser()
            .IncludeShiftType();
    
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
            return shifts.OrderByDescending(s => s.StartTime);
        }

        var orderQuery = QueryBuilder.CreateOrderQuery<Shift>(queryParameters.OrderBy);

        return string.IsNullOrWhiteSpace(orderQuery) 
            ? shifts.OrderBy(s => s.StartTime) 
            : shifts.OrderBy(orderQuery);
    }
    
    public static IQueryable<Shift> IncludeLocation(this IQueryable<Shift> shifts) =>
        shifts.Include(s => s.Location);

    public static IQueryable<Shift> IncludeUser(this IQueryable<Shift> shifts) =>
        shifts.Include(s => s.User);

    public static IQueryable<Shift> IncludeShiftType(this IQueryable<Shift> shifts) =>
        shifts.Include(s => s.ShiftType);
}