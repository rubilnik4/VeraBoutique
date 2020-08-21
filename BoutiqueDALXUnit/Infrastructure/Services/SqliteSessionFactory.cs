using System;
using System.Data.SQLite;
using System.IO;
using System.Reflection;
using BoutiqueDAL.Configuration.Clothes;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace BoutiqueDALXUnit.Infrastructure.Services
{
    public class DatabaseScope : IDisposable
    {
        //private static Assembly _prototypeAssembly;
        //private const string PrototypeConnectionString = "FullUri=file:prototype.db?mode=memory&cache=shared";
        //private static ISessionFactory _prototypeSessionFactory;
        //private static SQLiteConnection _prototypeConnection;

        private const string InstanceConnectionString = "FullUri=file:instance.db?mode=memory&cache=shared";
        private ISessionFactory _instanceSessionFactory;
        private SQLiteConnection _instanceConnection;

        public DatabaseScope()
        {
            //InitDatabasePrototype(assembly);
            InitDatabaseInstance();
        }

        //private void InitDatabasePrototype(Assembly assembly)
        //{
        //    if (_prototypeAssembly == assembly) return;

        //    if (_prototypeConnection != null)
        //    {
        //        _prototypeConnection.Close();
        //        _prototypeConnection.Dispose();
        //        _prototypeSessionFactory.Dispose();
        //    }

        //    _prototypeAssembly = assembly;

        //    _prototypeConnection = new SQLiteConnection(PrototypeConnectionString);
        //    _prototypeConnection.Open();

        //    _prototypeSessionFactory = Fluently
        //        .Configure()
        //        .Database(SQLiteConfiguration.Standard.ConnectionString(PrototypeConnectionString))
        //        .Mappings(m => m.HbmMappings.AddFromAssembly(assembly))
        //        .ExposeConfiguration(cfg => new SchemaExport(cfg).Execute(false, true, false, _prototypeConnection, null))
        //        .BuildSessionFactory();
        //}

        private void InitDatabaseInstance()
        {
            _instanceSessionFactory = Fluently
                .Configure()
                .Database(SQLiteConfiguration.Standard.ConnectionString(InstanceConnectionString))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<GenderConfiguration>())
                .BuildSessionFactory();

            _instanceConnection = new SQLiteConnection(InstanceConnectionString);
            _instanceConnection.Open();
        }

        public ISession OpenSession()
        {
            return _instanceSessionFactory.OpenSession(_instanceConnection);
        }

        public void Dispose()
        {
            _instanceConnection.Close();
            _instanceConnection.Dispose();
            _instanceSessionFactory.Dispose();
        }
    }
}