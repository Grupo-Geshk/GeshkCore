using Microsoft.AspNetCore.Mvc;
using GeshkCore.API.DTOs.Encuestas;
using GeshkCore.Infrastructure.DbContext;
using GeshkCore.Domain.Entities.Encuestas;
using GeshkCore.API.Controllers;

namespace GeshkCore.API.Controllers.Encuestas
{
    [ApiController]
    [Route("api/encuestas")]
    public class EncuestasController : ClienteControllerBase
    {
        private readonly GeshkDbContext _context;

        public EncuestasController(GeshkDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET /api/encuestas
        /// Listar todas las encuestas del cliente autenticado
        /// </summary>
        [HttpGet]
        public IActionResult ObtenerTodas()
        {
            var clientId = ObtenerClientId();

            var encuestas = _context.Encuestas
                .Where(e => e.ClientId == clientId)
                .Select(e => new EncuestaDto
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    Tipo = e.Tipo,
                    Activa = e.Activa,
                    BrandingJson = e.BrandingJson
                })
                .ToList();

            return Ok(encuestas);
        }

        /// <summary>
        /// GET /api/encuestas/{id}
        /// Obtener detalles de una encuesta por ID
        /// </summary>
        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(Guid id)
        {
            var clientId = ObtenerClientId();

            var encuesta = _context.Encuestas
                .FirstOrDefault(e => e.Id == id && e.ClientId == clientId);

            if (encuesta == null)
                return NotFound("Encuesta no encontrada o no autorizada.");

            var dto = new EncuestaDto
            {
                Id = encuesta.Id,
                Nombre = encuesta.Nombre,
                Tipo = encuesta.Tipo,
                Activa = encuesta.Activa,
                BrandingJson = encuesta.BrandingJson
            };

            return Ok(dto);
        }

        /// <summary>
        /// POST /api/encuestas
        /// Crear una nueva encuesta para el cliente autenticado
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearEncuestaDto dto)
        {
            var encuesta = new Encuesta
            {
                Id = Guid.NewGuid(),
                ClientId = ObtenerClientId(),
                Nombre = dto.Nombre,
                Tipo = dto.Tipo,
                Activa = dto.Activa,
                BrandingJson = dto.BrandingJson
            };

            await _context.Encuestas.AddAsync(encuesta);
            await _context.SaveChangesAsync();

            return Ok(new { encuesta.Id, mensaje = "Encuesta creada." });
        }

        /// <summary>
        /// PUT /api/encuestas/{id}
        /// Editar encuesta del cliente autenticado
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(Guid id, [FromBody] CrearEncuestaDto dto)
        {
            var clientId = ObtenerClientId();

            var encuesta = _context.Encuestas.FirstOrDefault(e => e.Id == id && e.ClientId == clientId);
            if (encuesta == null)
                return NotFound("Encuesta no encontrada o no autorizada.");

            encuesta.Nombre = dto.Nombre;
            encuesta.Tipo = dto.Tipo;
            encuesta.Activa = dto.Activa;
            encuesta.BrandingJson = dto.BrandingJson;

            await _context.SaveChangesAsync();
            return Ok(new { mensaje = "Encuesta actualizada." });
        }
    }
}
