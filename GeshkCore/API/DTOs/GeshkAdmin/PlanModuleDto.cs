namespace GeshkCore.API.DTOs.GeshkAdmin
{
    public class PlanModuleDto
    {
        public int Id { get; set; }
        public string Plan { get; set; }       // Starter, Boost, Suite
        public string ModuleKey { get; set; }  // pos, citas, inventario
        public bool Permitido { get; set; }
    }

    public class CrearPlanModuleDto
    {
        public string Plan { get; set; }
        public string ModuleKey { get; set; }
        public bool Permitido { get; set; }
    }
}
