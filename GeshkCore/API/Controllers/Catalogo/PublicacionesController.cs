using Microsoft.AspNetCore.Mvc;
using GeshkCore.API.DTOs.Catalogo;
using GeshkCore.Infrastructure.DbContext;
using GeshkCore.Domain.Entities.Catalogo;
using GeshkCore.API.Controllers;

namespace GeshkCore.API.Controllers.Catalogo
{
    [ApiController]
    [Route("api/catalogo/publicaciones")]
    public class PublicacionesController : ClienteControllerBase
    {
        private readonly GeshkDbContext _context;

        public PublicacionesController(GeshkDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET /api/catalogo/publicaciones
        /// Listar todas las publicaciones del cliente autenticado
        /// </summary>
        [HttpGet]
        public IActionResult ObtenerTodas()
        {
            var clientId = ObtenerClientId();

            var publicaciones = _context.Publicaciones
                .Where(p => _context.ProductosCatalogo.Any(pc => pc.Id == p.ProductoCatalogoId && pc.ClientId == clientId))
                .Select(p => new PublicacionDto
                {
                    Id = p.Id,
                    ProductoCatalogoId = p.ProductoCatalogoId,
                    Fondo = p.Fondo,
                    TextoExtra = p.TextoExtra,
                    UrlImagenGenerada = p.UrlImagenGenerada,
                    FechaCreacion = p.FechaCreacion
                })
                .ToList();

            return Ok(publicaciones);
        }

        /// <summary>
        /// POST /api/catalogo/publicaciones
        /// Crear una nueva publicación para un producto del catálogo
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearPublicacionDto dto)
        {
            var clientId = ObtenerClientId();
            var producto = _context.ProductosCatalogo.FirstOrDefault(p => p.Id == dto.ProductoCatalogoId && p.ClientId == clientId);
            if (producto == null)
                return NotFound("Producto del catálogo no encontrado o no autorizado.");

            var publicacion = new Publicacion
            {
                Id = Guid.NewGuid(),
                ProductoCatalogoId = dto.ProductoCatalogoId,
                Fondo = dto.Fondo,
                TextoExtra = dto.TextoExtra,
                UrlImagenGenerada = dto.UrlImagenGenerada,
                FechaCreacion = DateTime.UtcNow
            };

            await _context.Publicaciones.AddAsync(publicacion);
            await _context.SaveChangesAsync();

            return Ok(new { publicacion.Id, mensaje = "Publicación creada." });
        }

        /// <summary>
        /// DELETE /api/catalogo/publicaciones/{id}
        /// Eliminar una publicación del catálogo
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var clientId = ObtenerClientId();
            var publicacion = _context.Publicaciones
                .FirstOrDefault(p => p.Id == id && _context.ProductosCatalogo.Any(pc => pc.Id == p.ProductoCatalogoId && pc.ClientId == clientId));

            if (publicacion == null)
                return NotFound("Publicación no encontrada o no autorizada.");

            _context.Publicaciones.Remove(publicacion);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Publicación eliminada." });
        }
    }
}
