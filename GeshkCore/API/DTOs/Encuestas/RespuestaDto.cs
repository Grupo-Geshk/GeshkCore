namespace GeshkCore.API.DTOs.Encuestas
{
    public class RespuestaDto
    {
        public Guid Id { get; set; }
        public Guid EncuestaId { get; set; }
        public Guid PreguntaId { get; set; }
        public string RespuestaTexto { get; set; }
        public DateTime Fecha { get; set; }
        public string CanalOrigen { get; set; }
    }

    public class CrearRespuestaDto
    {
        public Guid EncuestaId { get; set; }
        public Guid PreguntaId { get; set; }
        public string RespuestaTexto { get; set; }
        public string CanalOrigen { get; set; }
    }
}
