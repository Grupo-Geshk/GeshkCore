using Microsoft.AspNetCore.Mvc;
using GeshkCore.API.DTOs.Inventario;
using GeshkCore.Infrastructure.DbContext;
using GeshkCore.Domain.Entities.Inventario;

namespace GeshkCore.API.Controllers.Inventario
{
    [ApiController]
    [Route("api/entradas")]
    public class EntradasInventarioController : ControllerBase
    {
        // ============================
        // ⚙️ CONFIGURACIÓN INICIAL
        // ============================

        private readonly GeshkDbContext _context;

        public EntradasInventarioController(GeshkDbContext context)
        {
            _context = context;
        }

        // ============================
        // 📥 ENDPOINTS DE ENTRADAS A INVENTARIO
        // ============================

        /// <summary>
        /// POST /api/entradas
        /// Registrar una nueva entrada de inventario
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearEntradaInventarioDto dto)
        {
            var entrada = new EntradaInventario
            {
                Id = Guid.NewGuid(),
                ProductoId = dto.ProductoId,
                ClientId = Guid.Empty, // TODO: obtener desde JWT
                Cantidad = dto.Cantidad,
                Tipo = dto.Tipo,
                Usuario = dto.Usuario,
                Fecha = DateTime.UtcNow
            };

            var producto = await _context.Productos.FindAsync(dto.ProductoId);
            if (producto == null)
                return NotFound("Producto no encontrado.");

            producto.Stock += dto.Cantidad;

            await _context.EntradasInventario.AddAsync(entrada);
            await _context.SaveChangesAsync();

            return Ok(new { entrada.Id, mensaje = "Entrada registrada." });
        }

        /// <summary>
        /// GET /api/entradas
        /// Obtener el historial de entradas de inventario
        /// </summary>
        [HttpGet]
        public IActionResult ObtenerHistorial()
        {
            var entradas = _context.EntradasInventario
                .OrderByDescending(e => e.Fecha)
                .Select(e => new EntradaInventarioDto
                {
                    Id = e.Id,
                    ProductoId = e.ProductoId,
                    Cantidad = e.Cantidad,
                    Tipo = e.Tipo,
                    Usuario = e.Usuario,
                    Fecha = e.Fecha
                }).ToList();

            return Ok(entradas);
        }
    }
}
