using Microsoft.AspNetCore.Mvc;
using GeshkCore.API.DTOs.Inventario;
using GeshkCore.Infrastructure.DbContext;
using GeshkCore.Domain.Entities.Inventario;

namespace GeshkCore.API.Controllers.Inventario
{
    [ApiController]
    [Route("api/salidas")]
    public class SalidasInventarioController : ControllerBase
    {
        // ============================
        // ⚙️ CONFIGURACIÓN INICIAL
        // ============================

        private readonly GeshkDbContext _context;

        public SalidasInventarioController(GeshkDbContext context)
        {
            _context = context;
        }

        // ============================
        // 📤 ENDPOINTS DE SALIDAS DE INVENTARIO
        // ============================

        /// <summary>
        /// POST /api/salidas
        /// Registrar una salida de inventario
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearSalidaInventarioDto dto)
        {
            var producto = await _context.Productos.FindAsync(dto.ProductoId);
            if (producto == null)
                return NotFound("Producto no encontrado.");

            if (producto.Stock < dto.Cantidad)
                return BadRequest("Stock insuficiente para realizar la salida.");

            var salida = new SalidaInventario
            {
                Id = Guid.NewGuid(),
                ProductoId = dto.ProductoId,
                ClientId = Guid.Empty, // TODO: obtener desde JWT
                Cantidad = dto.Cantidad,
                Tipo = dto.Tipo,
                ModuloOrigen = dto.ModuloOrigen,
                Fecha = DateTime.UtcNow
            };

            producto.Stock -= dto.Cantidad;

            await _context.SalidasInventario.AddAsync(salida);
            await _context.SaveChangesAsync();

            return Ok(new { salida.Id, mensaje = "Salida registrada." });
        }

        /// <summary>
        /// GET /api/salidas
        /// Obtener historial de salidas de inventario
        /// </summary>
        [HttpGet]
        public IActionResult ObtenerHistorial()
        {
            var salidas = _context.SalidasInventario
                .OrderByDescending(s => s.Fecha)
                .Select(s => new SalidaInventarioDto
                {
                    Id = s.Id,
                    ProductoId = s.ProductoId,
                    Cantidad = s.Cantidad,
                    Tipo = s.Tipo,
                    ModuloOrigen = s.ModuloOrigen,
                    Fecha = s.Fecha
                }).ToList();

            return Ok(salidas);
        }
    }
}
