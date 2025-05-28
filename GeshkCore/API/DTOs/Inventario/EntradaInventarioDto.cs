namespace GeshkCore.API.DTOs.Inventario
{
    public class EntradaInventarioDto
    {
        public Guid Id { get; set; }
        public Guid ProductoId { get; set; }
        public int Cantidad { get; set; }
        public string Tipo { get; set; }
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; }
    }

    public class CrearEntradaInventarioDto
    {
        public Guid ProductoId { get; set; }
        public int Cantidad { get; set; }
        public string Tipo { get; set; }  // compra, devolución, ajuste
        public string Usuario { get; set; }
    }
}
