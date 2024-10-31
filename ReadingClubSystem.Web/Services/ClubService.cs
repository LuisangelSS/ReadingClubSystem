using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using ReadingClubSystem.Domain.Entities;
using ReadingClubSystem.Web.Services.Interfaces;

namespace ReadingClubSystem.Web.Services
{
    public class ClubService : IClubService
    {
        private readonly HttpClient _httpClient;

        public ClubService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7146/api/Club/");
        }

        public async Task<IEnumerable<Club>> GetAllClubsAsync()
        {
            var response = await _httpClient.GetAsync("listado/");
            if (response.IsSuccessStatusCode)
            {
                var clubes = await response.Content.ReadFromJsonAsync<IEnumerable<Club>>();
                return clubes;
            }
            return new List<Club>();
        }

        public async Task<Club> GetClubByIdAsync(int id)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.Preserve
            };

            return await _httpClient.GetFromJsonAsync<Club>($"detalle/{id}", options);
        }

        public async Task AddClubAsync(Club club)
        {
            var response = await _httpClient.PostAsJsonAsync("crear", club);
            response.EnsureSuccessStatusCode(); // Asegúrate de que la solicitud fue exitosa
        }

        public async Task UpdateClubAsync(Club club)
        {
            var response = await _httpClient.PutAsJsonAsync($"actualizar/{club.Id}", club);
            response.EnsureSuccessStatusCode(); // Asegúrate de que la solicitud fue exitosa
        }

        public async Task DeleteClubAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"eliminar/{id}");
            response.EnsureSuccessStatusCode(); // Asegúrate de que la solicitud fue exitosa
        }
    }
}
