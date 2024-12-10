using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasData(
            new User
            {
                Id = Guid.Parse(SeedConstants.Users.UserOneGuid),
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@example.com",
                Role = "Designer"
            },
            new User
            {
                Id = Guid.Parse(SeedConstants.Users.UserTwoGuid),
                FirstName = "Jane",
                LastName = "Smith",
                Email = "janesmith@example.com",
                Role = "Software Developer"
            }
        );
    }
}