namespace GeshkCore.API.DTOs.POS
{
    public class CrearOrdenDto
    {
        public decimal Total { get; set; }
        public List<CrearItemOrdenDto> Items { get; set; }
        public List<CrearMetodoPagoDto> MetodosPago { get; set; }
        public List<CrearComprobantePagoDto>? ComprobantesPago { get; set; }
    }

    public class CrearItemOrdenDto
    {
        public string ProductoNombre { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }

    public class CrearMetodoPagoDto
    {
        public string Tipo { get; set; }
        public string? Referencia { get; set; }
    }

    public class CrearComprobantePagoDto
    {
        public string UrlArchivo { get; set; }
    }
}
