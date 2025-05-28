namespace GeshkCore.API.DTOs.Encuestas
{
    public class ConfiguracionEncuestaDto
    {
        public Guid Id { get; set; }
        public Guid EncuestaId { get; set; }
        public string Visibilidad { get; set; }  // pública, con QR, protegida
        public bool ActivarQR { get; set; }
        public bool ActivarPostVenta { get; set; }
        public bool ActivarPostCita { get; set; }
    }

    public class CrearConfiguracionEncuestaDto : ConfiguracionEncuestaDto
    {
    }
}
