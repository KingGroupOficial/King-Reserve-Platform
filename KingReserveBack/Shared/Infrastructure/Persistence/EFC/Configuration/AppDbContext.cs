using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using KingReserveBack.PersonAdministration.Domain.Model.Entities;
using KingReserveBack.ReserveAdministration.Domain.Model.Aggregates.Reserve;
using KingReserveBack.ReserveAdministration.Domain.Model.Entities;
using KingReserveBack.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using KingReserveBack.StaffManagement.Domain.Model.Aggregates;
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
        
        //Properties for Person
        builder.Entity<Person>().HasKey(p => p.Id);
        builder.Entity<Person>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Person>().Property(p => p.Name).IsRequired().HasMaxLength(50);
        builder.Entity<Person>().Property(p=> p.Age).IsRequired();
        builder.Entity<Person>().Property(p => p.Date).IsRequired();
        builder.Entity<Person>().Property(p => p.Country).IsRequired().HasMaxLength(50);
        builder.Entity<Person>().Property(p => p.City).IsRequired().HasMaxLength(50);
        builder.Entity<Person>().Property(p => p.District).IsRequired().HasMaxLength(50);
        builder.Entity<Person>().Property(p => p.Observations).HasMaxLength(100);
        builder.Entity<Person>().Property(p => p.RoomId).IsRequired();
        
        // Properties for Staff
        builder.Entity<Staff>().HasKey(e => e.Id);
        builder.Entity<Staff>().Property(e => e.Name).IsRequired().HasMaxLength(50);
        builder.Entity<Staff>().Property(e => e.Last_name).IsRequired().HasMaxLength(50);
        builder.Entity<Staff>().Property(e => e.Job_description).IsRequired().HasMaxLength(50);
          
        builder.Entity<Staff>().Property(e => e.Email).IsRequired().HasMaxLength(50);
        builder.Entity<Staff>().Property(e => e.Reserves_id).IsRequired().HasMaxLength(50);
        builder.Entity<Staff>().Property(e => e.On_job_status).IsRequired().HasMaxLength(20);
            
        
        
        
        
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
        
        // Relationships Bounded Context PersonAdministration
        builder.Entity<Person>()
            .Property(c => c.Date)
            .HasConversion(
                v => v.ToDateTime(TimeOnly.MinValue),
                v => DateOnly.FromDateTime(v)
            );

        // Ensure RoomId references an existing Room
        builder.Entity<Person>()
            .HasOne<Room>()
            .WithMany()
            .HasForeignKey(p => p.RoomId)
            .IsRequired();
        
        
        builder.UseSnakeCaseNamingConvention();
    }
}
