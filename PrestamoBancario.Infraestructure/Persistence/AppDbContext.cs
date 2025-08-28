using Microsoft.EntityFrameworkCore;
using PrestamoBancario.Domain.Entities;

namespace PrestamoBancario.Infraestructure.Persistence;

internal class AppDbContext : DbContext
{
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Prestamo> Prestamos => Set<Prestamo>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasIndex(x => x.Email).IsUnique();
            e.Property(x => x.Email).IsRequired();
            e.Property(x => x.Constrasena).IsRequired();
            e.Property(x => x.Rol).IsRequired();
        });

        modelBuilder.Entity<Prestamo>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Cantidad).HasPrecision(18, 2);
            e.Property(x => x.Estado).HasConversion<int>();
            e.HasIndex(x => new { x.IdUsuario, x.FechaCreacion });
        });
    }
}
