using Microsoft.AspNetCore.Mvc;
using GeshkCore.API.DTOs.Encuestas;
using GeshkCore.Infrastructure.DbContext;
using GeshkCore.Domain.Entities.Encuestas;
using GeshkCore.API.Controllers;

namespace GeshkCore.API.Controllers.Encuestas
{
    [ApiController]
    [Route("api/configuraciones-encuesta")]
    public class ConfiguracionesEncuestaController : ClienteControllerBase
    {
        private readonly GeshkDbContext _context;

        public ConfiguracionesEncuestaController(GeshkDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET /api/configuraciones-encuesta/{encuestaId}
        /// Obtener configuración especial de una encuesta del cliente autenticado
        /// </summary>
        [HttpGet("{encuestaId}")]
        public IActionResult ObtenerPorEncuesta(Guid encuestaId)
        {
            var clientId = ObtenerClientId();

            var encuesta = _context.Encuestas.FirstOrDefault(e => e.Id == encuestaId && e.ClientId == clientId);
            if (encuesta == null)
                return NotFound("Encuesta no encontrada o no autorizada.");

            var config = _context.ConfiguracionesEncuesta.FirstOrDefault(c => c.EncuestaId == encuestaId);
            if (config == null)
                return Ok(null);

            var dto = new ConfiguracionEncuestaDto
            {
                Id = config.Id,
                EncuestaId = config.EncuestaId,
                Visibilidad = config.Visibilidad,
                ActivarQR = config.ActivarQR,
                ActivarPostVenta = config.ActivarPostVenta,
                ActivarPostCita = config.ActivarPostCita
            };

            return Ok(dto);
        }

        /// <summary>
        /// POST /api/configuraciones-encuesta
        /// Crear o actualizar configuración especial de una encuesta
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CrearOActualizar([FromBody] ConfiguracionEncuestaDto dto)
        {
            var clientId = ObtenerClientId();
            var encuesta = _context.Encuestas.FirstOrDefault(e => e.Id == dto.EncuestaId && e.ClientId == clientId);
            if (encuesta == null)
                return NotFound("Encuesta no encontrada o no autorizada.");

            var existente = _context.ConfiguracionesEncuesta.FirstOrDefault(c => c.EncuestaId == dto.EncuestaId);

            if (existente == null)
            {
                var nueva = new ConfiguracionEncuesta
                {
                    Id = Guid.NewGuid(),
                    EncuestaId = dto.EncuestaId,
                    Visibilidad = dto.Visibilidad,
                    ActivarQR = dto.ActivarQR,
                    ActivarPostVenta = dto.ActivarPostVenta,
                    ActivarPostCita = dto.ActivarPostCita
                };

                await _context.ConfiguracionesEncuesta.AddAsync(nueva);
            }
            else
            {
                existente.Visibilidad = dto.Visibilidad;
                existente.ActivarQR = dto.ActivarQR;
                existente.ActivarPostVenta = dto.ActivarPostVenta;
                existente.ActivarPostCita = dto.ActivarPostCita;
            }

            await _context.SaveChangesAsync();
            return Ok(new { mensaje = "Configuración guardada." });
        }
    }
}