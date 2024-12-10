using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.HasData(
            new Location
            {
                Id = Guid.Parse(SeedConstants.Locations.LocationOneGuid),
                Name = "Office",
                Address = "777 Lucky Str. SW, Edmonton, AB, Canada"
            },
            new Location
            {
                Id = Guid.Parse(SeedConstants.Locations.LocationTwoGuid),
                Name = "Home",
                Address = "666 Sleepy Str. SW, Edmonton, AB, Canada"
            }
        );
    }
}