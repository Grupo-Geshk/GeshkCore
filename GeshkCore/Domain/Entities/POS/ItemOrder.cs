namespace GeshkCore.Domain.Entities.POS
{
    public class ItemOrden
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OrdenId { get; set; }
        public Orden Orden { get; set; }

        public string ProductoNombre { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }

}
