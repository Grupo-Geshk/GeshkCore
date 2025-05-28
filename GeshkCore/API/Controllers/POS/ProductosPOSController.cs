using Microsoft.AspNetCore.Mvc;
using GeshkCore.API.DTOs.POS;
using GeshkCore.Infrastructure.DbContext;
using GeshkCore.Domain.Entities.POS;

namespace GeshkCore.API.Controllers.POS
{
    [ApiController]
    [Route("api/productos-pos")]
    public class ProductosPOSController : ControllerBase
    {
        // ========================
        // ⚙️ CONFIGURACIÓN INICIAL
        // ========================

        private readonly GeshkDbContext _context;

        public ProductosPOSController(GeshkDbContext context)
        {
            _context = context;
        }

        // ============================
        // 📦 ENDPOINTS DE PRODUCTOS POS
        // ============================

        /// GET /api/productos-pos
        /// Listar todos los productos POS
        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var productos = _context.ProductosPOS
                .Select(p => new ProductoPOSDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Precio = p.Precio
                })
                .ToList();

            return Ok(productos);
        }

        /// GET /api/productos-pos/{id}
        /// Obtener un producto POS por ID
        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(Guid id)
        {
            var producto = _context.ProductosPOS.Find(id);
            if (producto == null)
                return NotFound("Producto no encontrado.");

            return Ok(new ProductoPOSDto
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Precio = producto.Precio
            });
        }

        /// POST /api/productos-pos
        /// Crear un nuevo producto POS
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearProductoPOSDto dto)
        {
            var producto = new ProductoPOS
            {
                ClientId = Guid.Empty, // TODO: Obtener desde token o contexto
                Nombre = dto.Nombre,
                Precio = dto.Precio
            };

            await _context.ProductosPOS.AddAsync(producto);
            await _context.SaveChangesAsync();

            return Ok(new { producto.Id, mensaje = "Producto creado." });
        }

        /// PUT /api/productos-pos/{id}
        /// Actualizar un producto POS existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(Guid id, [FromBody] CrearProductoPOSDto dto)
        {
            var producto = await _context.ProductosPOS.FindAsync(id);
            if (producto == null)
                return NotFound("Producto no encontrado.");

            producto.Nombre = dto.Nombre;
            producto.Precio = dto.Precio;
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Producto actualizado." });
        }

        /// DELETE /api/productos-pos/{id}
        /// Eliminar un producto POS por ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var producto = await _context.ProductosPOS.FindAsync(id);
            if (producto == null)
                return NotFound("Producto no encontrado.");

            _context.ProductosPOS.Remove(producto);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Producto eliminado." });
        }
    }
}
