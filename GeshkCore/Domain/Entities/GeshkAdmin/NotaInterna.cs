namespace GeshkCore.Domain.Entities.GeshkAdmin
{
    public class NotaInterna
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ClientId { get; set; }

        public string UsuarioGeshk { get; set; }  // ej. GioJovane
        public string Contenido { get; set; }
        public DateTime Fecha { get; set; } = DateTime.UtcNow;
    }

}
