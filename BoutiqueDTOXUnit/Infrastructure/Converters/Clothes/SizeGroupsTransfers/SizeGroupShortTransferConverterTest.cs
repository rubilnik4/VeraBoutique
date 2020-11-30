using System.Linq;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.SizeGroupTransfers;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Converters.Clothes.SizeGroupsTransfers
{
    public class SizeGroupShortTransferConverterTest
    {
        /// <summary>
        /// Преобразования модели базовых данных размеров одежды в трансферную модель
        /// </summary>
        [Fact]
        public void SizeGroupShort_ToTransfer_FromTransfer()
        {
            var sizeGroup = SizeGroupData.SizeGroupDomain.First();
            var sizeGroupShortTransferConverter = new SizeGroupShortTransferConverter();

            var sizeGroupTransfer = sizeGroupShortTransferConverter.ToTransfer(sizeGroup);
            var sizeGroupAfterConverter = sizeGroupShortTransferConverter.FromTransfer(sizeGroupTransfer);

            Assert.True(sizeGroupAfterConverter.OkStatus);
            Assert.True(sizeGroup.Equals(sizeGroupAfterConverter.Value));
        }

    }
}