namespace GeshkCore.Domain.Entities.Inventario
{
    public class SalidaInventario
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ProductoId { get; set; }
        public Producto Producto { get; set; }

        public Guid ClientId { get; set; }
        public int Cantidad { get; set; }
        public string Tipo { get; set; }  // venta, merma, uso
        public string ModuloOrigen { get; set; }  // pos, citas, manual
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
    }

}
