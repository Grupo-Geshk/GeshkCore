using Microsoft.AspNetCore.Mvc;
using GeshkCore.API.DTOs.POS;
using GeshkCore.Infrastructure.DbContext;
using GeshkCore.Domain.Entities.POS;

namespace GeshkCore.API.Controllers.POS
{
    [ApiController]
    [Route("api/cierres-caja")]
    public class CierreCajaController : ControllerBase
    {
        // ============================
        // ⚙️ CONFIGURACIÓN INICIAL
        // ============================

        private readonly GeshkDbContext _context;

        public CierreCajaController(GeshkDbContext context)
        {
            _context = context;
        }

        // ============================
        // 📦 ENDPOINTS DE CIERRE DE CAJA
        // ============================

        /// GET /api/cierres-caja
        /// Listar todos los cierres de caja
        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var cierres = _context.CierresCaja
                .OrderByDescending(c => c.Fecha)
                .Select(c => new CierreCajaDto
                {
                    Id = c.Id,
                    Fecha = c.Fecha,
                    Total = c.Total,
                    ArchivoExcelUrl = c.ArchivoExcelUrl
                })
                .ToList();

            return Ok(cierres);
        }

        /// GET /api/cierres-caja/{id}
        /// Obtener un cierre de caja por ID
        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(Guid id)
        {
            var cierre = _context.CierresCaja.Find(id);
            if (cierre == null)
                return NotFound("Cierre no encontrado.");

            return Ok(new CierreCajaDto
            {
                Id = cierre.Id,
                Fecha = cierre.Fecha,
                Total = cierre.Total,
                ArchivoExcelUrl = cierre.ArchivoExcelUrl
            });
        }

        /// POST /api/cierres-caja
        /// Registrar un nuevo cierre de caja
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearCierreCajaDto dto)
        {
            var cierre = new CierreCaja
            {
                Id = Guid.NewGuid(),
                ClientId = Guid.Empty, // TODO: Obtener desde JWT o contexto
                Fecha = DateTime.UtcNow,
                Total = dto.Total,
                ArchivoExcelUrl = dto.ArchivoExcelUrl
            };

            await _context.CierresCaja.AddAsync(cierre);
            await _context.SaveChangesAsync();

            return Ok(new { cierre.Id, mensaje = "Cierre de caja registrado." });
        }
    }
}