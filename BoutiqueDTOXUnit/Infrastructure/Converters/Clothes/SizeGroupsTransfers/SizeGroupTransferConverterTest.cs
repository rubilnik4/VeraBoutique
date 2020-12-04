using System.Linq;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroup;
using BoutiqueDTOXUnit.Data.Services.Mocks.Converters;
using BoutiqueDTOXUnit.Data.Transfers;
using Functional.Models.Enums;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Converters.Clothes.SizeGroupsTransfers
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
        public void SizeGroup_ToTransfer_FromTransfer()
        {
            var sizeGroup = SizeGroupData.SizeGroupDomains.First();
            var sizeGroupTransferConverter = SizeGroupTransferConverterMock.SizeGroupTransferConverter;

            var sizeGroupTransfer = sizeGroupTransferConverter.ToTransfer(sizeGroup);
            var sizeGroupAfterConverter = sizeGroupTransferConverter.FromTransfer(sizeGroupTransfer);

            Assert.True(sizeGroupAfterConverter.OkStatus);
            Assert.True(sizeGroup.Equals(sizeGroupAfterConverter.Value));
        }

        /// <summary>
        /// Преобразования модели размеров одежды в трансферную модель. Ошибка размера
        /// </summary>
        [Fact]
        public void SizeGroup_ToTransfer_SizeError()
        {
            var sizeGroup = SizeGroupTransfersData.SizeGroupTransfers.First();
            sizeGroup.Sizes = null!;
            var sizeGroupTransferConverter = SizeGroupTransferConverterMock.SizeGroupTransferConverter;

            var sizeGroupAfterConverter = sizeGroupTransferConverter.FromTransfer(sizeGroup);

            Assert.True(sizeGroupAfterConverter.HasErrors);
            Assert.True(sizeGroupAfterConverter.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }

        /// <summary>
        /// Преобразования модели размеров одежды в трансферную модель. Ошибка размера
        /// </summary>
        [Fact]
        public void SizeGroup_ToTransfer_SizeCollectionError()
        {
            var sizeGroup = SizeGroupTransfersData.SizeGroupTransfers.First();
            sizeGroup.Sizes = sizeGroup.Sizes.Append(null).ToList();
            var sizeGroupTransferConverter = SizeGroupTransferConverterMock.SizeGroupTransferConverter;

            var sizeGroupAfterConverter = sizeGroupTransferConverter.FromTransfer(sizeGroup);

            Assert.True(sizeGroupAfterConverter.HasErrors);
            Assert.True(sizeGroupAfterConverter.Errors.First().ErrorResultType == ErrorResultType.ValueNotFound);
        }
    }
}