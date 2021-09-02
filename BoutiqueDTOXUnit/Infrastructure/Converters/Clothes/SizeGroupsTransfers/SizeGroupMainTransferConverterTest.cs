using System.Linq;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroupTransfers;
using BoutiqueDTOXUnit.Data.Transfers;
using BoutiqueDTOXUnit.Data.Transfers.Clothes;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Clothes;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Interfaces.Errors.CommonErrors;
using Xunit;

namespace BoutiqueDTOXUnit.Infrastructure.Converters.Clothes.SizeGroupsTransfers
{
    /// <summary>
    /// Конвертер группы размеров одежды в трансферную модель. Тесты
    /// </summary>
    public class SizeGroupMainTransferConverterTest
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
        public void SizeGroup_ToTransfer_SizeCollectionError()
        {
            var sizeGroup = SizeGroupTransfersData.SizeGroupMainTransfers.First();
            var sizeGroupNull = new SizeGroupMainTransfer(sizeGroup, sizeGroup.Sizes.Append(null!)!);
            var sizeGroupTransferConverter = SizeGroupTransferConverterMock.SizeGroupMainTransferConverter;

            var sizeGroupAfterConverter = sizeGroupTransferConverter.FromTransfer(sizeGroupNull);

            Assert.True(sizeGroupAfterConverter.HasErrors);
            Assert.IsAssignableFrom<IValueNotFoundErrorResult>(sizeGroupAfterConverter.Errors.First());
        }
    }
}