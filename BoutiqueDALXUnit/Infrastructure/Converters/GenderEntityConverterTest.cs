using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommon.Models.Implementation.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Converters
{
    /// <summary>
    /// Преобразования модели типа пола и модель базы данных. Тесты
    /// </summary>
    public class GenderEntityConverterTest
    {
        /// <summary>
        /// Преобразования модели типа пола и модель базы данных
        /// </summary>
        [Fact]
        public void ToEntity_FromEntity()
        {
            var gender = new Gender(GenderType.Male, "Мужик");

            var genderEntity = GenderEntityConverter.ToEntity(gender);
            var genderAfterConverter = GenderEntityConverter.FromEntity(genderEntity);

            Assert.True(gender.Equals(genderAfterConverter));
        }
    }
}