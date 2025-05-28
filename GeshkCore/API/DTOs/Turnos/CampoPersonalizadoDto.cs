namespace GeshkCore.API.DTOs.Turnos
{
    public class CampoPersonalizadoDto
    {
        public Guid Id { get; set; }
        public string Etiqueta { get; set; }
        public string Tipo { get; set; }
        public bool Requerido { get; set; }
    }

    public class CrearCampoPersonalizadoDto
    {
        public Guid FormularioId { get; set; }
        public string Etiqueta { get; set; }
        public string Tipo { get; set; }
        public bool Requerido { get; set; }
    }
}
