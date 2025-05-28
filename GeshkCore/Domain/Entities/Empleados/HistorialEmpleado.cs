namespace GeshkCore.Domain.Entities.Empleados
{
    public class HistorialEmpleado
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid EmpleadoId { get; set; }
        public Empleado Empleado { get; set; }

        public string Accion { get; set; }  // ingreso, baja, cambio de grupo
        public string Comentario { get; set; }
        public string RegistradoPor { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
    }

}
