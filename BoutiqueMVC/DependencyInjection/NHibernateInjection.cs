using Antlr.Runtime.Misc;
using BoutiqueDAL.Factories.Implementations;
using BoutiqueDAL.Factories.Interfaces;
using BoutiqueDAL.Services.Implementations;
using BoutiqueDAL.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;

namespace BoutiqueMVC.DependencyInjection
{
    /// <summary>
    /// Регистрация зависимостей для подключения NHibernate
    /// </summary>
    public static class NHibernateInjection
    {
        /// <summary>
        /// Добавить зависимости NHibernate к сервисам
        /// </summary>
        public static IServiceCollection ConfigureServices(IServiceCollection services, string connvectionString)
        {
            services.AddSingleton<ISessionFactory>(serviceProvider => NHibernateFactoryManager.SessionFactory(connvectionString));
            services.AddTransient<IUnitOfWork>(serviceProvider =>
            {
                var session = serviceProvider.GetService<ISessionFactory>();
                return new UnitOfWork(session);
            });

            services.AddTransient<IClothesUploadService>(serviceProvider => new ClothesUploadService(serviceProvider.GetService<IUnitOfWork>));

            return services;
        }
    }
}
