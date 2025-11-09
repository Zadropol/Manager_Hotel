using FluentValidation;
using Hotel_Manager.Core.Interfaces;
using Hotel_Manager.Infrastructure.Data;
using Hotel_Manager.Infrastructure.Filters;
using Hotel_Manager.Infrastructure.Mappings;
using Hotel_Manager.Infrastructure.Repositories;
using Hotel_Manager.Infrastructure.Services;
using Hotel_Manager.Infrastructure.Validators;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<BookingService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddValidatorsFromAssemblyContaining<BookingDTOvalidator>();


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("ConnectionMySql");
builder.Services.AddDbContext<Hotel_ManagerDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI();
    app.UseSwagger();
}

builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
    options.Filters.Add<ValidationFilter>();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
