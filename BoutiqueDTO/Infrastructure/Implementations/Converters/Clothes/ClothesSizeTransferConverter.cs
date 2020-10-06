using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes
{
    /// <summary>
    /// Конвертер размеров одежды в трансферную модель
    /// </summary>
    public class ClothesSizeTransferConverter : TransferConverter<string, IClothesSizeDomain, ClothesSizeTransfer>,
                                                IClothesSizeTransferConverter
    {
        /// <summary>
        /// Преобразовать размеры одежды в трансферную модель
        /// </summary>
        public override ClothesSizeTransfer ToTransfer(IClothesSizeDomain clothesSizeDomain) =>
            new ClothesSizeTransfer(clothesSizeDomain.SizeType, clothesSizeDomain.SizeValue, clothesSizeDomain.SizeName);

        /// <summary>
        /// Преобразовать размеры одежды из трансферной модели
        /// </summary>
        public override IClothesSizeDomain FromTransfer(ClothesSizeTransfer clothesSizeTransfer) =>
            new ClothesSizeDomain(clothesSizeTransfer.SizeType, clothesSizeTransfer.SizeValue, clothesSizeTransfer.SizeName);
    }
}