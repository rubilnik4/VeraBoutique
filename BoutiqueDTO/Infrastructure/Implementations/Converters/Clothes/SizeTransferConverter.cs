using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes
{
    /// <summary>
    /// Конвертер размеров одежды в трансферную модель
    /// </summary>
    public class SizeTransferConverter : TransferConverter<(SizeType, string), ISizeDomain, SizeTransfer>,
                                         ISizeTransferConverter
    {
        /// <summary>
        /// Преобразовать размеры одежды в трансферную модель
        /// </summary>
        public override SizeTransfer ToTransfer(ISizeDomain sizeDomain) =>
            new SizeTransfer(sizeDomain.SizeType,  sizeDomain.SizeName);

        /// <summary>
        /// Преобразовать размеры одежды из трансферной модели
        /// </summary>
        public override ISizeDomain FromTransfer(SizeTransfer sizeTransfer) =>
            new SizeDomain(sizeTransfer.SizeType, sizeTransfer.SizeName);
    }
}