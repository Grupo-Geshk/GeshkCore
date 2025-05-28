namespace GeshkCore.Domain.Entities.POS
{
    public class ProductoPOS
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ClientId { get; set; }

        public string Nombre { get; set; }
        public decimal Precio { get; set; }
    }

}
