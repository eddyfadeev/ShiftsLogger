using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

public class ShiftConfiguration : IEntityTypeConfiguration<Shift>
{
    public void Configure(EntityTypeBuilder<Shift> builder)
    {
        ConfigureComputedColumn(builder);
        SeedShifts(builder);
    }

    private static void ConfigureComputedColumn(EntityTypeBuilder<Shift> builder)
    {
        builder.Property(e => e.HoursWorked)
            .HasComputedColumnSql("DATEDIFF(MINUTE, StartTime, EndTime) / 60.00", stored: true);
    }

    private void SeedShifts(EntityTypeBuilder<Shift> builder)
    {
        var shifts = GenerateShiftSeedData();
        builder.HasData(shifts);
    }

    private List<Shift> GenerateShiftSeedData()
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
                        var random = new Random();
                        int workedHours = random.Next(minValue: 1, maxValue: 16);
                        
                        shifts.Add(new Shift
                        {
                            Id = Guid.NewGuid(),
                            StartTime = baseDate.AddDays(i),
                            EndTime = baseDate.AddDays(i).AddHours(workedHours),
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

        return shifts;
    }
}