namespace GeshkCore.API.DTOs.GeshkAdmin
{
    public class LogAccesoDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ClientId { get; set; }
        public string Accion { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string? IpOrigen { get; set; }
        public string? Modulo { get; set; }
    }
}
