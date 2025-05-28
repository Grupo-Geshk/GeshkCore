namespace GeshkCore.API.DTOs.POS
{
    public class CierreCajaDto
    {
        public Guid Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public string ArchivoExcelUrl { get; set; }
    }

    public class CrearCierreCajaDto
    {
        public decimal Total { get; set; }
        public string ArchivoExcelUrl { get; set; }
    }
}
