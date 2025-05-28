namespace GeshkCore.Domain.Entities.Turnos
{
    public class HistorialCita
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CitaId { get; set; }
        public Cita Cita { get; set; }

        public string Accion { get; set; }  // creada, confirmada, cancelada, reasignada, etc.
        public string Usuario { get; set; }  // nombre de usuario o email
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public string? Comentario { get; set; }
    }

}
