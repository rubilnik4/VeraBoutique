using NSwag.Generation;
using NSwag.Generation.AspNetCore;

namespace BoutiqueMVC.Factories.Swagger
{
    /// <summary>
    /// Параметры swagger
    /// </summary>
    public static class SwaggerConfiguration
    {
        /// <summary>
        /// Установить параметры swagger
        /// </summary>
        public static void ConfigSwagger(AspNetCoreOpenApiDocumentGeneratorSettings config) =>
            config.PostProcess = document =>
            {
                document.Info.Version = "v1";
                document.Info.Title = "Boutique API";
                document.Info.Description = "Clothes Web Api for VeraBoutique store";
                document.Info.Contact = new NSwag.OpenApiContact
                {
                    Name = "Ivan Rubilnik",
                    Email = "rubilnik4@yandex.ru",
                    Url = "https://github.com/rubilnik4/VeraBoutique"
                };
            };
        
    }
}