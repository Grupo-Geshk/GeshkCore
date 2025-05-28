namespace GeshkCore.Domain.Entities.Encuestas
{
    public class Respuesta
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid EncuestaId { get; set; }
        public Encuesta Encuesta { get; set; }

        public Guid PreguntaId { get; set; }
        public Pregunta Pregunta { get; set; }

        public string RespuestaTexto { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
        public string CanalOrigen { get; set; }  // QR, POS, cita
    }

}
