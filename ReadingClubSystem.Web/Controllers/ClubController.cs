using Microsoft.AspNetCore.Mvc;
using ReadingClubSystem.Infrastructure.Interfaces;
using ReadingClubSystem.Domain.Entities;
using System.Threading.Tasks;

namespace ReadingClubSystem.Web.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubService _clubService;

        public ClubController(IClubService clubService)
        {
            _clubService = clubService;
        }

        // Método para listar todos los clubes
        public async Task<IActionResult> Index()
        {
            var clubes = await _clubService.GetAllClubsAsync();
            return View(clubes);
        }

        // Método para mostrar el formulario de creación
        public IActionResult Create()
        {
            return View();
        }

        // Método para crear un nuevo club
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Club club)
        {
            if (ModelState.IsValid)
            {
                await _clubService.AddClubAsync(club);
                return RedirectToAction(nameof(Index));
            }
            return View(club);
        }

        // Método para mostrar el formulario de edición
        public async Task<IActionResult> Edit(int id)
        {
            var club = await _clubService.GetClubByIdAsync(id);
            if (club == null)
            {
                return NotFound();
            }
            return View(club);
        }

        // Método para actualizar un club existente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Club club)
        {
            if (ModelState.IsValid)
            {
                await _clubService.UpdateClubAsync(club);
                return RedirectToAction(nameof(Index));
            }
            return View(club);
        }

        // Método para mostrar el formulario de eliminación
        public async Task<IActionResult> Delete(int id)
        {
            var club = await _clubService.GetClubByIdAsync(id);
            if (club == null)
            {
                return NotFound();
            }
            return View(club);
        }

        // Método para confirmar la eliminación
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _clubService.DeleteClubAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
