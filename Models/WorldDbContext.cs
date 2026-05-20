using Microsoft.EntityFrameworkCore;

namespace WorldApi.Models;

public class WorldDbContext : DbContext
{
    public WorldDbContext(DbContextOptions<WorldDbContext> options) : base(options)
    {
    }

    public DbSet<City> Cities { get; set; } = null!;
    public DbSet<Country> Countries { get; set; } = null!;
    public DbSet<CountryLanguage> CountryLanguages { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.ToTable("city");
            entity.HasKey(e => e.ID);
            entity.Property(e => e.Name).HasMaxLength(35);
            entity.Property(e => e.CountryCode).HasMaxLength(3);
            entity.Property(e => e.District).HasMaxLength(20);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("country");
            entity.HasKey(e => e.Code);
            entity.Property(e => e.Code).HasMaxLength(3);
            entity.Property(e => e.Name).HasMaxLength(52);
            entity.Property(e => e.Continent).HasColumnType("enum('Asia','Europe','North America','Africa','Oceania','Antarctica','South America')");
            entity.Property(e => e.Region).HasMaxLength(26);
            entity.Property(e => e.LocalName).HasMaxLength(45);
            entity.Property(e => e.GovernmentForm).HasMaxLength(45);
            entity.Property(e => e.HeadOfState).HasMaxLength(60);
            entity.Property(e => e.Code2).HasMaxLength(2);
        });

        modelBuilder.Entity<CountryLanguage>(entity =>
        {
            entity.ToTable("countrylanguage");
            entity.HasKey(e => new { e.CountryCode, e.Language });
            entity.Property(e => e.CountryCode).HasMaxLength(3);
            entity.Property(e => e.Language).HasMaxLength(30);
            entity.Property(e => e.IsOfficial).HasColumnType("enum('T','F')");
        });
    }
}
