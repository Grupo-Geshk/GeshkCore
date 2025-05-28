namespace GeshkCore.Domain.Entities.Turnos
{
    public class Formulario
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ClientId { get; set; }

        public string Nombre { get; set; }
        public string Tipo { get; set; }  // local o domicilio
        public bool Activo { get; set; } = true;
        public string? PlanRequerido { get; set; }  // ejemplo: Starter, Boost

        public ICollection<CampoPersonalizado> Campos { get; set; }
        public ConfiguracionAgenda Configuracion { get; set; }
        public ICollection<Cita> Citas { get; set; }
    }

}
