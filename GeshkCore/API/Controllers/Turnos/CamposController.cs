using Microsoft.AspNetCore.Mvc;
using GeshkCore.API.DTOs.Turnos;
using GeshkCore.Infrastructure.DbContext;
using GeshkCore.Domain.Entities.Turnos;

namespace GeshkCore.API.Controllers.Turnos
{
    [ApiController]
    [Route("api/campos")]
    public class CamposController : ControllerBase
    {
        // ============================
        // ⚙️ CONFIGURACIÓN INICIAL
        // ============================

        private readonly GeshkDbContext _context;

        public CamposController(GeshkDbContext context)
        {
            _context = context;
        }

        // ============================
        // 📄 ENDPOINTS DE CAMPOS PERSONALIZADOS
        // ============================

        /// POST /api/campos
        /// Crear un nuevo campo personalizado para un formulario
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearCampoPersonalizadoDto dto)
        {
            var campo = new CampoPersonalizado
            {
                Id = Guid.NewGuid(),
                FormularioId = dto.FormularioId,
                Etiqueta = dto.Etiqueta,
                Tipo = dto.Tipo,
                Requerido = dto.Requerido
            };

            await _context.CamposPersonalizados.AddAsync(campo);
            await _context.SaveChangesAsync();

            return Ok(new { campo.Id, mensaje = "Campo creado correctamente." });
        }

        /// DELETE /api/campos/{id}
        /// Eliminar un campo personalizado por ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var campo = await _context.CamposPersonalizados.FindAsync(id);
            if (campo == null)
                return NotFound("Campo no encontrado.");

            _context.CamposPersonalizados.Remove(campo);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Campo eliminado." });
        }
    }
}
