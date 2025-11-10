using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Hotel_Manager.API.Swagger
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var desc in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(desc.GroupName, new OpenApiInfo
                {
                    Version = desc.ApiVersion.ToString(),
                    Title = "Hotel Manager API",
                    Description = "API para la gestión de reservas, habitaciones y huéspedes de un hotel.",
                    Contact = new OpenApiContact
                    {
                        Name = "Equipo de Desarrollo - Proyecto TecJairo",
                        Email = "soporte@hotelmanager.com",
                        Url = new Uri("https://github.com/Zadropol/Manager_Hotel")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Uso académico - Universidad Católica Boliviana"
                    }
                });
            }
        }
}
