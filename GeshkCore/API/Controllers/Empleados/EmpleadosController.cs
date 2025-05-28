using Microsoft.AspNetCore.Mvc;
using GeshkCore.API.DTOs.Empleados;
using GeshkCore.Infrastructure.DbContext;
using GeshkCore.Domain.Entities.Empleados;
using GeshkCore.API.Controllers;

namespace GeshkCore.API.Controllers.Empleados
{
    [ApiController]
    [Route("api/empleados")]
    public class EmpleadosController : ClienteControllerBase
    {
        private readonly GeshkDbContext _context;

        public EmpleadosController(GeshkDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET /api/empleados
        /// Lista todos los empleados del cliente autenticado
        /// </summary>
        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var clientId = ObtenerClientId();

            var empleados = _context.Empleados
                .Where(e => e.ClientId == clientId)
                .Select(e => new EmpleadoDto
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    Correo = e.Correo,
                    Telefono = e.Telefono,
                    Cargo = e.Cargo,
                    AvatarUrl = e.AvatarUrl,
                    FechaIngreso = e.FechaIngreso,
                    Activo = e.Activo
                })
                .ToList();

            return Ok(empleados);
        }

        /// <summary>
        /// GET /api/empleados/{id}
        /// Obtener detalles de un empleado del cliente autenticado
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(Guid id)
        {
            var clientId = ObtenerClientId();
            var empleado = _context.Empleados.FirstOrDefault(e => e.Id == id && e.ClientId == clientId);

            if (empleado == null)
                return NotFound("Empleado no encontrado o no autorizado.");

            var dto = new EmpleadoDto
            {
                Id = empleado.Id,
                Nombre = empleado.Nombre,
                Correo = empleado.Correo,
                Telefono = empleado.Telefono,
                Cargo = empleado.Cargo,
                AvatarUrl = empleado.AvatarUrl,
                FechaIngreso = empleado.FechaIngreso,
                Activo = empleado.Activo
            };

            return Ok(dto);
        }

        /// <summary>
        /// POST /api/empleados
        /// Crear un nuevo empleado
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearEmpleadoDto dto)
        {
            var empleado = new Empleado
            {
                Id = Guid.NewGuid(),
                ClientId = ObtenerClientId(),
                Nombre = dto.Nombre,
                Correo = dto.Correo,
                Telefono = dto.Telefono,
                Cargo = dto.Cargo,
                AvatarUrl = dto.AvatarUrl,
                FechaIngreso = dto.FechaIngreso,
                Activo = true
            };

            await _context.Empleados.AddAsync(empleado);
            await _context.SaveChangesAsync();

            return Ok(new { empleado.Id, mensaje = "Empleado registrado." });
        }

        /// <summary>
        /// PUT /api/empleados/{id}
        /// Editar los datos de un empleado
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(Guid id, [FromBody] CrearEmpleadoDto dto)
        {
            var clientId = ObtenerClientId();
            var empleado = _context.Empleados.FirstOrDefault(e => e.Id == id && e.ClientId == clientId);

            if (empleado == null)
                return NotFound("Empleado no encontrado o no autorizado.");

            empleado.Nombre = dto.Nombre;
            empleado.Correo = dto.Correo;
            empleado.Telefono = dto.Telefono;
            empleado.Cargo = dto.Cargo;
            empleado.AvatarUrl = dto.AvatarUrl;
            empleado.FechaIngreso = dto.FechaIngreso;

            await _context.SaveChangesAsync();
            return Ok(new { mensaje = "Empleado actualizado." });
        }

        /// <summary>
        /// DELETE /api/empleados/{id}
        /// Desactivar (eliminar lógico) un empleado del cliente autenticado
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Desactivar(Guid id)
        {
            var clientId = ObtenerClientId();
            var empleado = _context.Empleados.FirstOrDefault(e => e.Id == id && e.ClientId == clientId);
            if (empleado == null)
                return NotFound("Empleado no encontrado o no autorizado.");

            empleado.Activo = false;
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Empleado desactivado." });
        }
    }
}
