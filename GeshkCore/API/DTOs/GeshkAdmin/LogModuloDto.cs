namespace GeshkCore.API.DTOs.GeshkAdmin
{
    public class LogModuloDto
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public string Modulo { get; set; }
        public string TipoEvento { get; set; }  // operación, error, advertencia
        public string Detalle { get; set; }
        public DateTime Fecha { get; set; }
    }

    public class CrearLogModuloDto
    {
        public Guid ClientId { get; set; }
        public string Modulo { get; set; }
        public string TipoEvento { get; set; }
        public string Detalle { get; set; }
    }
}
