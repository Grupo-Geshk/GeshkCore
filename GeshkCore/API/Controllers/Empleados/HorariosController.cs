using Microsoft.AspNetCore.Mvc;
using GeshkCore.API.DTOs.Empleados;
using GeshkCore.Infrastructure.DbContext;
using GeshkCore.Domain.Entities.Empleados;
using GeshkCore.API.Controllers;

namespace GeshkCore.API.Controllers.Empleados
{
    [ApiController]
    [Route("api/horarios")]
    public class HorariosController : ClienteControllerBase
    {
        private readonly GeshkDbContext _context;

        public HorariosController(GeshkDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET /api/horarios/{empleadoId}
        /// Obtener horarios semanales del empleado autenticado
        /// </summary>
        [HttpGet("{empleadoId}")]
        public IActionResult ObtenerPorEmpleado(Guid empleadoId)
        {
            var clientId = ObtenerClientId();
            var empleado = _context.Empleados.FirstOrDefault(e => e.Id == empleadoId && e.ClientId == clientId);
            if (empleado == null)
                return NotFound("Empleado no encontrado o no autorizado.");

            var horarios = _context.HorariosSemanales
                .Where(h => h.EmpleadoId == empleadoId)
                .Select(h => new HorarioSemanalDto
                {
                    Id = h.Id,
                    EmpleadoId = h.EmpleadoId,
                    DiaSemana = h.DiaSemana,
                    HoraInicio = h.HoraInicio,
                    HoraFin = h.HoraFin,
                    Grupo = h.Grupo,
                    Ubicacion = h.Ubicacion
                })
                .ToList();

            return Ok(horarios);
        }

        /// <summary>
        /// POST /api/horarios
        /// Crear un nuevo horario semanal para un empleado
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearHorarioSemanalDto dto)
        {
            var clientId = ObtenerClientId();
            var empleado = _context.Empleados.FirstOrDefault(e => e.Id == dto.EmpleadoId && e.ClientId == clientId);
            if (empleado == null)
                return NotFound("Empleado no encontrado o no autorizado.");

            var horario = new HorarioSemanal
            {
                Id = Guid.NewGuid(),
                EmpleadoId = dto.EmpleadoId,
                DiaSemana = dto.DiaSemana,
                HoraInicio = dto.HoraInicio,
                HoraFin = dto.HoraFin,
                Grupo = dto.Grupo,
                Ubicacion = dto.Ubicacion
            };

            await _context.HorariosSemanales.AddAsync(horario);
            await _context.SaveChangesAsync();

            return Ok(new { horario.Id, mensaje = "Horario registrado." });
        }

        /// <summary>
        /// PUT /api/horarios/{id}
        /// Editar un horario existente
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(Guid id, [FromBody] CrearHorarioSemanalDto dto)
        {
            var clientId = ObtenerClientId();
            var horario = _context.HorariosSemanales.FirstOrDefault(h => h.Id == id);
            if (horario == null)
                return NotFound("Horario no encontrado.");

            var empleado = _context.Empleados.FirstOrDefault(e => e.Id == horario.EmpleadoId && e.ClientId == clientId);
            if (empleado == null)
                return Unauthorized("No tienes acceso a este recurso.");

            horario.DiaSemana = dto.DiaSemana;
            horario.HoraInicio = dto.HoraInicio;
            horario.HoraFin = dto.HoraFin;
            horario.Grupo = dto.Grupo;
            horario.Ubicacion = dto.Ubicacion;

            await _context.SaveChangesAsync();
            return Ok(new { mensaje = "Horario actualizado." });
        }

        /// <summary>
        /// DELETE /api/horarios/{id}
        /// Eliminar un horario del sistema
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var clientId = ObtenerClientId();
            var horario = _context.HorariosSemanales.FirstOrDefault(h => h.Id == id);
            if (horario == null)
                return NotFound("Horario no encontrado.");

            var empleado = _context.Empleados.FirstOrDefault(e => e.Id == horario.EmpleadoId && e.ClientId == clientId);
            if (empleado == null)
                return Unauthorized("No tienes acceso a este recurso.");

            _context.HorariosSemanales.Remove(horario);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Horario eliminado." });
        }
    }
}
