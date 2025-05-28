namespace GeshkCore.Domain.Entities.GeshkAdmin
{
    public class LogModulo
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ClientId { get; set; }

        public string Modulo { get; set; }  // pos, citas, inventario...
        public string TipoEvento { get; set; }  // error, operación, advertencia
        public string Detalle { get; set; }

        public DateTime Fecha { get; set; } = DateTime.UtcNow;
    }

}
