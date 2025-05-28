namespace GeshkCore.API.DTOs.Empleados
{
    public class AsistenciaDto
    {
        public Guid Id { get; set; }
        public Guid EmpleadoId { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; }  // presente, falta, retiro
        public string? Observacion { get; set; }
        public string Usuario { get; set; }
    }

    public class CrearAsistenciaDto
    {
        public Guid EmpleadoId { get; set; }
        public DateTime Fecha { get; set; }
        public string Tipo { get; set; }
        public string Usuario { get; set; }
        public string? Observacion { get; set; }
    }
}
