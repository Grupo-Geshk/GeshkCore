using Microsoft.AspNetCore.Mvc;
using GeshkCore.API.DTOs.Turnos;
using GeshkCore.Infrastructure.DbContext;
using GeshkCore.Domain.Entities.Turnos;

namespace GeshkCore.API.Controllers.Turnos
{
    [ApiController]
    [Route("api/citas")]
    public class CitasController : ControllerBase
    {
        // ========================
        // ⚙️ CONFIGURACIÓN INICIAL
        // ========================

        private readonly GeshkDbContext _context;

        public CitasController(GeshkDbContext context)
        {
            _context = context;
        }

        // ========================
        // 📆 ENDPOINTS DE CITAS
        // ========================

        /// GET /api/citas
        /// Obtener todas las citas del cliente
        [HttpGet]
        public IActionResult ObtenerTodas()
        {
            // TODO: filtrar por ClientId desde JWT

            var citas = _context.Citas
                .Select(c => new CitaDto
                {
                    Id = c.Id,
                    ClienteNombre = c.ClienteNombre,
                    Telefono = c.Telefono,
                    Correo = c.Correo,
                    Estado = c.Estado,
                    Fecha = c.Fecha,
                    Hora = c.Hora,
                    ObservacionesInternas = c.ObservacionesInternas,
                    FormularioId = c.FormularioId,
                    Historial = _context.HistorialCitas
                        .Where(h => h.CitaId == c.Id)
                        .Select(h => new HistorialCitaDto
                        {
                            Id = h.Id,
                            Accion = h.Accion,
                            Usuario = h.Usuario,
                            Fecha = h.Fecha,
                            Comentario = h.Comentario
                        }).ToList()
                }).ToList();

            return Ok(citas);
        }

        /// GET /api/citas/{id}
        /// Obtener una cita por ID
        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(Guid id)
        {
            var cita = _context.Citas.FirstOrDefault(c => c.Id == id);
            if (cita == null)
                return NotFound("Cita no encontrada.");

            var dto = new CitaDto
            {
                Id = cita.Id,
                ClienteNombre = cita.ClienteNombre,
                Telefono = cita.Telefono,
                Correo = cita.Correo,
                Estado = cita.Estado,
                Fecha = cita.Fecha,
                Hora = cita.Hora,
                ObservacionesInternas = cita.ObservacionesInternas,
                FormularioId = cita.FormularioId,
                Historial = _context.HistorialCitas
                    .Where(h => h.CitaId == cita.Id)
                    .Select(h => new HistorialCitaDto
                    {
                        Id = h.Id,
                        Accion = h.Accion,
                        Usuario = h.Usuario,
                        Fecha = h.Fecha,
                        Comentario = h.Comentario
                    }).ToList()
            };

            return Ok(dto);
        }

        /// POST /api/citas
        /// Crear una nueva cita (desde el cliente final)
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearCitaDto dto)
        {
            if (dto.CamposPersonalizados == null || !dto.CamposPersonalizados.Any())
                return BadRequest("Se requieren los campos personalizados del formulario.");

            var cita = new Cita
            {
                Id = Guid.NewGuid(),
                ClientId = Guid.Empty, // TODO: obtener desde JWT si es necesario
                FormularioId = dto.FormularioId,
                ClienteNombre = dto.ClienteNombre,
                Telefono = dto.Telefono,
                Correo = dto.Correo,
                Fecha = dto.Fecha,
                Hora = dto.Hora,
                Estado = "pendiente",
                ObservacionesInternas = string.Empty // editable luego
            };

            await _context.Citas.AddAsync(cita);

            await _context.HistorialCitas.AddAsync(new HistorialCita
            {
                Id = Guid.NewGuid(),
                CitaId = cita.Id,
                Accion = "creada",
                Usuario = "sistema",
                Fecha = DateTime.UtcNow,
                Comentario = "Cita registrada por el cliente final."
            });

            await _context.SaveChangesAsync();
            return Ok(new { cita.Id, mensaje = "Cita registrada exitosamente." });
        }
    }
}