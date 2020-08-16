using System;
using System.IO;
using System.Text.RegularExpressions;
using Antlr.Runtime.Misc;
using BoutiqueCommon.Models.Implementation.Connection;
using BoutiqueDAL.Factories.Implementations;
using BoutiqueDAL.Factories.Interfaces;
using BoutiqueDAL.Services.Implementations;
using BoutiqueDAL.Services.Interfaces;
using FluentNHibernate.Utils;
using Functional.FunctionalExtensions;
using Functional.FunctionalExtensions.ResultExtension;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
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
            services.AddSingleton(serviceProvider => NHibernateFactoryManager.SessionFactory(PostgresConfiguration.GetPostgresConfiguration()));
            services.AddTransient<IUnitOfWork>(serviceProvider => new UnitOfWork(serviceProvider.GetService<ISessionFactory>()));
            services.AddTransient<IClothesUploadService>(serviceProvider => new ClothesUploadService(serviceProvider.GetService<IUnitOfWork>));

            return services;
        }
    }
}
