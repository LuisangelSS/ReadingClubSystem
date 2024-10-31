using System.Collections.Generic;
using System.Threading.Tasks;
using ReadingClubSystem.Domain.Entities;

namespace ReadingClubSystem.Web.Services.Interfaces
{
    public interface ILibroService
    {
        Task<IEnumerable<Libro>> GetAllLibrosAsync();
        Task<Libro> GetLibroByIdAsync(int id);
        Task AddLibroAsync(Libro libro);
        Task UpdateLibroAsync(Libro libro);
        Task DeleteLibroAsync(int id);
    }
}
