namespace GeshkCore.API.DTOs.Turnos
{
    public class CitaDto
    {
        public Guid Id { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public string ClienteNombre { get; set; }
        public string Telefono { get; set; }
        public string? Correo { get; set; }
        public string Estado { get; set; }
        public string? ObservacionesInternas { get; set; }

        public Guid FormularioId { get; set; }
        public List<HistorialCitaDto> Historial { get; set; }
    }

    public class CrearCitaDto
    {
        public Guid FormularioId { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public string ClienteNombre { get; set; }
        public string Telefono { get; set; }
        public string? Correo { get; set; }
        public Dictionary<string, string> CamposPersonalizados { get; set; }
    }
}
