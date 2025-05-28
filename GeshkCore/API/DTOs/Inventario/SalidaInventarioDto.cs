namespace GeshkCore.API.DTOs.Inventario
{
    public class SalidaInventarioDto
    {
        public Guid Id { get; set; }
        public Guid ProductoId { get; set; }
        public int Cantidad { get; set; }
        public string Tipo { get; set; }
        public string ModuloOrigen { get; set; }
        public DateTime Fecha { get; set; }
    }

    public class CrearSalidaInventarioDto
    {
        public Guid ProductoId { get; set; }
        public int Cantidad { get; set; }
        public string Tipo { get; set; }  // venta, merma, uso
        public string ModuloOrigen { get; set; }  // POS, Turnos, Manual
    }
}
