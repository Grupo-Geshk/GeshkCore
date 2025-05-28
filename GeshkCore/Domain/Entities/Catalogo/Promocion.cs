namespace GeshkCore.Domain.Entities.Catalogo
{
    public class Promocion
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ProductoCatalogoId { get; set; }
        public ProductoCatalogo ProductoCatalogo { get; set; }

        public string Titulo { get; set; }
        public string DescripcionCorta { get; set; }
        public bool Activa { get; set; } = true;
        public DateTime FechaPublicacion { get; set; } = DateTime.UtcNow;
    }

}
