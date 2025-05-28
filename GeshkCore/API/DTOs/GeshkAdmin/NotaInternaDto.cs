namespace GeshkCore.API.DTOs.GeshkAdmin
{
    public class NotaInternaDto
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public string UsuarioGeshk { get; set; }
        public string Contenido { get; set; }
        public DateTime Fecha { get; set; }
    }

    public class CrearNotaInternaDto
    {
        public Guid ClientId { get; set; }
        public string UsuarioGeshk { get; set; }
        public string Contenido { get; set; }
    }
}
