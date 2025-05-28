namespace GeshkCore.Domain.Entities.Encuestas
{
    public class ConfiguracionEncuesta
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid EncuestaId { get; set; }
        public Encuesta Encuesta { get; set; }

        public string Visibilidad { get; set; }  // pública, privada, con token
        public bool ActivarQR { get; set; }
        public bool ActivarPostVenta { get; set; }
        public bool ActivarPostCita { get; set; }
    }

}
