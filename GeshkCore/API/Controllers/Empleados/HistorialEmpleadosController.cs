using Microsoft.AspNetCore.Mvc;
using GeshkCore.API.DTOs.Empleados;
using GeshkCore.Infrastructure.DbContext;
using GeshkCore.Domain.Entities.Empleados;
using GeshkCore.API.Controllers;

namespace GeshkCore.API.Controllers.Empleados
{
    [ApiController]
    [Route("api/historial-empleados")]
    public class HistorialEmpleadosController : ClienteControllerBase
    {
        private readonly GeshkDbContext _context;

        public HistorialEmpleadosController(GeshkDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET /api/historial-empleados/{empleadoId}
        /// Obtener historial de eventos de un empleado del cliente autenticado
        /// </summary>
        [HttpGet("{empleadoId}")]
        public IActionResult ObtenerPorEmpleado(Guid empleadoId)
        {
            var clientId = ObtenerClientId();
            var empleado = _context.Empleados.FirstOrDefault(e => e.Id == empleadoId && e.ClientId == clientId);
            if (empleado == null)
                return NotFound("Empleado no encontrado o no autorizado.");

            var historial = _context.HistorialEmpleados
                .Where(h => h.EmpleadoId == empleadoId)
                .OrderByDescending(h => h.Fecha)
                .Select(h => new HistorialEmpleadoDto
                {
                    Id = h.Id,
                    EmpleadoId = h.EmpleadoId,
                    Accion = h.Accion,
                    Comentario = h.Comentario,
                    RegistradoPor = h.RegistradoPor,
                    Fecha = h.Fecha
                })
                .ToList();

            return Ok(historial);
        }

        /// <summary>
        /// POST /api/historial-empleados
        /// Registrar un evento interno sobre un empleado (cambio de puesto, advertencia, etc.)
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Registrar([FromBody] CrearHistorialEmpleadoDto dto)
        {
            var clientId = ObtenerClientId();
            var empleado = _context.Empleados.FirstOrDefault(e => e.Id == dto.EmpleadoId && e.ClientId == clientId);
            if (empleado == null)
                return NotFound("Empleado no encontrado o no autorizado.");

            var historial = new HistorialEmpleado
            {
                Id = Guid.NewGuid(),
                EmpleadoId = dto.EmpleadoId,
                Accion = dto.Accion,
                Comentario = dto.Comentario,
                RegistradoPor = dto.RegistradoPor,
                Fecha = dto.Fecha
            };

            await _context.HistorialEmpleados.AddAsync(historial);
            await _context.SaveChangesAsync();

            return Ok(new { historial.Id, mensaje = "Evento registrado en el historial del empleado." });
        }
    }
}
