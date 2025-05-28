namespace GeshkCore.Domain.Entities.GeshkAdmin
{
    public class ConsumoRecurso
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ClientId { get; set; }

        public double MbUsados { get; set; }
        public double AnchoBandaMb { get; set; }
        public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;
    }

}
