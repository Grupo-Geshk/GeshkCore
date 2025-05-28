namespace GeshkCore.API.DTOs.Catalogo
{
    public class PromocionDto
    {
        public Guid Id { get; set; }
        public Guid ProductoCatalogoId { get; set; }
        public string Titulo { get; set; }
        public string DescripcionCorta { get; set; }
        public bool Activa { get; set; }
        public DateTime FechaPublicacion { get; set; }
    }

    public class CrearPromocionDto
    {
        public Guid ProductoCatalogoId { get; set; }
        public string Titulo { get; set; }
        public string DescripcionCorta { get; set; }
    }
}
