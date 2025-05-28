using GeshkCore.API.DTOs.GeshkAdmin;
using GeshkCore.Domain.Entities.GeshkAdmin;
using GeshkCore.Infrastructure.DbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeshkCore.API.Controllers.GeshkAdmin
{
    [ApiController]
    [Route("api/admin/historial-cuentas")]
    public class HistorialCuentasController : ControllerBase
    {
        private readonly GeshkDbContext _context;

        public HistorialCuentasController(GeshkDbContext context)
        {
            _context = context;
        }

        // GET: /api/admin/historial-cuentas
        /// <summary>
        /// Lista todos los eventos de historial de cuentas.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistorialCuentaDto>>> GetAll()
        {
            var data = await _context.HistorialCuentas
                .OrderByDescending(h => h.Fecha)
                .Select(h => new HistorialCuentaDto
                {
                    Id = h.Id,
                    ClientId = h.ClientId,
                    TipoEvento = h.TipoEvento,
                    Nota = h.Nota,
                    Fecha = h.Fecha
                })
                .ToListAsync();

            return Ok(data);
        }

        // GET: /api/admin/historial-cuentas/cliente/{clientId}
        /// <summary>
        /// Filtra historial por cliente.
        /// </summary>
        [HttpGet("cliente/{clientId}")]
        public async Task<ActionResult<IEnumerable<HistorialCuentaDto>>> GetByClient(Guid clientId)
        {
            var data = await _context.HistorialCuentas
                .Where(h => h.ClientId == clientId)
                .OrderByDescending(h => h.Fecha)
                .Select(h => new HistorialCuentaDto
                {
                    Id = h.Id,
                    ClientId = h.ClientId,
                    TipoEvento = h.TipoEvento,
                    Nota = h.Nota,
                    Fecha = h.Fecha
                })
                .ToListAsync();

            return Ok(data);
        }

        // POST: /api/admin/historial-cuentas
        /// <summary>
        /// Registra un nuevo evento en el historial.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CrearHistorialCuentaDto dto)
        {
            var historial = new HistorialCuenta
            {
                ClientId = dto.ClientId,
                TipoEvento = dto.TipoEvento,
                Nota = dto.Nota,
                Fecha = DateTime.UtcNow
            };

            _context.HistorialCuentas.Add(historial);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByClient), new { clientId = dto.ClientId }, null);
        }
    }
}
