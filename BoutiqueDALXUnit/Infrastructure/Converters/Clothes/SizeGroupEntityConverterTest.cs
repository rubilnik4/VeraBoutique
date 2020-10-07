using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Converters.Clothes
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
        public void ToEntity_FromEntity()
        {
            var sizeGroupDomain = SizeGroupData.GetSizeGroupDomain().First();
            var sizeEntityConverter = new SizeEntityConverter();
            var sizeGroupEntityConverter = new SizeGroupEntityConverter(sizeEntityConverter);

            var sizeGroupEntity = sizeGroupEntityConverter.ToEntity(sizeGroupDomain);
            var sizeGroupAfterConverter = sizeGroupEntityConverter.FromEntity(sizeGroupEntity);

            Assert.True(sizeGroupDomain.Equals(sizeGroupAfterConverter));
        }
    }
}