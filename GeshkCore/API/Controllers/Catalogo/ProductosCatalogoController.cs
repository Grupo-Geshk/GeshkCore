using Microsoft.AspNetCore.Mvc;
using GeshkCore.API.DTOs.Catalogo;
using GeshkCore.Infrastructure.DbContext;
using GeshkCore.Domain.Entities.Catalogo;
using GeshkCore.API.Controllers;

namespace GeshkCore.API.Controllers.Catalogo
{
    [ApiController]
    [Route("api/catalogo/productos")]
    public class ProductosCatalogoController : ClienteControllerBase
    {
        private readonly GeshkDbContext _context;

        public ProductosCatalogoController(GeshkDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET /api/catalogo/productos
        /// Listar productos visibles del catálogo del cliente autenticado
        /// </summary>
        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var clientId = ObtenerClientId();

            var productos = _context.ProductosCatalogo
                .Where(p => p.ClientId == clientId && p.Visible)
                .Select(p => new ProductoCatalogoDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Precio = p.Precio,
                    Nota = p.Nota,
                    Destacado = p.Destacado,
                    ProductoId = p.ProductoId,
                    Visible = p.Visible
                })
                .ToList();

            return Ok(productos);
        }

        /// <summary>
        /// GET /api/catalogo/productos/{id}
        /// Obtener un producto del catálogo por ID
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(Guid id)
        {
            var clientId = ObtenerClientId();
            var producto = _context.ProductosCatalogo
                .FirstOrDefault(p => p.Id == id && p.ClientId == clientId);

            if (producto == null)
                return NotFound("Producto no encontrado o no autorizado.");

            var dto = new ProductoCatalogoDto
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Precio = producto.Precio,
                Nota = producto.Nota,
                Destacado = producto.Destacado,
                ProductoId = producto.ProductoId,
                Visible = producto.Visible
            };

            return Ok(dto);
        }

        /// <summary>
        /// POST /api/catalogo/productos
        /// Crear un nuevo producto en el catálogo del cliente autenticado
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearProductoCatalogoDto dto)
        {
            var producto = new ProductoCatalogo
            {
                Id = Guid.NewGuid(),
                ClientId = ObtenerClientId(),
                Nombre = dto.Nombre,
                Precio = dto.Precio,
                Nota = dto.Nota,
                Destacado = dto.Destacado,
                ProductoId = dto.ProductoId,
                Visible = dto.Visible
            };

            await _context.ProductosCatalogo.AddAsync(producto);
            await _context.SaveChangesAsync();

            return Ok(new { producto.Id, mensaje = "Producto agregado al catálogo." });
        }

        /// <summary>
        /// PUT /api/catalogo/productos/{id}
        /// Editar un producto del catálogo
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(Guid id, [FromBody] CrearProductoCatalogoDto dto)
        {
            var clientId = ObtenerClientId();
            var producto = _context.ProductosCatalogo.FirstOrDefault(p => p.Id == id && p.ClientId == clientId);
            if (producto == null)
                return NotFound("Producto no encontrado o no autorizado.");

            producto.Nombre = dto.Nombre;
            producto.Precio = dto.Precio;
            producto.Nota = dto.Nota;
            producto.Destacado = dto.Destacado;
            producto.ProductoId = dto.ProductoId;
            producto.Visible = dto.Visible;

            await _context.SaveChangesAsync();
            return Ok(new { mensaje = "Producto actualizado." });
        }

        /// <summary>
        /// DELETE /api/catalogo/productos/{id}
        /// Eliminar un producto del catálogo
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var clientId = ObtenerClientId();
            var producto = _context.ProductosCatalogo.FirstOrDefault(p => p.Id == id && p.ClientId == clientId);
            if (producto == null)
                return NotFound("Producto no encontrado o no autorizado.");

            _context.ProductosCatalogo.Remove(producto);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Producto eliminado del catálogo." });
        }
    }
}
