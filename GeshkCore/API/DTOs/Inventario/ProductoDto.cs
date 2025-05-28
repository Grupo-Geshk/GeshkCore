namespace GeshkCore.API.DTOs.Inventario
{
    public class ProductoDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal Costo { get; set; }
        public int Stock { get; set; }
        public int StockMinimo { get; set; }
        public bool Visible { get; set; }
        public Guid? ProveedorId { get; set; }
    }

    public class CrearProductoDto
    {
        public string Nombre { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal Costo { get; set; }
        public int Stock { get; set; }
        public int StockMinimo { get; set; }
        public bool Visible { get; set; } = true;
        public Guid? ProveedorId { get; set; }
    }
}
