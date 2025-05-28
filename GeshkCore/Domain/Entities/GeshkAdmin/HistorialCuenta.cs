namespace GeshkCore.Domain.Entities.GeshkAdmin
{
    public class HistorialCuenta
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ClientId { get; set; }

        public string TipoEvento { get; set; }  // activación, cambio_plan, suspensión
        public string? Nota { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
    }

}
