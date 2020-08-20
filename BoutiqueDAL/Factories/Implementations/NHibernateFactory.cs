using System;
using System.IO;
using BoutiqueDAL.Factories.Interfaces;
using BoutiqueDAL.Mappings.Clothes;
using BoutiqueDAL.Models.Implementations.Connection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Functional.FunctionalExtensions.Sync.ResultExtension;
using Functional.Models.Interfaces.Result;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace BoutiqueDAL.Factories.Implementations
{
    /// <summary>
    /// Фабрика для создания сессии подключения к БД
    /// </summary>
    public class NHibernateFactory : IDatabaseFactory
    {
        /// <summary>
        /// Параметры для подключения базы
        /// </summary>
        private readonly IResultValue<FluentConfiguration> _fluentConfiguration;

        public NHibernateFactory(IResultValue<FluentConfiguration> fluentConfiguration)
        {
            _fluentConfiguration = fluentConfiguration;
        }

        /// <summary>
        /// Фабрика для создания сессии
        /// </summary>
        private IResultValue<ISessionFactory>? _sessionFactory;

        /// <summary>
        /// Получить фабрику для создания сессии
        /// </summary>
        public IResultValue<ISessionFactory> SessionFactory =>
            _sessionFactory ??=
                _fluentConfiguration.ResultValueOk(configuration => configuration.BuildSessionFactory());

        /// <summary>
        /// Параметры для подключения базы данных postgres
        /// </summary>
        public static IResultValue<FluentConfiguration> PostgresConfiguration(IResultValue<DatabaseConnection> databaseConnection) =>
            databaseConnection.ResultValueOk(PostgresConfiguration);

        /// <summary>
        /// Параметры для подключения базы данных postgres
        /// </summary>
        public static FluentConfiguration PostgresConfiguration(DatabaseConnection databaseConnection) =>
            Fluently.Configure().
            Database(PostgreSQLConfiguration.Standard.
                     ConnectionString(c => c.Host(databaseConnection.Host).
                                             Port(databaseConnection.Port).
                                             Database(databaseConnection.Database).
                                             Username(databaseConnection.Username).
                                             Password(databaseConnection.Password))).
            Mappings(m => m.FluentMappings.AddFromAssemblyOf<GenderMap>()).
            ExposeConfiguration(c =>
            {
                var schema = new SchemaUpdate(c);
                schema.Execute(false, true);
            });

        /// <summary>
        /// Параметры для подключения базы данных sqlite в памяти
        /// </summary>
        public static FluentConfiguration SqliteMemoryConfiguration() =>
            Fluently.Configure().
                Database(SQLiteConfiguration.Standard.InMemory()).
                Mappings(m => m.FluentMappings.AddFromAssemblyOf<GenderMap>()).
                ExposeConfiguration(c =>
                {
                    var schema = new SchemaUpdate(c);
                    schema.Execute(false, true);
                });
    }
}