using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDALXUnit.Data;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Converters.Clothes
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
            var genderDomain = GenderData.GendersDomain.First(); 
            var genderEntityConverter = new GenderEntityConverter();

            var genderEntity = genderEntityConverter.ToEntity(genderDomain);
            var genderAfterConverter = genderEntityConverter.FromEntity(genderEntity);

            Assert.True(genderAfterConverter.OkStatus);
            Assert.True(genderDomain.Equals(genderAfterConverter.Value));
        }
    }
}