using System.Linq;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.SizeGroupEntities;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Converters.Clothes.SizeGroupEntities
{
    /// <summary>
    /// Преобразования модели группы размера одежды в модель базы данных. Тесты
    /// </summary>
    public class SizeGroupShortEntityConverterTest
    {
        /// <summary>
        /// Преобразования модели группы размера одежды в модель базы данных
        /// </summary>
        [Fact]
        public void ToEntityShort_FromEntityShort()
        {
            var sizeGroupShortDomain = SizeGroupData.SizeGroupDomain.First();
            var sizeGroupShortEntityConverter = new SizeGroupShortEntityConverter();

            var sizeGroupEntity = sizeGroupShortEntityConverter.ToEntity(sizeGroupShortDomain);
            var sizeGroupAfterConverter = sizeGroupShortEntityConverter.FromEntity(sizeGroupEntity);

            Assert.True(sizeGroupAfterConverter.OkStatus);
            Assert.True(sizeGroupShortDomain.Equals(sizeGroupAfterConverter.Value));
        }
    }
}