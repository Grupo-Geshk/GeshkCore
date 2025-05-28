namespace GeshkCore.API.DTOs.Empleados
{
    public class EmpleadoDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string Cargo { get; set; }
        public string? AvatarUrl { get; set; }
        public DateTime FechaIngreso { get; set; }
        public bool Activo { get; set; }
    }

    public class CrearEmpleadoDto
    {
        public string Nombre { get; set; }
        public string Cargo { get; set; }
        public string? Correo { get; set; }
        public string? Telefono { get; set; }
        public string? AvatarUrl { get; set; }
        public DateTime FechaIngreso { get; set; }
    }
}
