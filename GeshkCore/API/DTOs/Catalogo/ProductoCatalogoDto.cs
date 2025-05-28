namespace GeshkCore.API.DTOs.Catalogo
{
    public class ProductoCatalogoDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string? Nota { get; set; }
        public decimal? Precio { get; set; }
        public bool Visible { get; set; }
        public bool Destacado { get; set; }
        public Guid? ProductoId { get; set; }  // Si viene del inventario
    }

    public class CrearProductoCatalogoDto
    {
        public string Nombre { get; set; }
        public string? Nota { get; set; }
        public decimal? Precio { get; set; }
        public bool Visible { get; set; } = true;
        public bool Destacado { get; set; } = false;
        public Guid? ProductoId { get; set; }
    }
}
