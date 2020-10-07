using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Converters.Clothes
{
    /// <summary>
    /// Преобразования модели размера одежды в модель базы данных. Тесты
    /// </summary>
    public class SizeEntityConverterTest
    {
        /// <summary>
        /// Преобразования модели размера одежды в модель базы данных
        /// </summary>
        [Fact]
        public void ToEntity_FromEntity()
        {
            var sizeDomain = SizeData.GetSizeDomain().First();
            var sizeEntityConverter = new SizeEntityConverter();

            var sizeEntity = sizeEntityConverter.ToEntity(sizeDomain);
            var sizeAfterConverter = sizeEntityConverter.FromEntity(sizeEntity);

            Assert.True(sizeDomain.Equals(sizeAfterConverter));
        }
    }
}