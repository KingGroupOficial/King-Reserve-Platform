using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using KingReserveBack.ReserveAdministration.Domain.Model.Aggregates.Reserve;
using KingReserveBack.ReserveAdministration.Domain.Model.Entities;
using KingReserveBack.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;

namespace KingReserveBack.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        //BusinessAdministration context
        //Properties for Reserve
        builder.Entity<Reserve>().HasKey(r => r.Id);
        builder.Entity<Reserve>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Reserve>().Property(r => r.Name).IsRequired().HasMaxLength(50);
        builder.Entity<Reserve>().Property(r => r.DateStart).IsRequired();
        builder.Entity<Reserve>().Property(r => r.DateEnd).IsRequired();
        builder.Entity<Reserve>().Property(r => r.Condition).HasMaxLength(50);
        builder.Entity<Reserve>().Property(r => r.Duration);
        
        
        // Properties for Room
        builder.Entity<Room>().HasKey(r => r.Id);
        builder.Entity<Room>().Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Room>().Property(r => r.Name).IsRequired().HasMaxLength(30);
        builder.Entity<Room>().Property(r => r.Area).HasMaxLength(8);
        builder.Entity<Room>().Property(r => r.Status).IsRequired().HasMaxLength(50); 
        
        
        //Relationships Bounded Context ReserveAdministration
        builder.Entity<Reserve>().HasMany(c => c.Rooms);
        
        builder.Entity<Reserve>()
            .Property(c => c.DateStart)
            .HasConversion(
                v => v.ToDateTime(TimeOnly.MinValue),
                v => DateOnly.FromDateTime(v)
            );

        builder.Entity<Reserve>()
            .Property(c => c.DateEnd)
            .HasConversion(
                v => v.ToDateTime(TimeOnly.MinValue),
                v => DateOnly.FromDateTime(v)
            );
        
        
        builder.UseSnakeCaseNamingConvention();
    }
}
