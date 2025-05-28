namespace GeshkCore.Domain.Entities.Encuestas
{
    public class Encuesta
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ClientId { get; set; }

        public string Nombre { get; set; }
        public string Tipo { get; set; }  // post-venta, post-cita, general
        public bool Activa { get; set; } = true;

        public string? BrandingJson { get; set; }  // colores, mensaje, logo si aplica

        public ICollection<Pregunta> Preguntas { get; set; }
        public ICollection<Respuesta> Respuestas { get; set; }
    }

}
