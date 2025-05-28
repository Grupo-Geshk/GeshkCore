namespace GeshkCore.Domain.Entities.Inventario
{
    public class Producto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ClientId { get; set; }

        public string Nombre { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal Costo { get; set; }
        public int Stock { get; set; }
        public int StockMinimo { get; set; }
        public bool Visible { get; set; } = true;

        public Guid? ProveedorId { get; set; }
        public Proveedor? Proveedor { get; set; }

        public ICollection<EntradaInventario> Entradas { get; set; }
        public ICollection<SalidaInventario> Salidas { get; set; }
    }

}
