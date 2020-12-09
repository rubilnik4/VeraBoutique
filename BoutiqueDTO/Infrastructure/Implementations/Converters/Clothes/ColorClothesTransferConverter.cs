using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using Functional.FunctionalExtensions.Async;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes
{
    /// <summary>
    /// Конвертер цвета одежды в трансферную модель
    /// </summary>
    public class ColorClothesTransferConverter : TransferConverter<string, IColorDomain, ColorClothesTransfer>,
                                                 IColorClothesTransferConverter
    {
        /// <summary>
        /// Преобразовать категории одежды в трансферную модель
        /// </summary>
        public override ColorClothesTransfer ToTransfer(IColorDomain colorDomain) =>
            new ColorClothesTransfer(colorDomain.Name);

        /// <summary>
        /// Преобразовать категории одежды из трансферной модели
        /// </summary>
        public override IResultValue<IColorDomain> FromTransfer(ColorClothesTransfer colorClothesTransfer) =>
            new ColorDomain(colorClothesTransfer.Name).
            Map(colorClothesDomain => new ResultValue<IColorDomain>(colorClothesDomain));
    }
}