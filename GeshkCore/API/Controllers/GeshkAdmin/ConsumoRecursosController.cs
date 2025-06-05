using GeshkCore.API.DTOs.GeshkAdmin;
using GeshkCore.Domain.Entities.GeshkAdmin;
using GeshkCore.Infrastructure.DbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeshkCore.API.Controllers.GeshkAdmin
{
    [ApiController]
    [Route("api/admin/consumo-recursos")]
    public class ConsumoRecursosController : ControllerBase
    {
        private readonly GeshkDbContext _context;

        public ConsumoRecursosController(GeshkDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista el consumo de recursos de todos los clientes registrados.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsumoRecursoDto>>> GetAll()
        {
            var lista = await _context.ConsumoRecursos
                .OrderByDescending(c => c.FechaActualizacion)
                .Select(c => new ConsumoRecursoDto
                {
                    Id = c.Id,
                    ClientId = c.ClientId,
                    MbUsados = c.MbUsados,
                    AnchoBandaMb = c.AnchoBandaMb,
                    FechaActualizacion = c.FechaActualizacion
                })
                .ToListAsync();

            return Ok(lista);
        }

        /// <summary>
        /// Muestra el historial de consumo de un cliente específico.
        /// </summary>
        [HttpGet("cliente/{clientId}")]
        public async Task<ActionResult<IEnumerable<ConsumoRecursoDto>>> GetByClient(Guid clientId)
        {
            var historial = await _context.ConsumoRecursos
                .Where(c => c.ClientId == clientId)
                .OrderByDescending(c => c.FechaActualizacion)
                .Select(c => new ConsumoRecursoDto
                {
                    Id = c.Id,
                    ClientId = c.ClientId,
                    MbUsados = c.MbUsados,
                    AnchoBandaMb = c.AnchoBandaMb,
                    FechaActualizacion = c.FechaActualizacion
                })
                .ToListAsync();

            return Ok(historial);
        }
    }
}
