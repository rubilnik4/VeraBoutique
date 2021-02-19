using System.Linq;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Clothes;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Converters.Clothes.SizeGroupsTransfers
{
    public class SizeGroupTransferConverterTest
    {
        /// <summary>
        /// Преобразования модели базовых данных размеров одежды в трансферную модель
        /// </summary>
        [Fact]
        public void SizeGroup_ToTransfer_FromTransfer()
        {
            var sizeGroup = SizeGroupData.SizeGroupDomains.First();
            var sizeGroupTransferConverter = SizeGroupTransferConverterMock.SizeGroupTransferConverter;

            var sizeGroupTransfer = sizeGroupTransferConverter.ToTransfer(sizeGroup);
            var sizeGroupAfterConverter = sizeGroupTransferConverter.FromTransfer(sizeGroupTransfer);

            Assert.True(sizeGroupAfterConverter.OkStatus);
            Assert.True(sizeGroup.Equals(sizeGroupAfterConverter.Value));
        }

    }
}