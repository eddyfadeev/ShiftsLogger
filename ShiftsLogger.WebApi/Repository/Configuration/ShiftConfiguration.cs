using Entities.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

public class ShiftConfiguration : IEntityTypeConfiguration<Shift>
{
    public void Configure(EntityTypeBuilder<Shift> builder)
    {
        var shifts = new List<Shift>();
        var baseDate = DateTime.Today.AddDays(-200);

        var users = new[] { SeedConstants.Users.UserOneGuid, SeedConstants.Users.UserTwoGuid };
        var locations = new[] { SeedConstants.Locations.LocationOneGuid, SeedConstants.Locations.LocationTwoGuid };
        var shiftTypes = new[] { SeedConstants.ShiftTypes.ShiftTypeOneGuid, SeedConstants.ShiftTypes.ShiftTypeTwoGuid };

        foreach (var user in users)
        {
            foreach (var location in locations)
            {
                foreach (var shiftType in shiftTypes)
                {
                    // Create two shifts for each combination
                    for (int i = 0; i < 2; i++)
                    {
                        shifts.Add(new Shift
                        {
                            Id = Guid.NewGuid(),
                            StartTime = baseDate.AddDays(i),
                            EndTime = baseDate.AddDays(i).AddHours(8), // Example: 8-hour shift
                            Description = $"Worked a shift on {baseDate.AddDays(i):yyyy-MM-dd}",
                            LocationId = Guid.Parse(location),
                            ShiftTypeId = Guid.Parse(shiftType),
                            UserId = Guid.Parse(user),
                        });
                    }

                    baseDate = baseDate.AddDays(10);
                }
            }
        }

        builder.HasData(shifts);
    }
}