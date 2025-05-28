using Microsoft.AspNetCore.Mvc;
using GeshkCore.Infrastructure.DbContext;
using GeshkCore.API.DTOs.GeshkAdmin;
using GeshkCore.API.Controllers;

namespace GeshkCore.API.Controllers.GeshkAdmin
{
    [ApiController]
    [Route("api/admin/logs-acceso")]
    public class LogsAccesoController : ControllerBase
    {
        private readonly GeshkDbContext _context;

        public LogsAccesoController(GeshkDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GET /api/admin/logs-acceso
        /// Listar todos los logs de acceso del sistema (uso exclusivo de GESHK)
        /// </summary>
        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var logs = _context.LogsAcceso
                .OrderByDescending(l => l.Fecha)
                .Select(l => new LogAccesoDto
                {
                    Id = l.Id,
                    UserId = l.UserId,
                    ClientId = l.ClientId,
                    Accion = l.Accion,
                    Fecha = l.Fecha,
                    IpOrigen = l.IpOrigen,
                    Modulo = l.Modulo
                })
                .ToList();

            return Ok(logs);
        }

        /// <summary>
        /// GET /api/admin/logs-acceso/cliente/{clientId}
        /// Listar logs de acceso por cliente
        /// </summary>
        [HttpGet("cliente/{clientId}")]
        public IActionResult ObtenerPorCliente(Guid clientId)
        {
            var logs = _context.LogsAcceso
                .Where(l => l.ClientId == clientId)
                .OrderByDescending(l => l.Fecha)
                .Select(l => new LogAccesoDto
                {
                    Id = l.Id,
                    UserId = l.UserId,
                    ClientId = l.ClientId,
                    Accion = l.Accion,
                    Fecha = l.Fecha,
                    IpOrigen = l.IpOrigen,
                    Modulo = l.Modulo
                })
                .ToList();

            return Ok(logs);
        }
    }
}
