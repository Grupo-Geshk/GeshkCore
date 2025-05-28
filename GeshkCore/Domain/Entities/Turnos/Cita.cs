namespace GeshkCore.Domain.Entities.Turnos
{
    public class Cita
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ClientId { get; set; }
        public Guid FormularioId { get; set; }
        public Formulario Formulario { get; set; }

        public string ClienteNombre { get; set; }
        public string Telefono { get; set; }
        public string? Correo { get; set; }

        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }

        public string Estado { get; set; }  // pendiente, confirmada, cancelada, finalizada
        public string? ObservacionesInternas { get; set; }

        public ICollection<HistorialCita> Historial { get; set; }
    }

}
