using Microsoft.AspNetCore.Mvc;
using GeshkCore.API.DTOs.POS;
using GeshkCore.Infrastructure.DbContext;
using GeshkCore.Domain.Entities.POS;

namespace GeshkCore.API.Controllers.POS
{
    [ApiController]
    [Route("api/mesas")]
    public class MesasController : ControllerBase
    {
        // ========================
        // ⚙️ CONFIGURACIÓN INICIAL
        // ========================

        private readonly GeshkDbContext _context;

        public MesasController(GeshkDbContext context)
        {
            _context = context;
        }

        // ========================
        // 📦 ENDPOINTS DE MESAS
        // ========================

        /// GET /api/mesas
        /// Listar todas las mesas (solo si el negocio es restaurante)
        [HttpGet]
        public IActionResult ObtenerTodas()
        {
            // TODO: Validar si el cliente es restaurante desde el contexto o JWT

            var mesas = _context.Mesas
                .Select(m => new MesaDto
                {
                    Id = m.Id,
                    Zona = m.Zona,
                    Numero = m.Numero,
                    Estado = m.Estado
                })
                .ToList();

            return Ok(mesas);
        }

        /// GET /api/mesas/{id}
        /// Obtener una mesa por ID
        [HttpGet("{id}")]
        public IActionResult ObtenerPorId(Guid id)
        {
            var mesa = _context.Mesas.Find(id);
            if (mesa == null)
                return NotFound("Mesa no encontrada.");

            return Ok(new MesaDto
            {
                Id = mesa.Id,
                Zona = mesa.Zona,
                Numero = mesa.Numero,
                Estado = mesa.Estado
            });
        }

        /// POST /api/mesas
        /// Crear una nueva mesa
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CrearMesaDto dto)
        {
            var mesa = new Mesa
            {
                ClientId = Guid.Empty, // TODO: Obtener desde el token
                Zona = dto.Zona,
                Numero = dto.Numero,
                Estado = "libre"
            };

            await _context.Mesas.AddAsync(mesa);
            await _context.SaveChangesAsync();

            return Ok(new { mesa.Id, mensaje = "Mesa creada." });
        }

        /// PUT /api/mesas/{id}
        /// Editar zona, número o estado de una mesa
        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(Guid id, [FromBody] MesaDto dto)
        {
            var mesa = await _context.Mesas.FindAsync(id);
            if (mesa == null)
                return NotFound("Mesa no encontrada.");

            mesa.Zona = dto.Zona;
            mesa.Numero = dto.Numero;
            mesa.Estado = dto.Estado;
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Mesa actualizada." });
        }

        /// DELETE /api/mesas/{id}
        /// Eliminar una mesa por ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(Guid id)
        {
            var mesa = await _context.Mesas.FindAsync(id);
            if (mesa == null)
                return NotFound("Mesa no encontrada.");

            _context.Mesas.Remove(mesa);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Mesa eliminada." });
        }
    }
}