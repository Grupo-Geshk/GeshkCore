using Microsoft.AspNetCore.Mvc;
using GeshkCore.API.DTOs.Empleados;
using GeshkCore.Infrastructure.DbContext;
using GeshkCore.Domain.Entities.Empleados;
using GeshkCore.API.Controllers;

namespace GeshkCore.API.Controllers.Empleados
{
    [ApiController]
    [Route("api/asistencias")]
    public class AsistenciasController : ClienteControllerBase
    {
        private readonly GeshkDbContext _context;

        public AsistenciasController(GeshkDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET /api/asistencias/{empleadoId}
        /// Obtener historial de asistencias de un empleado
        /// </summary>
        [HttpGet("{empleadoId}")]
        public IActionResult ObtenerPorEmpleado(Guid empleadoId)
        {
            var clientId = ObtenerClientId();
            var empleado = _context.Empleados.FirstOrDefault(e => e.Id == empleadoId && e.ClientId == clientId);
            if (empleado == null)
                return NotFound("Empleado no encontrado o no autorizado.");

            var asistencias = _context.Asistencias
                .Where(a => a.EmpleadoId == empleadoId)
                .OrderByDescending(a => a.Fecha)
                .Select(a => new AsistenciaDto
                {
                    Id = a.Id,
                    EmpleadoId = a.EmpleadoId,
                    Fecha = a.Fecha,
                    Tipo = a.Tipo,
                    Observacion = a.Observacion,
                    Usuario = a.Usuario
                })
                .ToList();

            return Ok(asistencias);
        }

        /// <summary>
        /// POST /api/asistencias
        /// Registrar una asistencia para un empleado
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Registrar([FromBody] CrearAsistenciaDto dto)
        {
            var clientId = ObtenerClientId();
            var empleado = _context.Empleados.FirstOrDefault(e => e.Id == dto.EmpleadoId && e.ClientId == clientId);
            if (empleado == null)
                return NotFound("Empleado no encontrado o no autorizado.");

            var asistencia = new Asistencia
            {
                Id = Guid.NewGuid(),
                EmpleadoId = dto.EmpleadoId,
                Fecha = dto.Fecha,
                Tipo = dto.Tipo,
                Observacion = dto.Observacion,
                Usuario = dto.Usuario
            };

            await _context.Asistencias.AddAsync(asistencia);
            await _context.SaveChangesAsync();

            return Ok(new { asistencia.Id, mensaje = "Asistencia registrada." });
        }
    }
}