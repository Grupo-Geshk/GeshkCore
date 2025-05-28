using Microsoft.AspNetCore.Mvc;
using GeshkCore.API.DTOs.Empleados;
using GeshkCore.Infrastructure.DbContext;
using GeshkCore.Domain.Entities.Empleados;
using GeshkCore.API.Controllers;

namespace GeshkCore.API.Controllers.Empleados
{
    [ApiController]
    [Route("api/grupos-trabajo")]
    public class GruposTrabajoController : ClienteControllerBase
    {
        private readonly GeshkDbContext _context;

        public GruposTrabajoController(GeshkDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET /api/grupos-trabajo
        /// Lista todos los grupos de trabajo del cliente autenticado
        /// </summary>
        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var clientId = ObtenerClientId();

            var grupos = _context.GruposTrabajo
                .Where(g => g.ClientId == clientId)
                .Select(g => new GrupoTrabajoDto
                {
                    Id = g.Id,
                    Nombre = g.Nombre,
                    Descripcion = g.Descripcion
                })
                .ToList();

            return Ok(grupos);
        }

        /// <summary>
        /// POST /api/grupos-trabajo
        /// Crear un nuevo grupo de trabajo
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearGrupoTrabajoDto dto)
        {
            var grupo = new GrupoTrabajo
            {
                Id = Guid.NewGuid(),
                ClientId = ObtenerClientId(),
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion
            };

            await _context.GruposTrabajo.AddAsync(grupo);
            await _context.SaveChangesAsync();

            return Ok(new { grupo.Id, mensaje = "Grupo creado correctamente." });
        }

        /// <summary>
        /// DELETE /api/grupos-trabajo/{id}
        /// Eliminar un grupo de trabajo del cliente autenticado
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var clientId = ObtenerClientId();
            var grupo = _context.GruposTrabajo.FirstOrDefault(g => g.Id == id && g.ClientId == clientId);
            if (grupo == null)
                return NotFound("Grupo no encontrado o no autorizado.");

            _context.GruposTrabajo.Remove(grupo);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Grupo eliminado correctamente." });
        }
    }
}
