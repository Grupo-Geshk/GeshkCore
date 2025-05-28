using Microsoft.AspNetCore.Mvc;
using GeshkCore.API.DTOs.Turnos;
using GeshkCore.Infrastructure.DbContext;
using GeshkCore.Domain.Entities.Turnos;
using System.Text.Json;
using GeshkCore.API.Controllers;

namespace GeshkCore.API.Controllers.Turnos
{
    [ApiController]
    [Route("api/configuraciones-agenda")]
    public class ConfiguracionesAgendaController : ClienteControllerBase
    {
        private readonly GeshkDbContext _context;

        public ConfiguracionesAgendaController(GeshkDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET /api/configuraciones-agenda/{formularioId}
        /// Obtener configuración de agenda de un formulario del cliente autenticado
        /// </summary>
        [HttpGet("{formularioId}")]
        public IActionResult ObtenerPorFormulario(Guid formularioId)
        {
            var clientId = ObtenerClientId();
            var formulario = _context.Formularios.FirstOrDefault(f => f.Id == formularioId && f.ClientId == clientId);
            if (formulario == null)
                return NotFound("Formulario no encontrado o no autorizado.");

            var config = _context.ConfiguracionesAgenda.FirstOrDefault(c => c.FormularioId == formularioId);
            if (config == null)
                return NotFound("Configuración no encontrada.");

            var dto = new ConfiguracionAgendaDto
            {
                DiasActivos = JsonSerializer.Deserialize<List<int>>(config.DiasActivosJson)!,
                Excepciones = JsonSerializer.Deserialize<List<string>>(config.ExcepcionesJson)!,
                HorarioInicio = config.HorarioInicio,
                HorarioFin = config.HorarioFin,
                IntervaloMinutos = config.IntervaloMinutos
            };

            return Ok(dto);
        }

        /// <summary>
        /// POST /api/configuraciones-agenda
        /// Crear o actualizar configuración de agenda del formulario del cliente autenticado
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CrearOActualizar([FromBody] CrearConfiguracionAgendaDto dto)
        {
            var clientId = ObtenerClientId();
            var formulario = _context.Formularios.FirstOrDefault(f => f.Id == dto.FormularioId && f.ClientId == clientId);
            if (formulario == null)
                return NotFound("Formulario no encontrado o no autorizado.");

            var existente = _context.ConfiguracionesAgenda.FirstOrDefault(c => c.FormularioId == dto.FormularioId);

            var diasJson = JsonSerializer.Serialize(dto.DiasActivos);
            var excepcionesJson = JsonSerializer.Serialize(dto.Excepciones);

            if (existente == null)
            {
                var nueva = new ConfiguracionAgenda
                {
                    Id = Guid.NewGuid(),
                    FormularioId = dto.FormularioId,
                    DiasActivosJson = diasJson,
                    ExcepcionesJson = excepcionesJson,
                    HorarioInicio = dto.HorarioInicio,
                    HorarioFin = dto.HorarioFin,
                    IntervaloMinutos = dto.IntervaloMinutos
                };

                await _context.ConfiguracionesAgenda.AddAsync(nueva);
            }
            else
            {
                existente.DiasActivosJson = diasJson;
                existente.ExcepcionesJson = excepcionesJson;
                existente.HorarioInicio = dto.HorarioInicio;
                existente.HorarioFin = dto.HorarioFin;
                existente.IntervaloMinutos = dto.IntervaloMinutos;
            }

            await _context.SaveChangesAsync();
            return Ok(new { mensaje = "Configuración guardada." });
        }
    }
}
