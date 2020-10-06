using System.Linq;
using BoutiqueCommonXUnit.Data;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Converters.Clothes
{
    /// <summary>
    /// Конвертер группы размеров одежды в трансферную модель. Тесты
    /// </summary>
    public class SizeGroupTransferConverterTest
    {
        /// <summary>
        /// Преобразования модели размеров одежды в трансферную модель
        /// </summary>
        [Fact]
        public void ToTransfer_FromTransfer()
        {
            var clothesSizeGroup = ClothesSizeGroupData.GetClothesSizeGroupDomain().First();
            var clothesSizeGroupTransferConverter = new SizeGroupTransferConverter();

            var clothesSizeGroupTransfer = clothesSizeGroupTransferConverter.ToTransfer(clothesSizeGroup);
            var clothesSizeGroupAfterConverter = clothesSizeGroupTransferConverter.FromTransfer(clothesSizeGroupTransfer);

            Assert.True(clothesSizeGroup.Equals(clothesSizeGroupAfterConverter));
        }
    }
}