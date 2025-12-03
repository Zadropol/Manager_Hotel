using FluentValidation;
using Hotel_Manager.Core.Interfaces;
using Hotel_Manager.Core.Services;
using Hotel_Manager.Infrastructure.Data;
using Hotel_Manager.Infrastructure.Filters;
using Hotel_Manager.Infrastructure.Mappings;
using Hotel_Manager.Infrastructure.Repositories;
using Hotel_Manager.Infrastructure.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

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
    // Muestra versiones soportadas en headers
    options.ReportApiVersions = true;

    // Usa versión por defecto cuando no se especifica
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);

    // Tipos de versionamiento
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),                 // /api/v1/
        new HeaderApiVersionReader("x-api-version"),      // header: x-api-version: 1.0
        new QueryStringApiVersionReader("api-version")    // ?api-version=1.0
    );
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
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

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });


app.UseAuthentication();
app.UseAuthorization();

// --------------------
// ?? Ejecución final
// --------------------
app.UseHttpsRedirection();
app.MapControllers();
app.Run();