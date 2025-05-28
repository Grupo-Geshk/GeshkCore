namespace GeshkCore.API.DTOs.Catalogo
{
    public class ConfiguracionCatalogoDto
    {
        public Guid Id { get; set; }
        public string LogoClienteUrl { get; set; }
        public string ColorAcento { get; set; }
        public string MensajeBienvenida { get; set; }
        public string? WhatsAppContacto { get; set; }
        public string? PlantillaPublicacion { get; set; }
    }

    public class ActualizarConfiguracionCatalogoDto
    {
        public string LogoClienteUrl { get; set; }
        public string ColorAcento { get; set; }
        public string MensajeBienvenida { get; set; }
        public string? WhatsAppContacto { get; set; }
        public string? PlantillaPublicacion { get; set; }
    }
}
