namespace GeshkCore.Domain.Entities.POS
{
    public class Mesa
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ClientId { get; set; }

        public string Zona { get; set; }
        public int Numero { get; set; }
        public string Estado { get; set; }  // libre, ocupada, reservada
    }

}
