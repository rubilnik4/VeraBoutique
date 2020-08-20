using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Entities.Clothes;
using BoutiqueDAL.Mappings;
using BoutiqueDAL.Mappings.Clothes;
using BoutiqueDAL.Models.Implementations.Connection;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Testing;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Xunit;

namespace BoutiqueDALXUnit.Mappings.Clothes
{
    /// <summary>
    /// Пол. Схема базы данных. Тест
    /// </summary>
    public class GenderMapTest
    {
        [Fact]
        public void GenderMap_Correctly()
        {
            new PersistenceSpecification<TestEntity>(SqliteSession)
              // .CheckProperty(gender => gender.Id, 1)
              // .CheckProperty(gender => gender.GenderType, GenderType.Male)
               .CheckProperty(test => test.Name, "Мужик")
               .VerifyTheMappings();
        }

        private static ISession SqliteSession =>
            Fluently.Configure().
            Database(SQLiteConfiguration.Standard.
                                         InMemory).
            Mappings(m => m.FluentMappings.AddFromAssemblyOf<TestMap>()).
            ExposeConfiguration(c =>
            {
                var schema = new SchemaUpdate(c);
                schema.Execute(false, true);
            }).
            BuildSessionFactory().
            OpenSession();
    }
}