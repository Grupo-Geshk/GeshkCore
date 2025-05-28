namespace GeshkCore.API.DTOs.Turnos
{
    public class ConfiguracionAgendaDto
    {
        public List<int> DiasActivos { get; set; }         // 1-7 (Lunes-Domingo)
        public List<string> Excepciones { get; set; }      // fechas bloqueadas
        public TimeSpan HorarioInicio { get; set; }
        public TimeSpan HorarioFin { get; set; }
        public int IntervaloMinutos { get; set; }
    }

    public class CrearConfiguracionAgendaDto : ConfiguracionAgendaDto
    {
        public Guid FormularioId { get; set; }
    }
}
