using Microsoft.AspNetCore.Mvc;
using GeshkCore.API.DTOs.Encuestas;
using GeshkCore.Infrastructure.DbContext;
using GeshkCore.Domain.Entities.Encuestas;
using GeshkCore.API.Controllers;

namespace GeshkCore.API.Controllers.Encuestas
{
    [ApiController]
    [Route("api/respuestas")]
    public class RespuestasController : ClienteControllerBase
    {
        private readonly GeshkDbContext _context;

        public RespuestasController(GeshkDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET /api/respuestas/{encuestaId}
        /// Obtener todas las respuestas de una encuesta del cliente autenticado
        /// </summary>
        [HttpGet("{encuestaId}")]
        public IActionResult ObtenerPorEncuesta(Guid encuestaId)
        {
            var clientId = ObtenerClientId();
            var encuesta = _context.Encuestas.FirstOrDefault(e => e.Id == encuestaId && e.ClientId == clientId);
            if (encuesta == null)
                return NotFound("Encuesta no encontrada o no autorizada.");

            var respuestas = _context.Respuestas
                .Where(r => r.EncuestaId == encuestaId)
                .Select(r => new RespuestaDto
                {
                    Id = r.Id,
                    EncuestaId = r.EncuestaId,
                    PreguntaId = r.PreguntaId,
                    RespuestaTexto = r.RespuestaTexto,
                    Fecha = r.Fecha,
                    CanalOrigen = r.CanalOrigen
                })
                .ToList();

            return Ok(respuestas);
        }

        /// <summary>
        /// POST /api/respuestas
        /// Registrar una nueva respuesta a una encuesta (público o post-evento)
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearRespuestaDto dto)
        {
            var pregunta = _context.Preguntas.FirstOrDefault(p => p.Id == dto.PreguntaId);
            var encuesta = _context.Encuestas.FirstOrDefault(e => e.Id == dto.EncuestaId);

            if (pregunta == null || encuesta == null)
                return NotFound("Encuesta o pregunta inválida.");

            if (pregunta.EncuestaId != encuesta.Id)
                return BadRequest("La pregunta no pertenece a la encuesta indicada.");

            var respuesta = new Respuesta
            {
                Id = Guid.NewGuid(),
                EncuestaId = dto.EncuestaId,
                PreguntaId = dto.PreguntaId,
                RespuestaTexto = dto.RespuestaTexto,
                Fecha = DateTime.UtcNow,
                CanalOrigen = dto.CanalOrigen
            };

            await _context.Respuestas.AddAsync(respuesta);
            await _context.SaveChangesAsync();

            return Ok(new { respuesta.Id, mensaje = "Respuesta registrada." });
        }
    }
}
