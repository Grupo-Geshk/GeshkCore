namespace GeshkCore.API.DTOs.POS
{
    public class OrdenDto
    {
        public Guid Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }
        public decimal Total { get; set; }

        public List<ItemOrdenDto> Items { get; set; }
        public List<MetodoPagoDto> MetodosPago { get; set; }
        public List<ComprobantePagoDto> ComprobantesPago { get; set; }
    }

    public class ItemOrdenDto
    {
        public string ProductoNombre { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }

    public class MetodoPagoDto
    {
        public string Tipo { get; set; }  // efectivo, yappy, etc.
        public string? Referencia { get; set; }
    }

    public class ComprobantePagoDto
    {
        public string UrlArchivo { get; set; }
        public DateTime Fecha { get; set; }
    }
}
