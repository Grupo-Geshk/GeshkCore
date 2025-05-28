namespace GeshkCore.Domain.Entities.Inventario
{
    public class AlertaStock
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ProductoId { get; set; }
        public Producto Producto { get; set; }

        public bool Notificado { get; set; } = false;
        public DateTime? FechaUltimaAlerta { get; set; }
    }

}
