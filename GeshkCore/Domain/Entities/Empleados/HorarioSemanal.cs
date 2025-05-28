namespace GeshkCore.Domain.Entities.Empleados
{
    public class HorarioSemanal
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid EmpleadoId { get; set; }
        public Empleado Empleado { get; set; }

        public int DiaSemana { get; set; }  // 1=Lunes ... 7=Domingo
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }

        public string Grupo { get; set; }  // cocina, taller, caja
        public string? Ubicacion { get; set; }
    }

}
