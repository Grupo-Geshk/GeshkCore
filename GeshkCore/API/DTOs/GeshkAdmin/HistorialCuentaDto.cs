namespace GeshkCore.API.DTOs.GeshkAdmin
{
    public class HistorialCuentaDto
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public string TipoEvento { get; set; }  // activación, suspensión, cambio_plan
        public string? Nota { get; set; }
        public DateTime Fecha { get; set; }
    }

    public class CrearHistorialCuentaDto
    {
        public Guid ClientId { get; set; }
        public string TipoEvento { get; set; }
        public string? Nota { get; set; }
    }
}
