namespace GeshkCore.Domain.Entities.Empleados
{
    public class GrupoTrabajo
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ClientId { get; set; }

        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
    }

}
