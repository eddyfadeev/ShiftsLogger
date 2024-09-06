using Microsoft.EntityFrameworkCore;
using ShiftsLogger.Domain.Models;

namespace ShiftsLogger.Infrastructure.Data;

public class ShiftsLoggerDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Shift> Shifts { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<ShiftType> ShiftTypes { get; set; }

    public ShiftsLoggerDbContext(DbContextOptions<ShiftsLoggerDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Shift>()
            .HasOne(s => s.User)
            .WithMany(u => u.Shifts)
            .HasForeignKey(s => s.UserId);
        
        modelBuilder.Entity<Shift>()
            .HasOne(s => s.Location)
            .WithMany(l => l.Shifts)
            .HasForeignKey(s => s.LocationId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Shift>()
            .HasOne(s => s.ShiftType)
            .WithMany(st => st.Shifts)
            .HasForeignKey(s => s.ShiftTypeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}