using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDALXUnit.Data;
using System.Linq;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Converters.Base
{
    /// <summary>
    /// Базовый конвертер из доменной модели в модель базы данных. Тесты
    /// </summary>
    public class EntityConverterTest
    {
        /// <summary>
        /// Преобразования модели типа пола и модель базы данных
        /// </summary>
        [Fact]
        public void ToEntities_FromEntities()
        {
            var genders = EntityData.GetGendersDomain();
            var genderEntityConverter = new GenderEntityConverter();

            var genderEntities = genderEntityConverter.ToEntities(genders);
            var gendersAfterConverter = genderEntityConverter.FromEntities(genderEntities);

            Assert.True(genders.SequenceEqual(gendersAfterConverter));
        }
    }
}