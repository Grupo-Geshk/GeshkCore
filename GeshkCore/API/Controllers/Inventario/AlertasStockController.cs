using Microsoft.AspNetCore.Mvc;
using GeshkCore.API.DTOs.Inventario;
using GeshkCore.Infrastructure.DbContext;
using GeshkCore.Domain.Entities.Inventario;

namespace GeshkCore.API.Controllers.Inventario
{
    [ApiController]
    [Route("api/alertas-stock")]
    public class AlertasStockController : ControllerBase
    {
        // ============================
        // ⚙️ CONFIGURACIÓN INICIAL
        // ============================

        private readonly GeshkDbContext _context;

        public AlertasStockController(GeshkDbContext context)
        {
            _context = context;
        }

        // ============================
        // 🚨 ENDPOINTS DE ALERTAS DE STOCK
        // ============================

        /// <summary>
        /// GET /api/alertas-stock
        /// Obtener todos los productos con stock bajo el mínimo configurado
        /// </summary>
        [HttpGet]
        public IActionResult ObtenerAlertas()
        {
            var alertas = _context.Productos
                .Where(p => p.Stock <= p.StockMinimo)
                .Select(p => new AlertaStockDto
                {
                    ProductoId = p.Id,
                    Nombre = p.Nombre,
                    Stock = p.Stock,
                    StockMinimo = p.StockMinimo,
                    Notificado = _context.AlertasStock.Any(a => a.ProductoId == p.Id && a.Notificado)
                })
                .ToList();

            return Ok(alertas);
        }
    }
}
