using Microsoft.AspNetCore.Mvc;
using ReadingClubSystem.Web.Services.Interfaces;
using ReadingClubSystem.Domain.Entities;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReadingClubSystem.Web.Services;
using ReadingClubSystem.Domain.ViewModels;

namespace ReadingClubSystem.Web.Controllers
{
    public class ReunionController : Controller
    {
        private readonly IReunionService _reunionService;
        private readonly IClubService _clubService;

        public ReunionController(IReunionService reunionService, IClubService clubService)
        {
            _reunionService = reunionService;
            _clubService = clubService;
        }

        // Método para listar todas las reuniones
        public async Task<IActionResult> Index()
        {
            var reuniones = await _reunionService.GetAllReunionesAsync();
            return View(reuniones);
        }

        // Método para mostrar el formulario de creación
        public async Task<IActionResult> Create()
        {

            var clubes = await _clubService.GetAllClubsAsync();
            ViewBag.Clubes = new SelectList(clubes, "Id", "Nombre");

            return View();
        }

        // Método para crear una nueva reunión
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReunionDto reunionDto)
        {
            if (ModelState.IsValid)
            {
                await _reunionService.AddReunionAsync(reunionDto);
                return RedirectToAction(nameof(Index));
            }
            var clubes = await _clubService.GetAllClubsAsync();

            ViewBag.Clubes = new SelectList(clubes, "Id", "Nombre");

            return View(reunionDto);
        }

        // Método para mostrar el formulario de edición
        public async Task<IActionResult> Edit(int id)
        {
            var reunion = await _reunionService.GetReunionByIdAsync(id);
            if (reunion == null)
            {
                return NotFound();
            }

            var clubes = await _clubService.GetAllClubsAsync();

            ViewBag.Clubes = new SelectList(clubes, "Id", "Nombre");

            return View(reunion);

        }

        // Método para actualizar una reunión existente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Reunion reunion)
        {
            ModelState.Remove(nameof(Reunion.Club));

            if (ModelState.IsValid)
            {
                await _reunionService.UpdateReunionAsync(reunion);
                return RedirectToAction(nameof(Index));
            }

            var clubes = await _clubService.GetAllClubsAsync();

            ViewBag.Clubes = new SelectList(clubes, "Id", "Nombre");

            return View(reunion);
        }

        // Método para mostrar el formulario de eliminación
        public async Task<IActionResult> Delete(int id)
        {
            var reunion = await _reunionService.GetReunionByIdAsync(id);
            if (reunion == null)
            {
                return NotFound();
            }
            return View(reunion);
        }

        // Método para confirmar la eliminación
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _reunionService.DeleteReunionAsync(id);
            return RedirectToAction(nameof(Index));

        }
    }
}
