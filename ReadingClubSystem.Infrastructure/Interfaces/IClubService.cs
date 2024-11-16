using System.Collections.Generic;
using System.Threading.Tasks;
using ReadingClubSystem.Domain.Entities;

namespace ReadingClubSystem.Infrastructure.Interfaces
{
    public interface IClubService
    {
        Task<IEnumerable<Club>> GetAllClubsAsync();
        Task<Club> GetClubByIdAsync(int id);
        Task AddClubAsync(Club club);
        Task UpdateClubAsync(Club club);
        Task DeleteClubAsync(int id);
    }
}
