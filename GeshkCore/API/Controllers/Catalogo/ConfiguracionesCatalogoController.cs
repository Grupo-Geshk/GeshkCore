using Microsoft.AspNetCore.Mvc;
using GeshkCore.API.DTOs.Catalogo;
using GeshkCore.Infrastructure.DbContext;
using GeshkCore.Domain.Entities.Catalogo;
using GeshkCore.API.Controllers;

namespace GeshkCore.API.Controllers.Catalogo
{
    [ApiController]
    [Route("api/catalogo/configuracion")]
    public class ConfiguracionesCatalogoController : ClienteControllerBase
    {
        private readonly GeshkDbContext _context;

        public ConfiguracionesCatalogoController(GeshkDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET /api/catalogo/configuracion
        /// Obtener la configuración visual del catálogo del cliente autenticado
        /// </summary>
        [HttpGet]
        public IActionResult Obtener()
        {
            var clientId = ObtenerClientId();

            var config = _context.ConfiguracionesCatalogo.FirstOrDefault(c => c.ClientId == clientId);
            if (config == null)
                return Ok(null);

            var dto = new ConfiguracionCatalogoDto
            {
                Id = config.Id,
                LogoClienteUrl = config.LogoClienteUrl,
                ColorAcento = config.ColorAcento,
                MensajeBienvenida = config.MensajeBienvenida,
                WhatsAppContacto = config.WhatsAppContacto,
                PlantillaPublicacion = config.PlantillaPublicacion
            };

            return Ok(dto);
        }

        /// <summary>
        /// POST /api/catalogo/configuracion
        /// Crear o actualizar configuración del catálogo del cliente
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CrearOActualizar([FromBody] ConfiguracionCatalogoDto dto)
        {
            var clientId = ObtenerClientId();
            var config = _context.ConfiguracionesCatalogo.FirstOrDefault(c => c.ClientId == clientId);

            if (config == null)
            {
                var nueva = new ConfiguracionCatalogo
                {
                    Id = Guid.NewGuid(),
                    ClientId = clientId,
                    LogoClienteUrl = dto.LogoClienteUrl,
                    ColorAcento = dto.ColorAcento,
                    MensajeBienvenida = dto.MensajeBienvenida,
                    WhatsAppContacto = dto.WhatsAppContacto,
                    PlantillaPublicacion = dto.PlantillaPublicacion
                };

                await _context.ConfiguracionesCatalogo.AddAsync(nueva);
            }
            else
            {
                config.LogoClienteUrl = dto.LogoClienteUrl;
                config.ColorAcento = dto.ColorAcento;
                config.MensajeBienvenida = dto.MensajeBienvenida;
                config.WhatsAppContacto = dto.WhatsAppContacto;
                config.PlantillaPublicacion = dto.PlantillaPublicacion;
            }

            await _context.SaveChangesAsync();
            return Ok(new { mensaje = "Configuración guardada." });
        }
    }
}
