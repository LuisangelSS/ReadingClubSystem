using Microsoft.EntityFrameworkCore;
using ReadingClubSystem.Domain.Entities;

namespace ReadingClubSystem.Api.Data
{
    public class ReadingClubContext : DbContext
    {
        public ReadingClubContext(DbContextOptions<ReadingClubContext> options)
            : base(options)
        {
        }

        // Constructor sin parámetros para el tiempo de diseño
        public ReadingClubContext() { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Club> Clubes { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Reunion> Reuniones { get; set; }
        public DbSet<Recomendacion> Recomendaciones { get; set; }
    }
}
