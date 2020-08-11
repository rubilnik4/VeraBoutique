using System;
using System.IO;
using System.Text.RegularExpressions;
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
        public static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(serviceProvider => NHibernateFactoryManager.SessionFactory(GetPostgresConfiguration()));
            services.AddTransient<IUnitOfWork>(serviceProvider => new UnitOfWork(serviceProvider.GetService<ISessionFactory>()));
            services.AddTransient<IClothesUploadService>(serviceProvider => new ClothesUploadService(serviceProvider.GetService<IUnitOfWork>));

            return services;
        }

        /// <summary>
        /// Получить параметры подключения к базе из переменных окружения
        /// </summary>
        private static ConnectionConfiguration GetPostgresConfiguration()
        {
            string host = Environment.GetEnvironmentVariable("POSTGRES_HOST") ?? throw new ArgumentNullException(nameof(host));

            string portToParse = Environment.GetEnvironmentVariable("POSTGRES_PORT") ?? throw new ArgumentNullException(nameof(portToParse));
            if (!Int32.TryParse(portToParse, out int port)) throw new FormatException(nameof(port));

            string database = Environment.GetEnvironmentVariable("POSTGRES_DB") ?? throw new ArgumentNullException(nameof(database));
            string username = Environment.GetEnvironmentVariable("POSTGRES_USER") ?? throw new ArgumentNullException(nameof(username));
            string password = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD") ?? throw new ArgumentNullException(nameof(password));

            return new ConnectionConfiguration(host, port, database, username, password);
        }
    }
}
