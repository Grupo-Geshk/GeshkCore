namespace GeshkCore.Domain.Entities.Catalogo
{
    public class ConfiguracionCatalogo
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ClientId { get; set; }

        public string LogoClienteUrl { get; set; }
        public string ColorAcento { get; set; }
        public string MensajeBienvenida { get; set; }
        public string? WhatsAppContacto { get; set; }
        public string? PlantillaPublicacion { get; set; }
    }

}
