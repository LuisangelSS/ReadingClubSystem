using System.Collections.Generic;
using System.Threading.Tasks;
using ReadingClubSystem.Domain.Entities;
using ReadingClubSystem.Domain.ViewModels;

namespace ReadingClubSystem.Infrastructure.Interfaces
{
    public interface IReunionService
    {
        Task<IEnumerable<Reunion>> GetAllReunionesAsync();
        Task<Reunion> GetReunionByIdAsync(int id);
        Task AddReunionAsync(ReunionDto reunionDto);
        Task UpdateReunionAsync(Reunion reunion);
        Task DeleteReunionAsync(int id);
    }
}
