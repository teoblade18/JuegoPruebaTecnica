using JuegoPruebaTecnica.Controllers;
using JuegoPruebaTecnica.Models;
using JuegoPruebaTecnica.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuración de la base de datos
builder.Services.AddDbContext<LocaldbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro de controladores como servicios
builder.Services.AddScoped<PartidaController>();
builder.Services.AddScoped<JugadorController>();

// Configurar servicios adicionales
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReglasCors",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

var app = builder.Build();

// Configuración del middleware para desarrollo
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("swagger/v1/swagger.json", "piedra-papel-tijera Swagger");
    options.RoutePrefix = String.Empty;
});

app.UseHttpsRedirection();
app.UseCors("ReglasCors");
app.UseAuthorization();
app.MapControllers();
app.Run();
