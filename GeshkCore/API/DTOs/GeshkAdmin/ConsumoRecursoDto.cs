namespace GeshkCore.API.DTOs.GeshkAdmin
{
    public class ConsumoRecursoDto
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public double MbUsados { get; set; }
        public double AnchoBandaMb { get; set; }
        public DateTime FechaActualizacion { get; set; }
    }

    public class ActualizarConsumoRecursoDto
    {
        public double MbUsados { get; set; }
        public double AnchoBandaMb { get; set; }
    }
}
