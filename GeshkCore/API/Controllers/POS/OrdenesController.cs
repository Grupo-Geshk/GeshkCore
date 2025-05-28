using Microsoft.AspNetCore.Mvc;
using GeshkCore.API.DTOs.POS;
using GeshkCore.Infrastructure.DbContext;
using GeshkCore.Domain.Entities.POS;

namespace GeshkCore.API.Controllers.POS
{
    [ApiController]
    [Route("api/ordenes")]
    public class OrdenesController : ControllerBase
    {
        // ========================
        // ⚙️ CONFIGURACIÓN INICIAL
        // ========================

        private readonly GeshkDbContext _context;

        public OrdenesController(GeshkDbContext context)
        {
            _context = context;
        }

        // ========================
        // 📦 ENDPOINTS DE ORDENES
        // ========================

        /// <summary>
        /// POST /api/ordenes
        /// Crear una nueva orden con items, métodos de pago y comprobantes
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CrearOrden([FromBody] CrearOrdenDto dto)
        {
            if (dto.Items == null || !dto.Items.Any())
                return BadRequest("La orden debe tener al menos un producto.");

            var orden = new Orden
            {
                ClientId = Guid.Empty, // TODO: Obtener desde token o contexto
                Fecha = DateTime.UtcNow,
                Estado = "abierta",
                Total = dto.Total,
                Items = dto.Items.Select(i => new ItemOrden
                {
                    ProductoNombre = i.ProductoNombre,
                    Cantidad = i.Cantidad,
                    PrecioUnitario = i.PrecioUnitario
                }).ToList(),
                MetodosPago = dto.MetodosPago.Select(m => new MetodoPago
                {
                    Tipo = m.Tipo,
                    Referencia = m.Referencia
                }).ToList(),
                Comprobantes = dto.ComprobantesPago?.Select(c => new ComprobantePago
                {
                    UrlArchivo = c.UrlArchivo,
                    Fecha = DateTime.UtcNow
                }).ToList()
            };

            await _context.Ordenes.AddAsync(orden);
            await _context.SaveChangesAsync();

            return Ok(new { orden.Id, mensaje = "Orden creada exitosamente." });
        }

        /// <summary>
        /// GET /api/ordenes
        /// Obtener todas las órdenes registradas
        /// </summary>
        [HttpGet]
        public IActionResult ObtenerTodas()
        {
            var ordenes = _context.Ordenes
                .Select(o => new OrdenDto
                {
                    Id = o.Id,
                    Fecha = o.Fecha,
                    Estado = o.Estado,
                    Total = o.Total,
                    Items = o.Items.Select(i => new ItemOrdenDto
                    {
                        ProductoNombre = i.ProductoNombre,
                        Cantidad = i.Cantidad,
                        PrecioUnitario = i.PrecioUnitario
                    }).ToList(),
                    MetodosPago = o.MetodosPago.Select(m => new MetodoPagoDto
                    {
                        Tipo = m.Tipo,
                        Referencia = m.Referencia
                    }).ToList(),
                    ComprobantesPago = o.Comprobantes.Select(c => new ComprobantePagoDto
                    {
                        UrlArchivo = c.UrlArchivo,
                        Fecha = c.Fecha
                    }).ToList()
                })
                .ToList();

            return Ok(ordenes);
        }

        /// <summary>
        /// GET /api/ordenes/{id}
        /// Obtener una orden por ID
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(Guid id)
        {
            var orden = _context.Ordenes.FirstOrDefault(o => o.Id == id);

            if (orden == null)
                return NotFound("Orden no encontrada.");

            var dto = new OrdenDto
            {
                Id = orden.Id,
                Fecha = orden.Fecha,
                Estado = orden.Estado,
                Total = orden.Total,
                Items = orden.Items.Select(i => new ItemOrdenDto
                {
                    ProductoNombre = i.ProductoNombre,
                    Cantidad = i.Cantidad,
                    PrecioUnitario = i.PrecioUnitario
                }).ToList(),
                MetodosPago = orden.MetodosPago.Select(m => new MetodoPagoDto
                {
                    Tipo = m.Tipo,
                    Referencia = m.Referencia
                }).ToList(),
                ComprobantesPago = orden.Comprobantes.Select(c => new ComprobantePagoDto
                {
                    UrlArchivo = c.UrlArchivo,
                    Fecha = c.Fecha
                }).ToList()
            };

            return Ok(dto);
        }
    }
}
