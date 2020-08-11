using System;
using System.IO;
using BoutiqueDAL.Entities.Clothes;
using BoutiqueDAL.Mappings.Clothes;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace BoutiqueDAL.Factories.Implementations
{
    /// <summary>
    /// Фабрика для создания сессии подключения к БД
    /// </summary>
    public static class NHibernateFactoryManager
    {
        /// <summary>
        /// Фабрика для создания сессии
        /// </summary>
        private static ISessionFactory? _sessionFactory;

        /// <summary>
        /// Получить фабрику для создания сессии
        /// </summary>
        public static ISessionFactory SessionFactory(ConnectionConfiguration connectionConfiguration) =>
            _sessionFactory ??= PostgresConfigurationFactory(connectionConfiguration).BuildSessionFactory();

        /// <summary>
        /// Параметры для подключения базы данных SqLite
        /// </summary>
        private static FluentConfiguration PostgresConfigurationFactory(ConnectionConfiguration connectionConfiguration) =>
            Fluently.Configure().
            Database(PostgreSQLConfiguration.Standard.
                     ConnectionString(c => c.Host(connectionConfiguration.Host).
                                             Port(connectionConfiguration.Port).
                                             Database(connectionConfiguration.Database).
                                             Username(connectionConfiguration.Username).
                                             Password(connectionConfiguration.Password))).
            Mappings(m => m.FluentMappings.AddFromAssemblyOf<SexMap>()).
            ExposeConfiguration(c =>
            {
                var schema = new SchemaUpdate(c);
                schema.Execute(false, true);
            });
    }
}