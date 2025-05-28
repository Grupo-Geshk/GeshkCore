namespace GeshkCore.API.DTOs.Catalogo
{
    public class PublicacionDto
    {
        public Guid Id { get; set; }
        public Guid ProductoCatalogoId { get; set; }
        public string Fondo { get; set; }
        public string? TextoExtra { get; set; }
        public string UrlImagenGenerada { get; set; }
        public DateTime FechaCreacion { get; set; }
    }

    public class CrearPublicacionDto
    {
        public Guid ProductoCatalogoId { get; set; }
        public string Fondo { get; set; }
        public string? TextoExtra { get; set; }
        public string UrlImagenGenerada { get; set; }
    }
}
