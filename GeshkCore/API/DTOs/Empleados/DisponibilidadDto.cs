namespace GeshkCore.API.DTOs.Empleados
{
    public class DisponibilidadDto
    {
        public Guid Id { get; set; }
        public Guid EmpleadoId { get; set; }
        public List<string> FechasBloqueadas { get; set; }
        public List<string> Vacaciones { get; set; } // formato: ["2025-06-01:2025-06-10"]
    }

    public class CrearDisponibilidadDto : DisponibilidadDto
    {
    }
}
