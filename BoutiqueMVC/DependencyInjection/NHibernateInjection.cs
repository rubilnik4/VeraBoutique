using System;
using System.IO;
using System.Text.RegularExpressions;
using BoutiqueDAL.Factories.Implementations;
using BoutiqueDAL.Factories.Interfaces;
using BoutiqueDAL.Infrastructure.Implementations.Services;
using BoutiqueDAL.Infrastructure.Interfaces.Services;
using Functional.FunctionalExtensions.Sync.ResultExtension;
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
        public static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDatabaseFactory>(serviceProvider => new NHibernateFactory(PostgresConnectionFactory.PostgresConfiguration));
            services.AddTransient<IUnitOfWork>(serviceProvider => new UnitOfWork(serviceProvider.GetService<IDatabaseFactory>().SessionFactory.
                                                                                 ResultValueOk(sessionFactory => sessionFactory.OpenSession())));
            services.AddTransient<IClothesService>(serviceProvider => new ClothesService(serviceProvider.GetService<IUnitOfWork>));

            return services;
        }
    }
}
