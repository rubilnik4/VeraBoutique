using System;
using System.IO;
using BoutiqueDAL.Factories.Interfaces;
using BoutiqueDAL.Mappings.Clothes;
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
    public class NHibernateFactory: IDatabaseFactory
    {
        /// <summary>
        /// Параметры подключения к базе данных
        /// </summary>
        private readonly IResultValue<DatabaseConnection> _databaseConnection;

        public NHibernateFactory(IResultValue<DatabaseConnection> databaseConnection)
        {
            _databaseConnection = databaseConnection;
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
                _databaseConnection.
                ResultValueOk(connection => PostgresConfigurationFactory(connection).BuildSessionFactory());

        /// <summary>
        /// Параметры для подключения базы данных SqLite
        /// </summary>
        private static FluentConfiguration PostgresConfigurationFactory(DatabaseConnection connectionConfiguration) =>
            Fluently.Configure().
            Database(PostgreSQLConfiguration.Standard.
                     ConnectionString(c => c.Host(connectionConfiguration.Host).
                                             Port(connectionConfiguration.Port).
                                             Database(connectionConfiguration.Database).
                                             Username(connectionConfiguration.Username).
                                             Password(connectionConfiguration.Password))).
            Mappings(m => m.FluentMappings.AddFromAssemblyOf<GenderMap>()).
            ExposeConfiguration(c =>
            {
                var schema = new SchemaUpdate(c);
                schema.Execute(false, true);
            });
    }
}