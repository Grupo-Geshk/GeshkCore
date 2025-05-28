namespace GeshkCore.API.DTOs.Empleados
{
    public class GrupoTrabajoDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
    }

    public class CrearGrupoTrabajoDto
    {
        public string Nombre { get; set; }
        public string? Descripcion { get; set; }
    }
}
