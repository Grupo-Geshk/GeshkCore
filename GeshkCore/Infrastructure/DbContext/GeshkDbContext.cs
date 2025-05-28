using Microsoft.EntityFrameworkCore;
using GeshkCore.Domain.Entities;
using GeshkCore.Domain.Entities.POS;
using GeshkCore.Domain.Entities.Turnos;
using GeshkCore.Domain.Entities.Inventario;
using GeshkCore.Domain.Entities.Empleados;
using GeshkCore.Domain.Entities.Catalogo;
using GeshkCore.Domain.Entities.Encuestas;
using GeshkCore.Domain.Entities.GeshkAdmin;

namespace GeshkCore.Infrastructure.DbContext
{
    public class GeshkDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public GeshkDbContext(DbContextOptions<GeshkDbContext> options)
            : base(options)
        {
        }

        // --- Base
        public DbSet<Client> Clients { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<LogAcceso> LogsAcceso { get; set; }

        // --- POS
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<ItemOrden> ItemsOrden { get; set; }
        public DbSet<MetodoPago> MetodosPago { get; set; }
        public DbSet<ComprobantePago> ComprobantesPago { get; set; }
        public DbSet<ProductoPOS> ProductosPOS { get; set; }
        public DbSet<Mesa> Mesas { get; set; }
        public DbSet<CierreCaja> CierresCaja { get; set; }

        // --- Turnos / Citas
        public DbSet<Cita> Citas { get; set; }
        public DbSet<Formulario> Formularios { get; set; }
        public DbSet<CampoPersonalizado> CamposPersonalizados { get; set; }
        public DbSet<ConfiguracionAgenda> ConfiguracionesAgenda { get; set; }
        public DbSet<HistorialCita> HistorialCitas { get; set; }

        // --- Inventario
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<EntradaInventario> EntradasInventario { get; set; }
        public DbSet<SalidaInventario> SalidasInventario { get; set; }
        public DbSet<AlertaStock> AlertasStock { get; set; }

        // --- Empleados
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Asistencia> Asistencias { get; set; }
        public DbSet<HorarioSemanal> HorariosSemanales { get; set; }
        public DbSet<HistorialEmpleado> HistorialEmpleados { get; set; }
        public DbSet<Disponibilidad> Disponibilidades { get; set; }
        public DbSet<GrupoTrabajo> GruposTrabajo { get; set; }

        // --- Catálogo Público
        public DbSet<ProductoCatalogo> ProductosCatalogo { get; set; }
        public DbSet<Promocion> Promociones { get; set; }
        public DbSet<Publicacion> Publicaciones { get; set; }
        public DbSet<ConfiguracionCatalogo> ConfiguracionesCatalogo { get; set; }

        // --- Encuestas / Feedback
        public DbSet<Encuesta> Encuestas { get; set; }
        public DbSet<Pregunta> Preguntas { get; set; }
        public DbSet<Respuesta> Respuestas { get; set; }
        public DbSet<ConfiguracionEncuesta> ConfiguracionesEncuesta { get; set; }

        // --- Panel GESHK
        public DbSet<LogModulo> LogsModulo { get; set; }
        public DbSet<ConsumoRecurso> ConsumoRecursos { get; set; }
        public DbSet<HistorialCuenta> HistorialCuentas { get; set; }
        public DbSet<NotaInterna> NotasInternas { get; set; }
        public DbSet<PlanModule> PlanModules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Aquí puedes personalizar relaciones, restricciones, etc.
        }
    }
}
