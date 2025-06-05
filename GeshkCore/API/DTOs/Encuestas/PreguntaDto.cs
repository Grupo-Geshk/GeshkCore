namespace GeshkCore.API.DTOs.Encuestas
{
    public class PreguntaDto
    {
        public Guid Id { get; set; }
        public string Texto { get; set; }
        public Guid EncuestaId { get; set; }  // ✅ Esta es la línea que falta
        public string Tipo { get; set; }  // texto, escala, opción
        public bool Requerida { get; set; }
    }

    public class CrearPreguntaDto
    {
        public Guid EncuestaId { get; set; }
        public string Texto { get; set; }
        public string Tipo { get; set; }
        public bool Requerida { get; set; }
    }
}
