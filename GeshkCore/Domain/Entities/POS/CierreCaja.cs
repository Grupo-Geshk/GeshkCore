namespace GeshkCore.Domain.Entities.POS
{
    public class CierreCaja
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ClientId { get; set; }

        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public decimal Total { get; set; }
        public string ArchivoExcelUrl { get; set; }
    }

}
