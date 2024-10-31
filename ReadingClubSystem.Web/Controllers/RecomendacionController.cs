using Microsoft.AspNetCore.Mvc;
using ReadingClubSystem.Web.Services.Interfaces;
using ReadingClubSystem.Domain.Entities;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using ReadingClubSystem.Web.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReadingClubSystem.Domain.ViewModels;

namespace ReadingClubSystem.Web.Controllers
{
    public class RecomendacionController : Controller
    {

        private readonly IRecomendacionService _recomendacionService;
        private readonly IUsuarioService _usuarioService;
        private readonly ILibroService _libroService;
        private readonly IClubService _clubService;
        public RecomendacionController(IRecomendacionService recomendacionService, 
                                        IUsuarioService usuarioService,
                                        ILibroService libroService,
                                        IClubService clubService)
        {
            _recomendacionService = recomendacionService;
            _usuarioService = usuarioService;
            _libroService = libroService;
            _clubService = clubService;
        }

        public async Task<IActionResult> Index()
        {
            var recomendaciones = await _recomendacionService.GetAllRecomendacionesAsync();
            return View(recomendaciones);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var usuarios = await _usuarioService.GetAllUsuariosAsync();
            var libros = await _libroService.GetAllLibrosAsync();
            var clubes = await _clubService.GetAllClubsAsync();

            ViewBag.Usuarios = new SelectList(usuarios, "Id", "Nombre");
            ViewBag.Libros = new SelectList(libros, "Id", "Titulo");
            ViewBag.Clubes = new SelectList(clubes, "Id", "Nombre");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RecomendacionDto recomendacionDto)
        {
            if (ModelState.IsValid)
            {
                await _recomendacionService.AddRecomendacionAsync(recomendacionDto);
                return RedirectToAction(nameof(Index));
            }

            var usuarios = await _usuarioService.GetAllUsuariosAsync();
            var libros = await _libroService.GetAllLibrosAsync();
            var clubes = await _clubService.GetAllClubsAsync();

            ViewBag.Usuarios = new SelectList(usuarios, "Id", "Nombre");
            ViewBag.Libros = new SelectList(libros, "Id", "Titulo");
            ViewBag.Clubes = new SelectList(clubes, "Id", "Nombre");

            return View(recomendacionDto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var recomendacion = await _recomendacionService.GetRecomendacionByIdAsync(id);
            if (recomendacion == null)
            {
                return NotFound();
            }

            var usuarios = await _usuarioService.GetAllUsuariosAsync();
            var libros = await _libroService.GetAllLibrosAsync();
            var clubes = await _clubService.GetAllClubsAsync();

            ViewBag.Usuarios = new SelectList(usuarios, "Id", "Nombre");
            ViewBag.Libros = new SelectList(libros, "Id", "Titulo");
            ViewBag.Clubes = new SelectList(clubes, "Id", "Nombre");

            return View(recomendacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Recomendacion recomendacion)
        {
            ModelState.Remove(nameof(Recomendacion.Usuario));
            ModelState.Remove(nameof(Recomendacion.Libro));
            ModelState.Remove(nameof(Recomendacion.Club));

            if (ModelState.IsValid)
            {
                await _recomendacionService.UpdateRecomendacionAsync(recomendacion);
                return RedirectToAction(nameof(Index));
            }

            var usuarios = await _usuarioService.GetAllUsuariosAsync();
            var libros = await _libroService.GetAllLibrosAsync();
            var clubes = await _clubService.GetAllClubsAsync();

            ViewBag.Usuarios = new SelectList(usuarios, "Id", "Nombre");
            ViewBag.Libros = new SelectList(libros, "Id", "Titulo");
            ViewBag.Clubes = new SelectList(clubes, "Id", "Nombre");

            return View(recomendacion);
        }

        // Método para mostrar el formulario de eliminación
        public async Task<IActionResult> Delete(int id)
        {
            var recomendacion = await _recomendacionService.GetRecomendacionByIdAsync(id);
            if (recomendacion == null)
                return NotFound();
            return View(recomendacion);
        }

        // Método para confirmar la eliminación
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _recomendacionService.DeleteRecomendacionAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
