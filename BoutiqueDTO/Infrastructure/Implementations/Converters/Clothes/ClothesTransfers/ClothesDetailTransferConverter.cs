using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.GenderTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfers;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Functional.Models.Implementations.Results;
using Functional.Models.Interfaces.Results;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTransfers
{
    using ClothesFunc = Func<IEnumerable<IColorDomain>, IEnumerable<ISizeGroupMainDomain>, IClothesDetailDomain>;

    public class ClothesDetailTransferConverter : TransferConverter<int, IClothesDetailDomain, ClothesDetailTransfer>,
                                                  IClothesDetailTransferConverter
    {
        public ClothesDetailTransferConverter(IColorTransferConverter colorTransferConverter,
                                              ISizeGroupMainTransferConverter sizeGroupMainTransferConverter)
        {
            _colorTransferConverter = colorTransferConverter;
            _sizeGroupMainTransferConverter = sizeGroupMainTransferConverter;
        }

        /// <summary>
        /// Конвертер цвета одежды в трансферную модель
        /// </summary>
        private readonly IColorTransferConverter _colorTransferConverter;

        /// <summary>
        /// Конвертер цвета одежды в трансферную модель
        /// </summary>
        private readonly ISizeGroupMainTransferConverter _sizeGroupMainTransferConverter;

        /// <summary>
        /// Преобразовать одежду в трансферную модель
        /// </summary>
        public override ClothesDetailTransfer ToTransfer(IClothesDetailDomain clothesDetailDomain) =>
            new ClothesDetailTransfer(clothesDetailDomain, 
                                      _colorTransferConverter.ToTransfers(clothesDetailDomain.Colors),
                                      _sizeGroupMainTransferConverter.ToTransfers(clothesDetailDomain.SizeGroups));

        /// <summary>
        /// Преобразовать одежду из трансферной модели
        /// </summary>
        public override IResultValue<IClothesDetailDomain> FromTransfer(ClothesDetailTransfer clothesDetailTransfer) =>
              GetClothesFunc(clothesDetailTransfer).
              ResultValueCurryOk(_colorTransferConverter.GetDomains(clothesDetailTransfer.Colors)).
              ResultValueCurryOk(_sizeGroupMainTransferConverter.GetDomains(clothesDetailTransfer.SizeGroups)).
              ResultValueOk(func => func.Invoke());

        /// <summary>
        /// Функция получения одежды
        /// </summary>
        private static IResultValue<ClothesFunc> GetClothesFunc(IClothesBase clothes) =>
            new ResultValue<ClothesFunc>(
                (colors, sizeGroups) => new ClothesDetailDomain(clothes, colors, sizeGroups));
    }
}