namespace GeshkCore.Domain.Entities.Empleados
{
    public class Asistencia
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid EmpleadoId { get; set; }
        public Empleado Empleado { get; set; }

        public DateTime Fecha { get; set; }
        public string Tipo { get; set; }  // presente, falta, retiro
        public string? Observacion { get; set; }
        public string Usuario { get; set; }  // quién registró
    }

}
