namespace GeshkCore.Domain.Entities.Empleados
{
    public class Empleado
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ClientId { get; set; }

        public string Nombre { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string Cargo { get; set; }
        public string? AvatarUrl { get; set; }

        public DateTime FechaIngreso { get; set; } = DateTime.UtcNow;
        public bool Activo { get; set; } = true;

        public ICollection<Asistencia> Asistencias { get; set; }
        public ICollection<HistorialEmpleado> Historial { get; set; }
    }

}
