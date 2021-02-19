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
    public class SizeGroupEntityConverterTest
    {
        /// <summary>
        /// Преобразования модели группы размера одежды в модель базы данных
        /// </summary>
        [Fact]
        public void ToEntityShort_FromEntityShort()
        {
            var sizeGroupDomain = SizeGroupData.SizeGroupDomains.First();
            var sizeGroupEntityConverter = new SizeGroupEntityConverter();

            var sizeGroupEntity = sizeGroupEntityConverter.ToEntity(sizeGroupDomain);
            var sizeGroupAfterConverter = sizeGroupEntityConverter.FromEntity(sizeGroupEntity);

            Assert.True(sizeGroupAfterConverter.OkStatus);
            Assert.True(sizeGroupDomain.Equals(sizeGroupAfterConverter.Value));
        }
    }
}