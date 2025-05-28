namespace GeshkCore.API.DTOs.POS
{
    public class ProductoPOSDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
    }

    public class CrearProductoPOSDto
    {
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
    }
}
