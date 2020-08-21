using BoutiqueDAL.Factories.Implementations;
using Functional.Models.Implementations.Result;
using System.Threading.Tasks;
using Xunit;
using Functional.FunctionalExtensions.Sync.ResultExtension;
using FluentNHibernate.Cfg;
using BoutiqueDAL.Infrastructure.Implementations.Services;
using BoutiqueDAL.Factories.Interfaces;
using Functional.FunctionalExtensions.Sync;
using System.Collections.Generic;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommon.Models.Implementation.Clothes;
using System.Linq;
using NHibernate;

namespace BoutiqueDALXUnit.Infrastructure.Services
{
    /// <summary>
    /// Сервис загрузки данных в базу для типа пола одежды
    /// </summary>
    public class GenderServiceTest
    {
        [Fact]
        public async Task UploadGenders_EqualReturn()
        {
            var genders = GetGenders();
            var factory = new DatabaseScope();
            var session = new ResultValue<ISession>(factory.OpenSession());
            var genderService = new GenderService(() => new UnitOfWork(session));
            

            await genderService.UploadGenders(genders);
            factory.Dispose();

            var factoryRead = new DatabaseScope();
            var sessionRead = new ResultValue<ISession>(factoryRead.OpenSession());
            var genderServiceRead = new GenderService(() => new UnitOfWork(sessionRead));
            var gendersFromDatabase = await genderServiceRead.GetGenders();
            factoryRead.Dispose();

            Assert.True(gendersFromDatabase.OkStatus);
            Assert.True(gendersFromDatabase.Value.SequenceEqual(genders));
        }

        /// <summary>
        /// Получить класс-обертку для управления транзакциями
        /// </summary>
        private static IUnitOfWork GetUnitOfWork() =>
            new ResultValue<FluentConfiguration>(NHibernateFactory.SqliteMemoryConfiguration()).
            Map(sqlConfiguration => new NHibernateFactory(sqlConfiguration)).
            Map(databaseFactory => databaseFactory.SessionFactory.
                                   ResultValueOk(sessionFactory => sessionFactory.OpenSession())).
            Map(session => new UnitOfWork(session));

        /// <summary>
        /// Получить типы полов
        /// </summary>
        private static IReadOnlyCollection<Gender> GetGenders() =>
            new List<Gender>()
            {
                new Gender(GenderType.Male, "Мужик"),
                new Gender(GenderType.Female, "Тетя"),
            };
    }
}