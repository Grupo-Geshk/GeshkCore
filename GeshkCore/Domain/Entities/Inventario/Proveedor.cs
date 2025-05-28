namespace GeshkCore.Domain.Entities.Inventario
{
    public class Proveedor
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ClientId { get; set; }

        public string Nombre { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public string? Direccion { get; set; }

        public ICollection<Producto> Productos { get; set; }
    }
}
