using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using ReadingClubSystem.Domain.Entities;
using ReadingClubSystem.Web.Services.Interfaces;
using ReadingClubSystem.Domain.ViewModels;

namespace ReadingClubSystem.Web.Services
{
    public class RecomendacionService : IRecomendacionService
    {
        private readonly HttpClient _httpClient;

        public RecomendacionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7146/api/Recomendacion/");
        }

        public async Task<IEnumerable<Recomendacion>> GetAllRecomendacionesAsync()
        {
            var response = await _httpClient.GetAsync("listado/");
            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    ReferenceHandler = ReferenceHandler.Preserve
                };

                var recomendaciones = await response.Content.ReadFromJsonAsync<IEnumerable<Recomendacion>>(options);
                return recomendaciones;
            }
            return new List<Recomendacion>();
        }

        public async Task<Recomendacion> GetRecomendacionByIdAsync(int id)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.Preserve
            };

            return await _httpClient.GetFromJsonAsync<Recomendacion>($"detalle/{id}", options);
        }



        //crear
        public async Task AddRecomendacionAsync(RecomendacionDto recomendacion)
        {
            var response = await _httpClient.PostAsJsonAsync("crear", recomendacion);
            response.EnsureSuccessStatusCode(); // Asegúrate de que la solicitud fue exitosa
        }

        public async Task UpdateRecomendacionAsync(Recomendacion recomendacion)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var response = await _httpClient.PutAsJsonAsync($"actualizar/{recomendacion.Id}", recomendacion, options);
            response.EnsureSuccessStatusCode(); // Asegúrate de que la solicitud fue exitosa
        }


        public async Task DeleteRecomendacionAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"eliminar/{id}");
            response.EnsureSuccessStatusCode(); // Asegúrate de que la solicitud fue exitosa
        }
    }
}
