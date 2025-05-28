namespace GeshkCore.Domain.Entities
{
    public class Client
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string NombreComercial { get; set; }
        public string Subdominio { get; set; }
        public string Plan { get; set; }  // Ej: Starter, Boost
        public bool ModoSandbox { get; set; } = false;
        public bool Activo { get; set; } = true;
        public DateTime FechaActivacion { get; set; } = DateTime.UtcNow;

        public string? ConfiguracionJson { get; set; }  // json con preferencias

        public ICollection<User> Users { get; set; }
        public ICollection<Module> Modules { get; set; }
    }
}
