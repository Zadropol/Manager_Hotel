using FluentValidation;
using Hotel_Manager.Core.Interfaces;
using Hotel_Manager.Infrastructure.Data;
using Hotel_Manager.Infrastructure.Filters;
using Hotel_Manager.Infrastructure.Mappings;
using Hotel_Manager.Infrastructure.Repositories;
using Hotel_Manager.Core.Services;
using Hotel_Manager.Infrastructure.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// --------------------
// ?? Configuración MVC + Filtros globales
// --------------------
builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
    options.Filters.Add<ValidationFilter>();
});

// --------------------
// ?? Swagger + Versionamiento
// --------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});


// --------------------
// ?? Conexión MySQL
// --------------------
var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");
builder.Services.AddDbContext<Hotel_ManagerDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// --------------------
// ?? AutoMapper + FluentValidation
// --------------------
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddValidatorsFromAssemblyContaining<BookingDTOvalidator>();

// --------------------
// ?? Dependencias principales
// --------------------
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<BookingService>();

var app = builder.Build();

// --------------------
// ?? Configurar Swagger (UI + Metadata)
// --------------------
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Hotel Manager API v1");
    options.DocumentTitle = "Hotel Manager API";
    options.RoutePrefix = string.Empty; // Permite abrir Swagger directamente desde raíz
});

// --------------------
// ?? Ejecución final
// --------------------
app.UseHttpsRedirection();
app.MapControllers();
app.Run();