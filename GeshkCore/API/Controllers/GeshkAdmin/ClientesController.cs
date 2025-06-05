using GeshkCore.Infrastructure.DbContext;
using GeshkCore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeshkCore.API.Controllers.GeshkAdmin
{
    [ApiController]
    [Route("api/admin/clientes")]
    public class ClientesController : ControllerBase
    {
        private readonly GeshkDbContext _context;

        public ClientesController(GeshkDbContext context)
        {
            _context = context;
        }

        // GET: /api/admin/clientes
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var clientes = await _context.Clients
                .OrderBy(c => c.NombreComercial)
                .Select(c => new
                {
                    c.Id,
                    c.NombreComercial,
                    c.Subdominio,
                    c.Plan,
                    Estado = c.Activo ? "activo" : "suspendido", // 🔧 aquí
                    c.ModoSandbox,
                    ModulosActivos = _context.Modules
                        .Where(m => m.ClientId == c.Id)
                        .Select(m => m.ModuleKey)
                        .ToList()
                })
                .ToListAsync();

            return Ok(clientes);
        }

        // GET: /api/admin/clientes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var cliente = await _context.Clients.FindAsync(id);
            if (cliente == null) return NotFound();

            var modulos = await _context.Modules
                .Where(m => m.ClientId == id)
                .Select(m => m.ModuleKey)
                .ToListAsync();

            return Ok(new
            {
                cliente.Id,
                cliente.NombreComercial,
                cliente.Subdominio,
                cliente.Plan,
                Estado = cliente.Activo ? "activo" : "suspendido",
                cliente.ModoSandbox,
                ModulosActivos = modulos
            });
        }

        // PUT: /api/admin/clientes/{id}/estado
        [HttpPut("{id}/estado")]
        public async Task<ActionResult> CambiarEstado(Guid id)
        {
            var cliente = await _context.Clients.FindAsync(id);
            if (cliente == null) return NotFound();

            cliente.Activo = !cliente.Activo;
            await _context.SaveChangesAsync();

            var estado = cliente.Activo ? "activo" : "suspendido";
            return Ok(new { cliente.Id, Estado = estado });
        }

        // PUT: /api/admin/clientes/{id}/plan
        [HttpPut("{id}/plan")]
        public async Task<ActionResult> CambiarPlan(Guid id, [FromBody] string nuevoPlan)
        {
            var cliente = await _context.Clients.FindAsync(id);
            if (cliente == null) return NotFound();

            cliente.Plan = nuevoPlan;
            await _context.SaveChangesAsync();

            return Ok(new { cliente.Id, cliente.Plan });
        }

        // PUT: /api/admin/clientes/{id}/modulos
        [HttpPut("{id}/modulos")]
        public async Task<ActionResult> ActualizarModulos(Guid id, [FromBody] List<string> modulos)
        {
            var cliente = await _context.Clients.FindAsync(id);
            if (cliente == null) return NotFound();

            var plan = cliente.Plan;

            var permitidos = await _context.PlanModules
                .Where(p => p.Plan == plan && p.Permitido)
                .Select(p => p.ModuleKey)
                .ToListAsync();

            if (modulos.Except(permitidos).Any())
                return BadRequest("Incluye módulos no permitidos por el plan.");

            var existentes = await _context.Modules
                .Where(m => m.ClientId == id)
                .ToListAsync();

            _context.Modules.RemoveRange(existentes);

            foreach (var key in modulos)
            {
                _context.Modules.Add(new GeshkCore.Domain.Entities.Module
                {
                    ClientId = id,
                    ModuleKey = key
                });
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        // POST: /api/admin/clientes/temporal
        [HttpPost("temporal")]
        public async Task<ActionResult> CrearClienteTemporal()
        {
            var nuevoCliente = new Client
            {
                Id = Guid.NewGuid(),
                NombreComercial = "Demo Temporal " + DateTime.UtcNow.Ticks,
                Subdominio = "demo" + DateTime.UtcNow.Ticks,
                Plan = "Boost",
                Activo = true,
                ModoSandbox = true,
                FechaActivacion = DateTime.UtcNow
            };

            _context.Clients.Add(nuevoCliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = nuevoCliente.Id }, null);
        }
    }
}
