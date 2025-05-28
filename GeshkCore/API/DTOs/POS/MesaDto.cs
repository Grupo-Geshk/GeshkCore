namespace GeshkCore.API.DTOs.POS
{
    public class MesaDto
    {
        public Guid Id { get; set; }
        public string Zona { get; set; }
        public int Numero { get; set; }
        public string Estado { get; set; } // libre, ocupada, reservada
    }

    public class CrearMesaDto
    {
        public string Zona { get; set; }
        public int Numero { get; set; }
    }
}
