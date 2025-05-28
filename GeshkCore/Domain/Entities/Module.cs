namespace GeshkCore.Domain.Entities
{
    public class Module
    {
        public int Id { get; set; }
        public Guid ClientId { get; set; }
        public Client Client { get; set; }

        public string ModuleKey { get; set; }  // Ej: pos, inventory, citas
        public bool Activo { get; set; } = true;
    }

}
