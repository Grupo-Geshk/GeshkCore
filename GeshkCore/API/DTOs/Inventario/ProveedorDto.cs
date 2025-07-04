﻿namespace GeshkCore.API.DTOs.Inventario
{
    public class ProveedorDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public string? Direccion { get; set; }
    }

    public class CrearProveedorDto
    {
        public string Nombre { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public string? Direccion { get; set; }
    }
}
