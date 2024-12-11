using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

public class ShiftTypeConfiguration : IEntityTypeConfiguration<ShiftType>
{
    public void Configure(EntityTypeBuilder<ShiftType> builder)
    {
        builder.HasData(
            new ShiftType
            {
                Id = Guid.Parse(SeedConstants.ShiftTypes.ShiftTypeOneGuid),
                Name = "Morning Shift"
            },
            new ShiftType
            {
                Id = Guid.Parse(SeedConstants.ShiftTypes.ShiftTypeTwoGuid),
                Name = "Evening Shift"
            }
        );
    }
}