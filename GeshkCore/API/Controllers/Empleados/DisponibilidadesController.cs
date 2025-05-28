using Microsoft.AspNetCore.Mvc;
using GeshkCore.API.DTOs.Empleados;
using GeshkCore.Infrastructure.DbContext;
using GeshkCore.Domain.Entities.Empleados;
using GeshkCore.API.Controllers;
using System.Text.Json;

namespace GeshkCore.API.Controllers.Empleados
{
    [ApiController]
    [Route("api/disponibilidades")]
    public class DisponibilidadesController : ClienteControllerBase
    {
        private readonly GeshkDbContext _context;

        public DisponibilidadesController(GeshkDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET /api/disponibilidades/{empleadoId}
        /// Obtener disponibilidad de un empleado del cliente autenticado
        /// </summary>
        [HttpGet("{empleadoId}")]
        public IActionResult Obtener(Guid empleadoId)
        {
            var clientId = ObtenerClientId();
            var empleado = _context.Empleados.FirstOrDefault(e => e.Id == empleadoId && e.ClientId == clientId);
            if (empleado == null)
                return NotFound("Empleado no encontrado o no autorizado.");

            var disponibilidad = _context.Disponibilidades.FirstOrDefault(d => d.EmpleadoId == empleadoId);
            if (disponibilidad == null)
                return Ok(null);

            var dto = new DisponibilidadDto
            {
                Id = disponibilidad.Id,
                EmpleadoId = disponibilidad.EmpleadoId,
                FechasBloqueadas = JsonSerializer.Deserialize<List<string>>(disponibilidad.FechasBloqueadasJson)!,
                Vacaciones = JsonSerializer.Deserialize<List<string>>(disponibilidad.VacacionesJson)!
            };

            return Ok(dto);
        }

        /// <summary>
        /// POST /api/disponibilidades
        /// Crear o actualizar disponibilidad de un empleado del cliente autenticado
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CrearOActualizar([FromBody] CrearDisponibilidadDto dto)
        {
            var clientId = ObtenerClientId();
            var empleado = _context.Empleados.FirstOrDefault(e => e.Id == dto.EmpleadoId && e.ClientId == clientId);
            if (empleado == null)
                return NotFound("Empleado no encontrado o no autorizado.");

            var disponibilidad = _context.Disponibilidades.FirstOrDefault(d => d.EmpleadoId == dto.EmpleadoId);

            var fechasJson = JsonSerializer.Serialize(dto.FechasBloqueadas);
            var vacacionesJson = JsonSerializer.Serialize(dto.Vacaciones);

            if (disponibilidad == null)
            {
                var nueva = new Disponibilidad
                {
                    Id = Guid.NewGuid(),
                    EmpleadoId = dto.EmpleadoId,
                    FechasBloqueadasJson = fechasJson,
                    VacacionesJson = vacacionesJson
                };

                await _context.Disponibilidades.AddAsync(nueva);
            }
            else
            {
                disponibilidad.FechasBloqueadasJson = fechasJson;
                disponibilidad.VacacionesJson = vacacionesJson;
            }

            await _context.SaveChangesAsync();
            return Ok(new { mensaje = "Disponibilidad actualizada." });
        }
    }
}
