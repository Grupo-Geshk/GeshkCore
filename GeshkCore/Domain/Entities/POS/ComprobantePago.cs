namespace GeshkCore.Domain.Entities.POS
{
    public class ComprobantePago
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OrdenId { get; set; }
        public Orden Orden { get; set; }

        public string UrlArchivo { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
    }

}
