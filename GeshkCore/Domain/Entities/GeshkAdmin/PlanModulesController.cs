using GeshkCore.API.DTOs.GeshkAdmin;
using GeshkCore.Domain.Entities.GeshkAdmin;
using GeshkCore.Infrastructure.DbContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GeshkCore.API.Controllers.GeshkAdmin
{
    [ApiController]
    [Route("api/admin/planes/modulos")]
    public class PlanModulesController : ControllerBase
    {
        private readonly GeshkDbContext _context;

        public PlanModulesController(GeshkDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lista todas las combinaciones plan ↔ módulo registradas.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlanModuleDto>>> GetAll()
        {
            var data = await _context.PlanModules
                .OrderBy(p => p.Plan)
                .ThenBy(p => p.ModuleKey)
                .Select(p => new PlanModuleDto
                {
                    Id = p.Id,
                    Plan = p.Plan,
                    ModuleKey = p.ModuleKey,
                    Permitido = p.Permitido
                })
                .ToListAsync();

            return Ok(data);
        }

        /// <summary>
        /// Devuelve los módulos permitidos para un plan específico.
        /// </summary>
        [HttpGet("{plan}")]
        public async Task<ActionResult<IEnumerable<PlanModuleDto>>> GetByPlan(string plan)
        {
            var data = await _context.PlanModules
                .Where(p => p.Plan.ToLower() == plan.ToLower())
                .OrderBy(p => p.ModuleKey)
                .Select(p => new PlanModuleDto
                {
                    Id = p.Id,
                    Plan = p.Plan,
                    ModuleKey = p.ModuleKey,
                    Permitido = p.Permitido
                })
                .ToListAsync();

            return Ok(data);
        }

        /// <summary>
        /// Crea una nueva relación entre un plan y un módulo.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CrearPlanModuleDto dto)
        {
            var exists = await _context.PlanModules.AnyAsync(p =>
                p.Plan.ToLower() == dto.Plan.ToLower() &&
                p.ModuleKey.ToLower() == dto.ModuleKey.ToLower());

            if (exists)
                return Conflict("Ya existe una relación entre este plan y módulo.");

            var newRelation = new PlanModule
            {
                Plan = dto.Plan,
                ModuleKey = dto.ModuleKey,
                Permitido = dto.Permitido
            };

            _context.PlanModules.Add(newRelation);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetByPlan), new { plan = dto.Plan }, null);
        }

        /// <summary>
        /// Actualiza si un módulo está permitido en un plan.
        /// </summary>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] CrearPlanModuleDto dto)
        {
            var record = await _context.PlanModules.FindAsync(id);
            if (record == null)
                return NotFound();

            record.Permitido = dto.Permitido;
            record.Plan = dto.Plan;
            record.ModuleKey = dto.ModuleKey;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Elimina la asignación plan ↔ módulo.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var record = await _context.PlanModules.FindAsync(id);
            if (record == null)
                return NotFound();

            _context.PlanModules.Remove(record);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
