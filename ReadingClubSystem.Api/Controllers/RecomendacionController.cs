using Microsoft.AspNetCore.Mvc;
using ReadingClubSystem.Domain.Entities;
using ReadingClubSystem.Api.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using ReadingClubSystem.Domain.ViewModels;

namespace ReadingClubSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecomendacionController : ControllerBase
    {
        private readonly ReadingClubContext _context;

        public RecomendacionController(ReadingClubContext context)
        {
            _context = context;
        }

        // Método para obtener todas las recomendaciones
        [HttpGet("listado")]
        public async Task<ActionResult<IEnumerable<Recomendacion>>> GetRecomendaciones()
        {
            var recomendaciones = await _context.Recomendaciones
                .Include(r => r.Usuario)
                .Include(r => r.Libro)
                .Include(r => r.Club)
                .ToListAsync();

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            return new JsonResult(recomendaciones, options);
        }

        [HttpGet("detalle/{id}")]
        public async Task<ActionResult<Recomendacion>> GetRecomendacionById(int id)
        {
            var recomendacion = await _context.Recomendaciones
                .Include(r => r.Usuario)
                .Include(r => r.Libro)
                .Include(r => r.Club)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (recomendacion == null)
            {
                return NotFound();
            }

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };

            return new JsonResult(recomendacion, options);
        }

        //crear
        [HttpPost("crear")]
        public async Task<ActionResult<Recomendacion>> PostRecomendacion(RecomendacionDto recomendacionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var recomendacion = new Recomendacion
            {
                UsuarioId = recomendacionDto.UsuarioId,
                LibroId = recomendacionDto.LibroId,
                ClubId = recomendacionDto.ClubId,
                Fecha = recomendacionDto.Fecha
            };

            _context.Recomendaciones.Add(recomendacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRecomendacionById), new { id = recomendacion.Id }, recomendacion);
        }


        // PUT: api/Recomendacion/actualizar/5
        [HttpPut("actualizar/{id}")]
        public async Task<IActionResult> PutRecomendacion(int id, RecomendacionDto recomendacionDto)
        {
            if (id != recomendacionDto.Id)
            {
                return BadRequest();
            }

            var recomendacion = await _context.Recomendaciones.FindAsync(id);
            if (recomendacion == null)
            {
                return NotFound();
            }

            recomendacion.UsuarioId = recomendacionDto.UsuarioId;
            recomendacion.LibroId = recomendacionDto.LibroId;
            recomendacion.ClubId = recomendacionDto.ClubId;
            recomendacion.Fecha = recomendacionDto.Fecha;

            _context.Entry(recomendacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecomendacionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool RecomendacionExists(int id)
        {
            return _context.Recomendaciones.Any(e => e.Id == id);
        }


        // Método para eliminar una recomendación
        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> DeleteRecomendacion(int id)
        {
            var recomendacion = await _context.Recomendaciones.FindAsync(id);
            if (recomendacion == null)
            {
                return NotFound();
            }
            _context.Recomendaciones.Remove(recomendacion);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
