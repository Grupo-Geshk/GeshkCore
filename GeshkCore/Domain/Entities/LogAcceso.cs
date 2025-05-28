namespace GeshkCore.Domain.Entities
{
    public class LogAcceso
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid ClientId { get; set; }
        public string Accion { get; set; }  // Ej: login, crear orden, error
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public string? IpOrigen { get; set; }
        public string? Modulo { get; set; }
    }

}
