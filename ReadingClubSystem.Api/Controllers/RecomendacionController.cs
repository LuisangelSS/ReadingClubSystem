using Microsoft.AspNetCore.Mvc;
using ReadingClubSystem.Domain.Entities;
using ReadingClubSystem.Api.Data;
using Microsoft.EntityFrameworkCore;

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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recomendacion>>> GetRecomendaciones()
        {
            return await _context.Recomendaciones.ToListAsync();
        }

        // Método para obtener una recomendación específica por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Recomendacion>> GetRecomendacion(int id)
        {
            var recomendacion = await _context.Recomendaciones.FindAsync(id);

            if (recomendacion == null)
            {
                return NotFound();
            }

            return recomendacion;
        }

        // Método para crear una nueva recomendación
        [HttpPost]
        public async Task<ActionResult<Recomendacion>> PostRecomendacion(Recomendacion recomendacion)
        {
            _context.Recomendaciones.Add(recomendacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecomendacion", new { id = recomendacion.Id }, recomendacion);
        }

        // Método para actualizar una recomendación existente
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecomendacion(int id, Recomendacion recomendacion)
        {
            if (id != recomendacion.Id)
            {
                return BadRequest();
            }

            _context.Entry(recomendacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Recomendaciones.Any(e => e.Id == id))
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

        // Método para eliminar una recomendación
        [HttpDelete("{id}")]
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
