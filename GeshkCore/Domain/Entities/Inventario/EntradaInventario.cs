namespace GeshkCore.Domain.Entities.Inventario
{
    public class EntradaInventario
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ProductoId { get; set; }
        public Producto Producto { get; set; }

        public Guid ClientId { get; set; }
        public int Cantidad { get; set; }
        public string Tipo { get; set; }  // compra, devolución, ajuste
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
    }

}
