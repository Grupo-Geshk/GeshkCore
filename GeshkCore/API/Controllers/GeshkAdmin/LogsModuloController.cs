using Microsoft.AspNetCore.Mvc;
using GeshkCore.API.DTOs.GeshkAdmin;
using GeshkCore.Infrastructure.DbContext;
using GeshkCore.Domain.Entities.GeshkAdmin;

namespace GeshkCore.API.Controllers.GeshkAdmin
{
    [ApiController]
    [Route("api/admin/logs-modulo")]
    public class LogsModuloController : ControllerBase
    {
        private readonly GeshkDbContext _context;

        public LogsModuloController(GeshkDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET /api/admin/logs-modulo
        /// Lista todos los logs del sistema registrados por módulo
        /// </summary>
        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var logs = _context.LogsModulo
                .OrderByDescending(l => l.Fecha)
                .Select(l => new LogModuloDto
                {
                    Id = l.Id,
                    ClientId = l.ClientId,
                    Modulo = l.Modulo,
                    TipoEvento = l.TipoEvento,
                    Detalle = l.Detalle,
                    Fecha = l.Fecha
                })
                .ToList();

            return Ok(logs);
        }

        /// <summary>
        /// POST /api/admin/logs-modulo
        /// Registrar un nuevo log desde algún módulo del sistema
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearLogModuloDto dto)
        {
            var log = new LogModulo
            {
                Id = Guid.NewGuid(),
                ClientId = dto.ClientId,
                Modulo = dto.Modulo,
                TipoEvento = dto.TipoEvento,
                Detalle = dto.Detalle,
                Fecha = DateTime.UtcNow
            };

            await _context.LogsModulo.AddAsync(log);
            await _context.SaveChangesAsync();

            return Ok(new { log.Id, mensaje = "Log registrado correctamente." });
        }
    }
}
