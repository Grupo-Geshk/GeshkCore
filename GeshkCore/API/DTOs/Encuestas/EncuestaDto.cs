namespace GeshkCore.API.DTOs.Encuestas
{
    public class EncuestaDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }  // post-cita, post-venta, general
        public bool Activa { get; set; }
        public string? BrandingJson { get; set; }

        public List<PreguntaDto> Preguntas { get; set; }
    }

    public class CrearEncuestaDto
    {
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public bool Activa { get; set; } = true;
        public string? BrandingJson { get; set; }
    }
}
