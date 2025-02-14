using AplicacionesWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace AplicacionesWeb.Data
{
    public class AppDBContext: DbContext
    {
        public AppDBContext(DbContextOptions options): base(options)
        {
            
        }
        public DbSet<Libro> libros { get; set; }
        public DbSet<Autor> autores { get; set; }
        public DbSet<categoria> categorias { get; set; }

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Autor>(tabla =>
            {
                tabla.HasKey(columna => columna.Id);
                tabla.Property(a => a.Id).UseIdentityColumn().ValueGeneratedOnAdd();
                tabla.Property(a => a.Nombre).IsRequired();
                tabla.Property(a => a.Nacionalidad).IsRequired();
                tabla.ToTable("Autor");
            });

            modelBuilder.Entity<Libro>(tabla =>
            {
                tabla.HasKey(l => l.Id);
                tabla.Property(l => l.Id).UseIdentityColumn().ValueGeneratedOnAdd();
                tabla.Property(l => l.Titulo).IsRequired();
                tabla.Property(l => l.AñoPublicación).IsRequired();
                tabla.Property(l => l.Resumen).IsRequired();
                tabla.Property(l => l.AutorId).IsRequired();
                tabla.Property(l => l.CategoriaId).IsRequired();
                tabla.HasOne(l => l.Autor).WithMany(a => a.Libros).HasForeignKey(l => l.AutorId);
                tabla.HasOne(l => l.Categoria).WithMany(c => c.Libros).HasForeignKey(l => l.CategoriaId);
                tabla.ToTable("Libro");
            });

            modelBuilder.Entity<categoria>(tabla =>
            {
                tabla.HasKey(c => c.Id);
                tabla.Property(c => c.Id).UseIdentityColumn().ValueGeneratedOnAdd();
                tabla.Property(c => c.Nombre).IsRequired();
                tabla.ToTable("Categoria");
            });
        }



    }
}
