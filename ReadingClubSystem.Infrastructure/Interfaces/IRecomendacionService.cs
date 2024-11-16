using System.Collections.Generic;
using System.Threading.Tasks;
using ReadingClubSystem.Domain.Entities;
using ReadingClubSystem.Domain.ViewModels;

namespace ReadingClubSystem.Infrastructure.Interfaces
{
    public interface IRecomendacionService
    {
        Task<IEnumerable<Recomendacion>> GetAllRecomendacionesAsync();
        Task<Recomendacion> GetRecomendacionByIdAsync(int id);
        Task AddRecomendacionAsync(RecomendacionDto recomendacionDto);
        Task UpdateRecomendacionAsync(Recomendacion recomendacion);
        Task DeleteRecomendacionAsync(int id);

    }
}
