namespace GeshkCore.Domain.Entities.Catalogo
{
    public class ProductoCatalogo
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ClientId { get; set; }

        public Guid? ProductoId { get; set; }  // FK opcional si viene del inventario
        public string Nombre { get; set; }
        public string? Nota { get; set; }
        public decimal? Precio { get; set; }
        public bool Visible { get; set; } = true;
        public bool Destacado { get; set; } = false;

        public ICollection<Promocion> Promociones { get; set; }
        public ICollection<Publicacion> Publicaciones { get; set; }
    }

}
