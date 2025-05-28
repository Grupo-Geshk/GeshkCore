namespace GeshkCore.Domain.Entities.Catalogo
{
    public class Publicacion
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ProductoCatalogoId { get; set; }
        public ProductoCatalogo ProductoCatalogo { get; set; }

        public string Fondo { get; set; }  // nombre o url del fondo usado
        public string? TextoExtra { get; set; }
        public string UrlImagenGenerada { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    }

}
