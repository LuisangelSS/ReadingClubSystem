using System.Collections.Generic;
using System.Threading.Tasks;
using ReadingClubSystem.Domain.Entities;

namespace ReadingClubSystem.Infrastructure.Interfaces
{
    public interface IUsuarioService
    {
        Task<IEnumerable<Usuario>> GetAllUsuariosAsync();
        Task AddUsuarioAsync(Usuario usuario);
        Task<Usuario> GetUsuarioByIdAsync(int id);
        Task UpdateUsuarioAsync(Usuario usuario);
        Task DeleteUsuarioAsync(int id);
    }
}
