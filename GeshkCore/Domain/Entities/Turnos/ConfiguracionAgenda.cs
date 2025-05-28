namespace GeshkCore.Domain.Entities.Turnos
{
    public class ConfiguracionAgenda
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid FormularioId { get; set; }
        public Formulario Formulario { get; set; }

        public string DiasActivosJson { get; set; }  // ej: [1,2,3,4,5]
        public string ExcepcionesJson { get; set; }  // ej: ["2025-12-25","2025-01-01"]
        public TimeSpan HorarioInicio { get; set; }
        public TimeSpan HorarioFin { get; set; }
        public int IntervaloMinutos { get; set; }
    }

}
