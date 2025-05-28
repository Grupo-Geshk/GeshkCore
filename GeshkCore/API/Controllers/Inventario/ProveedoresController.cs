using Microsoft.AspNetCore.Mvc;
using GeshkCore.API.DTOs.Inventario;
using GeshkCore.Infrastructure.DbContext;
using GeshkCore.Domain.Entities.Inventario;

namespace GeshkCore.API.Controllers.Inventario
{
    [ApiController]
    [Route("api/proveedores")]
    public class ProveedoresController : ControllerBase
    {
        // ============================
        // ⚙️ CONFIGURACIÓN INICIAL
        // ============================

        private readonly GeshkDbContext _context;

        public ProveedoresController(GeshkDbContext context)
        {
            _context = context;
        }

        // ============================
        // 📇 ENDPOINTS DE PROVEEDORES
        // ============================

        /// <summary>
        /// GET /api/proveedores
        /// Obtener todos los proveedores registrados
        /// </summary>
        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var proveedores = _context.Proveedores
                .Select(p => new ProveedorDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Telefono = p.Telefono,
                    Correo = p.Correo,
                    Direccion = p.Direccion
                }).ToList();

            return Ok(proveedores);
        }

        /// <summary>
        /// POST /api/proveedores
        /// Crear un nuevo proveedor
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearProveedorDto dto)
        {
            var proveedor = new Proveedor
            {
                Id = Guid.NewGuid(),
                ClientId = Guid.Empty, // TODO: obtener desde JWT
                Nombre = dto.Nombre,
                Telefono = dto.Telefono,
                Correo = dto.Correo,
                Direccion = dto.Direccion
            };

            await _context.Proveedores.AddAsync(proveedor);
            await _context.SaveChangesAsync();

            return Ok(new { proveedor.Id, mensaje = "Proveedor creado." });
        }

        /// <summary>
        /// PUT /api/proveedores/{id}
        /// Editar los datos de un proveedor
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(Guid id, [FromBody] CrearProveedorDto dto)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null)
                return NotFound("Proveedor no encontrado.");

            proveedor.Nombre = dto.Nombre;
            proveedor.Telefono = dto.Telefono;
            proveedor.Correo = dto.Correo;
            proveedor.Direccion = dto.Direccion;

            await _context.SaveChangesAsync();
            return Ok(new { mensaje = "Proveedor actualizado." });
        }

        /// <summary>
        /// DELETE /api/proveedores/{id}
        /// Eliminar un proveedor si no está vinculado a productos
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null)
                return NotFound("Proveedor no encontrado.");

            var productosVinculados = _context.Productos.Any(p => p.ProveedorId == id);
            if (productosVinculados)
                return BadRequest("No se puede eliminar un proveedor con productos vinculados.");

            _context.Proveedores.Remove(proveedor);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Proveedor eliminado." });
        }
    }
}
