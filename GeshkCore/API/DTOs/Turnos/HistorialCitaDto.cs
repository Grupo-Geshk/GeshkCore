namespace GeshkCore.API.DTOs.Turnos
{
    public class HistorialCitaDto
    {
        public Guid Id { get; set; }
        public string Accion { get; set; }
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public string? Comentario { get; set; }
    }
}
