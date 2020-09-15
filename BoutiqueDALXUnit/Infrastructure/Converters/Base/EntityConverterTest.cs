using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDALXUnit.Data;
using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueDALXUnit.Data.Services.Implementation;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Converters.Base
{
    /// <summary>
    /// Базовый конвертер из доменной модели в модель базы данных. Тесты
    /// </summary>
    public class EntityConverterTest
    {
        /// <summary>
        /// Преобразования модели в модель базы данных
        /// </summary>
        [Fact]
        public void ToEntities_FromEntities()
        {
            var testDomains = TestData.GetTestDomains();
            var testEntityConverter = new TestEntityConverter();

            var testEntities = testEntityConverter.ToEntities(testDomains);
            var testDomainsAfterConverter = testEntityConverter.FromEntities(testEntities);

            Assert.True(testDomains.SequenceEqual(testDomainsAfterConverter));
        }
    }
}