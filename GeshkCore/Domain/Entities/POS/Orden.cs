namespace GeshkCore.Domain.Entities.POS
{
    public class Orden
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ClientId { get; set; }
        public Guid? UserId { get; set; }

        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public string Estado { get; set; }  // abierta, cerrada, anulada
        public decimal Total { get; set; }

        public ICollection<ItemOrden> Items { get; set; }
        public ICollection<MetodoPago> MetodosPago { get; set; }
        public ICollection<ComprobantePago> Comprobantes { get; set; }
    }
}
