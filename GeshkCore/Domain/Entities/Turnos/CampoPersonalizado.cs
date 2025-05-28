namespace GeshkCore.Domain.Entities.Turnos
{
    public class CampoPersonalizado
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid FormularioId { get; set; }
        public Formulario Formulario { get; set; }

        public string Etiqueta { get; set; }
        public string Tipo { get; set; }  // texto, selección, número, etc.
        public bool Requerido { get; set; }
    }

}
