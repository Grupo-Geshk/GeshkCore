using Microsoft.AspNetCore.Mvc;
using GeshkCore.API.DTOs.Encuestas;
using GeshkCore.Infrastructure.DbContext;
using GeshkCore.Domain.Entities.Encuestas;
using GeshkCore.API.Controllers;

namespace GeshkCore.API.Controllers.Encuestas
{
    [ApiController]
    [Route("api/preguntas")]
    public class PreguntasController : ClienteControllerBase
    {
        private readonly GeshkDbContext _context;

        public PreguntasController(GeshkDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET /api/preguntas/{encuestaId}
        /// Obtener preguntas de una encuesta del cliente autenticado
        /// </summary>
        [HttpGet("{encuestaId}")]
        public IActionResult ObtenerPorEncuesta(Guid encuestaId)
        {
            var clientId = ObtenerClientId();
            var encuesta = _context.Encuestas.FirstOrDefault(e => e.Id == encuestaId && e.ClientId == clientId);
            if (encuesta == null)
                return NotFound("Encuesta no encontrada o no autorizada.");

            var preguntas = _context.Preguntas
                .Where(p => p.EncuestaId == encuestaId)
                .Select(p => new PreguntaDto
                {
                    Id = p.Id,
                    EncuestaId = p.EncuestaId,
                    Texto = p.Texto,
                    Tipo = p.Tipo,
                    Requerida = p.Requerida
                })
                .ToList();

            return Ok(preguntas);
        }

        /// <summary>
        /// POST /api/preguntas
        /// Crear una nueva pregunta en una encuesta del cliente autenticado
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearPreguntaDto dto)
        {
            var clientId = ObtenerClientId();
            var encuesta = _context.Encuestas.FirstOrDefault(e => e.Id == dto.EncuestaId && e.ClientId == clientId);
            if (encuesta == null)
                return NotFound("Encuesta no encontrada o no autorizada.");

            var pregunta = new Pregunta
            {
                Id = Guid.NewGuid(),
                EncuestaId = dto.EncuestaId,
                Texto = dto.Texto,
                Tipo = dto.Tipo,
                Requerida = dto.Requerida
            };

            await _context.Preguntas.AddAsync(pregunta);
            await _context.SaveChangesAsync();

            return Ok(new { pregunta.Id, mensaje = "Pregunta creada." });
        }

        /// <summary>
        /// DELETE /api/preguntas/{id}
        /// Eliminar una pregunta de una encuesta del cliente autenticado
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var clientId = ObtenerClientId();
            var pregunta = _context.Preguntas.FirstOrDefault(p => p.Id == id);
            if (pregunta == null)
                return NotFound("Pregunta no encontrada.");

            var encuesta = _context.Encuestas.FirstOrDefault(e => e.Id == pregunta.EncuestaId && e.ClientId == clientId);
            if (encuesta == null)
                return Unauthorized("No tienes acceso a esta pregunta.");

            _context.Preguntas.Remove(pregunta);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Pregunta eliminada." });
        }
    }
}
