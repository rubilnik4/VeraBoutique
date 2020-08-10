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
        /// Фабрика для создания сессии
        /// </summary>
        public static ISessionFactory SessionFactory(string connectionString)
        {
            try
            {
                var session = PostgresConfigurationFactory(connectionString).BuildSessionFactory();
                return session;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


        }


        /// <summary>
        /// Параметры для подключения базы данных SqLite
        /// </summary>
        private static FluentConfiguration PostgresConfigurationFactory(string connectionString)
        {
            //var session = Fluently.Configure().
            //Database(PostgreSQLConfiguration.Standard.ConnectionString(connectionString)).
            //Mappings(m => m.FluentMappings.AddFromAssemblyOf<SexMap>());

            //return session;
            var session = Fluently.Configure().
            Database(PostgreSQLConfiguration.Standard.ConnectionString(c => c.Host("postgres").
                                                                              Port(5432).
                                                                              Database("postgres").
                                                                              Username("postgres").
                                                                              Password("postgres"))).
            Mappings(m => m.FluentMappings.AddFromAssemblyOf<SexMap>()).
            ExposeConfiguration(c =>
            {
                var schema = new SchemaUpdate(c);
                schema.Execute(false, true);
            });
            return session;
        }

    }
}