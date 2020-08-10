using System;
using System.IO;
using BoutiqueDAL.Entities.Clothes;
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
        public static ISessionFactory SessionFactory
        {
            get
            {
                try
                {
                    var session = PostgresConfigurationFactory().BuildSessionFactory();
                    return session;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
               
            }
        }
           

        /// <summary>
        /// Параметры для подключения базы данных SqLite
        /// </summary>
        private static FluentConfiguration PostgresConfigurationFactory()
        {
            var session = Fluently.Configure().
            Database(PostgreSQLConfiguration.Standard.ConnectionString(c => c.Host("localhost").
                                                                              Port(5434).
                                                                              Database("postgres").
                                                                              Username("postgres").
                                                                              Password("postgres")));

            return session;
        }
            
    }
}