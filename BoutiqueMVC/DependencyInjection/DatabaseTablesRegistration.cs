using BoutiqueDAL.Infrastructure.Implementations.Services.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Clothes;
using Microsoft.Extensions.DependencyInjection;

namespace BoutiqueMVC.DependencyInjection
{
    /// <summary>
    /// Регистрация таблиц для баз данных
    /// </summary>
    public static class DatabaseTablesRegistration
    {
        /// <summary>
        /// Внедрить зависимости к таблицам базы данных
        /// </summary>
        public static void RegisterDatabaseTables(IServiceCollection services)
        {
            services.AddTransient(service => service.GetService<IBoutiqueDatabase>()!.CategoryTable);
            services.AddTransient(service => service.GetService<IBoutiqueDatabase>()!.ClothesTable);
            services.AddTransient(service => service.GetService<IBoutiqueDatabase>()!.ClothesImageTable);
            services.AddTransient(service => service.GetService<IBoutiqueDatabase>()!.ClotheTypeTable);
            services.AddTransient(service => service.GetService<IBoutiqueDatabase>()!.ColorClothesTable);
            services.AddTransient(service => service.GetService<IBoutiqueDatabase>()!.GendersTable);
            services.AddTransient(service => service.GetService<IBoutiqueDatabase>()!.SizeTable);
            services.AddTransient(service => service.GetService<IBoutiqueDatabase>()!.SizeGroupTable);
        }
    }
}