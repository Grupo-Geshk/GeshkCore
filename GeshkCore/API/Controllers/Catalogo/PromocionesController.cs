using Microsoft.AspNetCore.Mvc;
using GeshkCore.API.DTOs.Catalogo;
using GeshkCore.Infrastructure.DbContext;
using GeshkCore.Domain.Entities.Catalogo;
using GeshkCore.API.Controllers;

namespace GeshkCore.API.Controllers.Catalogo
{
    [ApiController]
    [Route("api/catalogo/promociones")]
    public class PromocionesController : ClienteControllerBase
    {
        private readonly GeshkDbContext _context;

        public PromocionesController(GeshkDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET /api/catalogo/promociones
        /// Lista todas las promociones activas del catálogo del cliente
        /// </summary>
        [HttpGet]
        public IActionResult ObtenerTodas()
        {
            var clientId = ObtenerClientId();

            var promociones = _context.Promociones
                .Where(p => _context.ProductosCatalogo.Any(pc => pc.Id == p.ProductoCatalogoId && pc.ClientId == clientId))
                .Select(p => new PromocionDto
                {
                    Id = p.Id,
                    ProductoCatalogoId = p.ProductoCatalogoId,
                    Titulo = p.Titulo,
                    DescripcionCorta = p.DescripcionCorta,
                    Activa = p.Activa,
                    FechaPublicacion = p.FechaPublicacion
                })
                .ToList();

            return Ok(promociones);
        }

        /// <summary>
        /// POST /api/catalogo/promociones
        /// Crear una nueva promoción para un producto del catálogo
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearPromocionDto dto)
        {
            var clientId = ObtenerClientId();
            var producto = _context.ProductosCatalogo.FirstOrDefault(p => p.Id == dto.ProductoCatalogoId && p.ClientId == clientId);
            if (producto == null)
                return NotFound("Producto del catálogo no encontrado o no autorizado.");

            var promo = new Promocion
            {
                Id = Guid.NewGuid(),
                ProductoCatalogoId = dto.ProductoCatalogoId,
                Titulo = dto.Titulo,
                DescripcionCorta = dto.DescripcionCorta,
                Activa = true,
                FechaPublicacion = DateTime.UtcNow
            };

            await _context.Promociones.AddAsync(promo);
            await _context.SaveChangesAsync();

            return Ok(new { promo.Id, mensaje = "Promoción registrada." });
        }

        /// <summary>
        /// DELETE /api/catalogo/promociones/{id}
        /// Eliminar una promoción
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var clientId = ObtenerClientId();
            var promo = _context.Promociones
                .FirstOrDefault(p => p.Id == id && _context.ProductosCatalogo.Any(pc => pc.Id == p.ProductoCatalogoId && pc.ClientId == clientId));

            if (promo == null)
                return NotFound("Promoción no encontrada o no autorizada.");

            _context.Promociones.Remove(promo);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Promoción eliminada." });
        }
    }
}
