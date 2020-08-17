﻿using System;
using System.IO;
using System.Text.RegularExpressions;
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
            services.AddSingleton<IDatabaseFactory>(serviceProvider => NHibernateFactory.SessionFactory(PostgresConnectionFactory.PostgresConfiguration));
            services.AddTransient<IUnitOfWork>(serviceProvider => new UnitOfWork(serviceProvider.GetService<ISessionFactory>()));
            services.AddTransient<IClothesUploadService>(serviceProvider => new ClothesUploadService(serviceProvider.GetService<IUnitOfWork>));

            return services;
        }
    }
}
