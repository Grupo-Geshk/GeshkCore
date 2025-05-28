namespace GeshkCore.Domain.Entities.Empleados
{
    public class Disponibilidad
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid EmpleadoId { get; set; }
        public Empleado Empleado { get; set; }

        public string FechasBloqueadasJson { get; set; }  // ej: ["2025-07-15", "2025-08-01"]
        public string VacacionesJson { get; set; }        // ej: [{"inicio": "...", "fin": "..."}]
    }

}
