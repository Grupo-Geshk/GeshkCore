namespace GeshkCore.API.DTOs.Empleados
{
    public class HistorialEmpleadoDto
    {
        public Guid Id { get; set; }
        public Guid EmpleadoId { get; set; }
        public string Accion { get; set; }
        public string Comentario { get; set; }
        public string RegistradoPor { get; set; }
        public DateTime Fecha { get; set; }
    }

    public class CrearHistorialEmpleadoDto
    {
        public Guid EmpleadoId { get; set; }
        public string Accion { get; set; }
        public string Comentario { get; set; }
        public string RegistradoPor { get; set; }
        public DateTime Fecha { get; set; }
    }
}
