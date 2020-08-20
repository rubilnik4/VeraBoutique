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
            var genderService = new GenderService(GetUnitOfWork);

            await genderService.UploadGenders(genders);
            var gendersFromDatabase = await genderService.GetGenders();

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