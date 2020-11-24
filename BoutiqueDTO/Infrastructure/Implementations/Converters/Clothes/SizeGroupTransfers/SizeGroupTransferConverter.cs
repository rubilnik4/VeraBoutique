using BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroup;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.SizeGroupTransfers
{
    /// <summary>
    /// Конвертер группы размеров одежды в трансферную модель
    /// </summary>
    public class SizeGroupTransferConverter : TransferConverter<(ClothesSizeType, int), ISizeGroupDomain, SizeGroupTransfer>,
                                              ISizeGroupTransferConverter
    {
        public SizeGroupTransferConverter(ISizeTransferConverter sizeTransferConverter)
        {
            _sizeTransferConverter = sizeTransferConverter;
        }

        /// <summary>
        /// Конвертер размеров одежды в трансферную модель
        /// </summary>
        private readonly ISizeTransferConverter _sizeTransferConverter;

        /// <summary>
        /// Преобразовать группу размеров одежды в трансферную модель
        /// </summary>
        public override SizeGroupTransfer ToTransfer(ISizeGroupDomain sizeGroupDomain) =>
            new SizeGroupTransfer(sizeGroupDomain,
                                  _sizeTransferConverter.ToTransfers(sizeGroupDomain.Sizes));

        /// <summary>
        /// Преобразовать размеры одежды из трансферной модели
        /// </summary>
        public override ISizeGroupDomain FromTransfer(SizeGroupTransfer sizeGroupTransfer) =>
            new SizeGroupDomain(sizeGroupTransfer,
                                _sizeTransferConverter.FromTransfers(sizeGroupTransfer.Sizes));
    }
}