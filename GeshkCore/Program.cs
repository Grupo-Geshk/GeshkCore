using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GeshkCore.Infrastructure.DbContext; // Asegúrate de ajustar al namespace correcto

var builder = WebApplication.CreateBuilder(args);

// Cargar .env
Env.Load();

var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET");
var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");

// Configurar DbContext con PostgreSQL
builder.Services.AddDbContext<GeshkDbContext>(options =>
    options.UseNpgsql(connectionString));

// Configurar JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret!))
        };
    });

// Servicios adicionales
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // ?? Antes de Authorization
app.UseAuthorization();

app.MapControllers();

app.Run();
