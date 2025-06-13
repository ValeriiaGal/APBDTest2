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


    public DbSet<Language> Languages { get; set; }
    public DbSet<Tasks> Tasks { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Record> Records { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Language>()
            .HasKey(l => l.Id);

        modelBuilder.Entity<Language>()
            .Property(l => l.Name)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<Task>()
            .HasKey(t => t.Id);

        modelBuilder.Entity<Tasks>()
            .Property(t => t.Name)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<Tasks>()
            .Property(t => t.Description)
            .HasMaxLength(2000);

        modelBuilder.Entity<Student>()
            .HasKey(s => s.Id);

        modelBuilder.Entity<Student>()
            .Property(s => s.FirstName)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<Student>()
            .Property(s => s.LastName)
            .HasMaxLength(100)
            .IsRequired();

        modelBuilder.Entity<Student>()
            .Property(s => s.Email)
            .HasMaxLength(250)
            .IsRequired();

        modelBuilder.Entity<Record>()
            .HasKey(r => r.Id);

        modelBuilder.Entity<Record>()
            .HasOne(r => r.Language)
            .WithMany(l => l.Records)
            .HasForeignKey(r => r.LanguageId);

        modelBuilder.Entity<Record>()
            .HasOne(r => r.Tasks)
            .WithMany(t => t.Records)
            .HasForeignKey(r => r.TaskId);

        modelBuilder.Entity<Record>()
            .HasOne(r => r.Student)
            .WithMany(s => s.Records)
            .HasForeignKey(r => r.StudentId);

        modelBuilder.Entity<Record>()
            .Property(r => r.ExecutionTime)
            .IsRequired();

        modelBuilder.Entity<Record>()
            .Property(r => r.CreatedAt)
            .HasColumnType("datetime2")
            .IsRequired();
    }
}