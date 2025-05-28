namespace GeshkCore.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ClientId { get; set; }
        public Client Client { get; set; }

        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public string PasswordHash { get; set; }
        public string Rol { get; set; }  // admin, gerente, etc.
        public bool Activo { get; set; } = true;
    }
}
