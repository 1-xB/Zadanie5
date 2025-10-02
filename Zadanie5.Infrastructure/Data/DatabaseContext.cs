using Microsoft.EntityFrameworkCore;
using Zadanie5.Core;
using Zadanie5.Core.Models;

namespace Zadanie5.Data;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Klient> Klienci { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Klient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Klienci_pkey");

            entity.ToTable("Klienci");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Pesel)
                .HasMaxLength(11)
                .HasColumnName("PESEL");
            entity.Property(e => e.Surname).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}