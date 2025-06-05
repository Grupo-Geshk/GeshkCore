using GeshkCore.API.DTOs.GeshkAdmin;
using GeshkCore.Domain.Entities.GeshkAdmin;
using GeshkCore.Infrastructure.DbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeshkCore.API.Controllers.GeshkAdmin
{
    [ApiController]
    [Route("api/admin/notas-internas")]
    public class NotasInternasController : ControllerBase
    {
        private readonly GeshkDbContext _context;

        public NotasInternasController(GeshkDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todas las notas internas registradas para un cliente específico.
        /// </summary>
        [HttpGet("{clientId}")]
        public async Task<ActionResult<IEnumerable<NotaInternaDto>>> GetByClient(Guid clientId)
        {
            var notas = await _context.NotasInternas
                .Where(n => n.ClientId == clientId)
                .OrderByDescending(n => n.Fecha)
                .Select(n => new NotaInternaDto
                {
                    Id = n.Id,
                    ClientId = n.ClientId,
                    UsuarioGeshk = n.UsuarioGeshk,
                    Contenido = n.Contenido,
                    Fecha = n.Fecha
                })
                .ToListAsync();

            return Ok(notas);
        }

        /// <summary>
        /// Crea una nueva nota interna sobre un cliente.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CrearNotaInternaDto dto)
        {
            var nota = new NotaInterna
            {
                ClientId = dto.ClientId,
                UsuarioGeshk = dto.UsuarioGeshk,
                Contenido = dto.Contenido,
                Fecha = DateTime.UtcNow
            };

            _context.NotasInternas.Add(nota);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByClient), new { clientId = dto.ClientId }, null);
        }
    }
}
