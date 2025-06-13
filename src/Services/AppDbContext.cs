using Microsoft.EntityFrameworkCore;
using Models;

namespace Services;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
        
    }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }


    public DbSet<CarManufacturer> CarManufacturers { get; set; }
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Competition> Competitions { get; set; }
    public DbSet<DriverCompetition> DriverCompetitions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // DRIVER
        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(d => d.Id);

            entity.Property(d => d.FirstName)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(d => d.LastName)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(d => d.Birthday)
                .HasColumnType("datetime2")
                .IsRequired();

            entity.HasOne(d => d.Car)
                .WithMany()
                .HasForeignKey(d => d.CarId)
                .IsRequired();
        });

        // CAR
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(c => c.Id);

            entity.Property(c => c.ModelName)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(c => c.Number)
                .IsRequired();

            entity.HasOne(c => c.CarManufacture)
                .WithMany()
                .HasForeignKey(c => c.CarManufactureId)
                .IsRequired();
        });

        // CAR MANUFACTURER
        modelBuilder.Entity<CarManufacturer>(entity =>
        {
            entity.HasKey(cm => cm.Id);

            entity.Property(cm => cm.Name)
                .HasMaxLength(200)
                .IsRequired();
        });

        // COMPETITION
        modelBuilder.Entity<Competition>(entity =>
        {
            entity.HasKey(c => c.Id);

            entity.Property(c => c.Name)
                .HasMaxLength(200)
                .IsRequired();
        });

        // DRIVER-COMPETITION
        modelBuilder.Entity<DriverCompetition>(entity =>
        {
            entity.HasKey(dc => new { dc.DriverId, dc.CompetitionId });

            entity.Property(dc => dc.Date)
                .HasColumnType("datetime2")
                .IsRequired();

            entity.HasOne(dc => dc.Driver)
                .WithMany(d => d.DriverCompetitions)
                .HasForeignKey(dc => dc.DriverId);

            entity.HasOne(dc => dc.Competition)
                .WithMany(c => c.DriverCompetitions)
                .HasForeignKey(dc => dc.CompetitionId);
        });
    }
}