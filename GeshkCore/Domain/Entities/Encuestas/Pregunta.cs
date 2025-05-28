namespace GeshkCore.Domain.Entities.Encuestas
{
    public class Pregunta
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid EncuestaId { get; set; }
        public Encuesta Encuesta { get; set; }

        public string Texto { get; set; }
        public string Tipo { get; set; }  // texto, selección, escala
        public bool Requerida { get; set; } = true;
    }

}
