namespace GeshkCore.API.DTOs.Inventario
{
    public class AlertaStockDto
    {
        public Guid Id { get; set; }
        public Guid ProductoId { get; set; }
        public string Nombre { get; set; } = string.Empty; // ✅ agregar este campo
        public bool Notificado { get; set; }
        public DateTime? FechaUltimaAlerta { get; set; }
        public int Stock { get; set; }
        public int StockMinimo { get; set; }
    }
}
