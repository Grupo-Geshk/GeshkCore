namespace GeshkCore.API.DTOs.Empleados
{
    public class HorarioSemanalDto
    {
        public Guid Id { get; set; }
        public Guid EmpleadoId { get; set; }
        public int DiaSemana { get; set; }  // 1 = lunes
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public string Grupo { get; set; }
        public string? Ubicacion { get; set; }
    }

    public class CrearHorarioSemanalDto
    {
        public Guid EmpleadoId { get; set; }
        public int DiaSemana { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public string Grupo { get; set; }
        public string? Ubicacion { get; set; }
    }
}
