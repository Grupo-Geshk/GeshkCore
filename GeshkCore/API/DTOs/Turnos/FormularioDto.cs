namespace GeshkCore.API.DTOs.Turnos
{
    public class FormularioDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }  // local, domicilio
        public bool Activo { get; set; }
        public string? PlanRequerido { get; set; }

        public List<CampoPersonalizadoDto> Campos { get; set; }
        public ConfiguracionAgendaDto Configuracion { get; set; }
    }

    public class CrearFormularioDto
    {
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public bool Activo { get; set; } = true;
        public string? PlanRequerido { get; set; }
    }
}
