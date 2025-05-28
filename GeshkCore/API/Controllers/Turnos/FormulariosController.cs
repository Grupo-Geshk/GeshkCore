using Microsoft.AspNetCore.Mvc;
using GeshkCore.API.DTOs.Turnos;
using GeshkCore.Infrastructure.DbContext;
using GeshkCore.Domain.Entities.Turnos;
using Microsoft.Extensions.Configuration;

namespace GeshkCore.API.Controllers.Turnos
{
    [ApiController]
    [Route("api/formularios")]
    public class FormulariosController : ClienteControllerBase
    {
        // ============================
        // ⚙️ CONFIGURACIÓN INICIAL
        // ============================

        private readonly GeshkDbContext _context;

        public FormulariosController(GeshkDbContext context)
        {
            _context = context;
        }

        // ============================
        // 📄 ENDPOINTS DE FORMULARIOS
        // ============================

        /// <summary>
        /// GET /api/formularios
        /// Listar todos los formularios activos
        /// </summary>
        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var clientId = ObtenerClientId();

            var formulariosDb = _context.Formularios
                .Where(f => f.Activo && f.ClientId == clientId)
                .ToList();

            var formularios = formulariosDb.Select(f =>
            {
                var configuracion = _context.ConfiguracionesAgenda
                    .FirstOrDefault(c => c.FormularioId == f.Id);

                return new FormularioDto
                {
                    Id = f.Id,
                    Nombre = f.Nombre,
                    Tipo = f.Tipo,
                    Activo = f.Activo,
                    PlanRequerido = f.PlanRequerido,
                    Campos = _context.CamposPersonalizados
                        .Where(c => c.FormularioId == f.Id)
                        .Select(c => new CampoPersonalizadoDto
                        {
                            Id = c.Id,
                            Etiqueta = c.Etiqueta,
                            Tipo = c.Tipo,
                            Requerido = c.Requerido
                        }).ToList(),
                    Configuracion = configuracion != null ? new ConfiguracionAgendaDto
                    {
                        DiasActivos = System.Text.Json.JsonSerializer.Deserialize<List<int>>(configuracion.DiasActivosJson)!,
                        Excepciones = System.Text.Json.JsonSerializer.Deserialize<List<string>>(configuracion.ExcepcionesJson)!,
                        HorarioInicio = configuracion.HorarioInicio,
                        HorarioFin = configuracion.HorarioFin,
                        IntervaloMinutos = configuracion.IntervaloMinutos
                    } : null
                };
            }).ToList();

            return Ok(formularios);
        }


        /// GET /api/formularios/{id}
        /// Obtener formulario por ID

        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(Guid id)
        {
            var clientId = ObtenerClientId();
            var formulario = _context.Formularios.FirstOrDefault(f => f.Id == id && f.ClientId == clientId);

            if (formulario == null)
                return NotFound("Formulario no encontrado.");

            var campos = _context.CamposPersonalizados
                .Where(c => c.FormularioId == formulario.Id)
                .Select(c => new CampoPersonalizadoDto
                {
                    Id = c.Id,
                    Etiqueta = c.Etiqueta,
                    Tipo = c.Tipo,
                    Requerido = c.Requerido
                }).ToList();

            var configuracionRaw = _context.ConfiguracionesAgenda
                .FirstOrDefault(c => c.FormularioId == formulario.Id);

            ConfiguracionAgendaDto? configuracion = null;

            if (configuracionRaw != null)
            {
                configuracion = new ConfiguracionAgendaDto
                {
                    DiasActivos = System.Text.Json.JsonSerializer.Deserialize<List<int>>(configuracionRaw.DiasActivosJson)!,
                    Excepciones = System.Text.Json.JsonSerializer.Deserialize<List<string>>(configuracionRaw.ExcepcionesJson)!,
                    HorarioInicio = configuracionRaw.HorarioInicio,
                    HorarioFin = configuracionRaw.HorarioFin,
                    IntervaloMinutos = configuracionRaw.IntervaloMinutos
                };
            }

            var dto = new FormularioDto
            {
                Id = formulario.Id,
                Nombre = formulario.Nombre,
                Tipo = formulario.Tipo,
                Activo = formulario.Activo,
                PlanRequerido = formulario.PlanRequerido,
                Campos = campos,
                Configuracion = configuracion
            };

            return Ok(dto);
        }


        /// POST /api/formularios
        /// Crear un nuevo formulario
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearFormularioDto dto)
        {

            var formulario = new Formulario
            {
                Id = Guid.NewGuid(),
                ClientId = ObtenerClientId(), // TODO: Obtener desde JWT
                Nombre = dto.Nombre,
                Tipo = dto.Tipo,
                Activo = dto.Activo,
                PlanRequerido = dto.PlanRequerido
            };

            await _context.Formularios.AddAsync(formulario);
            await _context.SaveChangesAsync();

            return Ok(new { formulario.Id, mensaje = "Formulario creado." });
        }
    }
}
