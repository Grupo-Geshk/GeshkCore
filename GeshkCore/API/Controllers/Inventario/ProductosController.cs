using Microsoft.AspNetCore.Mvc;
using GeshkCore.API.DTOs.Inventario;
using GeshkCore.Infrastructure.DbContext;
using GeshkCore.Domain.Entities.Inventario;

namespace GeshkCore.API.Controllers.Inventario
{
    [ApiController]
    [Route("api/productos")]
    public class ProductosController : ControllerBase
    {
        // ============================
        // ⚙️ CONFIGURACIÓN INICIAL
        // ============================

        private readonly GeshkDbContext _context;

        public ProductosController(GeshkDbContext context)
        {
            _context = context;
        }

        // ============================
        // 📦 ENDPOINTS DE PRODUCTOS CON STOCK
        // ============================

        /// GET /api/productos
        /// Listar todos los productos con inventario
        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var productos = _context.Productos
                .Select(p => new ProductoDto
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    PrecioVenta = p.PrecioVenta,
                    Costo = p.Costo,
                    Stock = p.Stock,
                    StockMinimo = p.StockMinimo,
                    Visible = p.Visible,
                    ProveedorId = p.ProveedorId
                }).ToList();

            return Ok(productos);
        }

        /// <summary>
        /// GET /api/productos/{id}
        /// Obtener un producto específico por ID
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(Guid id)
        {
            var producto = _context.Productos.Find(id);
            if (producto == null)
                return NotFound("Producto no encontrado.");

            var dto = new ProductoDto
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                PrecioVenta = producto.PrecioVenta,
                Costo = producto.Costo,
                Stock = producto.Stock,
                StockMinimo = producto.StockMinimo,
                Visible = producto.Visible,
                ProveedorId = producto.ProveedorId
            };

            return Ok(dto);
        }

        /// <summary>
        /// POST /api/productos
        /// Crear un nuevo producto con inventario
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearProductoDto dto)
        {
            var producto = new Producto
            {
                Id = Guid.NewGuid(),
                ClientId = Guid.Empty, // TODO: obtener desde JWT
                Nombre = dto.Nombre,
                PrecioVenta = dto.PrecioVenta,
                Costo = dto.Costo,
                Stock = dto.Stock,
                StockMinimo = dto.StockMinimo,
                Visible = dto.Visible,
                ProveedorId = dto.ProveedorId
            };

            await _context.Productos.AddAsync(producto);
            await _context.SaveChangesAsync();

            return Ok(new { producto.Id, mensaje = "Producto creado." });
        }

        /// <summary>
        /// PUT /api/productos/{id}
        /// Editar los datos de un producto existente
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(Guid id, [FromBody] CrearProductoDto dto)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
                return NotFound("Producto no encontrado.");

            producto.Nombre = dto.Nombre;
            producto.PrecioVenta = dto.PrecioVenta;
            producto.Costo = dto.Costo;
            producto.Stock = dto.Stock;
            producto.StockMinimo = dto.StockMinimo;
            producto.Visible = dto.Visible;
            producto.ProveedorId = dto.ProveedorId;

            await _context.SaveChangesAsync();
            return Ok(new { mensaje = "Producto actualizado." });
        }

        /// <summary>
        /// DELETE /api/productos/{id}
        /// Eliminar un producto (solo si no tiene movimientos registrados)
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
                return NotFound("Producto no encontrado.");

            // TODO: Verificar si tiene entradas o salidas antes de eliminar
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Producto eliminado." });
        }
    }
}
