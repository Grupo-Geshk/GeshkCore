namespace GeshkCore.Domain.Entities.GeshkAdmin
{
    public class PlanModule
    {
        public int Id { get; set; }
        public string Plan { get; set; }  // Starter, Boost, Full, GESHKGratis
        public string ModuleKey { get; set; }  // pos, citas, inventario
        public bool Permitido { get; set; }
    }

}
