namespace GeshkCore.Domain.Entities.POS
{
    public class MetodoPago
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OrdenId { get; set; }
        public Orden Orden { get; set; }

        public string Tipo { get; set; }  // Efectivo, Transferencia, etc.
        public string? Referencia { get; set; }
    }

}
