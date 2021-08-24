using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Images;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.GenderTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ImagesConverters;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfers;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Functional.Models.Implementations.Results;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTransfers
{
    using ClothesFunc = Func<IEnumerable<IClothesImageDomain>, IGenderDomain, IClothesTypeDomain, IEnumerable<IColorDomain>, 
                             IEnumerable<ISizeGroupMainDomain>, IClothesMainDomain>;
   
    /// <summary>
    /// Конвертер информации об одежде в трансферную модель
    /// </summary>
    public class ClothesMainTransferConverter : TransferConverter<int, IClothesMainDomain, ClothesMainTransfer>,
                                            IClothesMainTransferConverter
    {
        public ClothesMainTransferConverter(IClothesImageTransferConverter clothesImageTransferConverter,
                                            IGenderTransferConverter genderTransferConverter,
                                            IClothesTypeTransferConverter clothesTypeShortTransferConverter,
                                            IColorTransferConverter colorTransferConverter,
                                            ISizeGroupMainTransferConverter sizeGroupMainTransferConverter)
        {
            _clothesImageTransferConverter = clothesImageTransferConverter;
            _genderTransferConverter = genderTransferConverter;
            _clothesTypeShortTransferConverter = clothesTypeShortTransferConverter;
            _colorTransferConverter = colorTransferConverter;
            _sizeGroupMainTransferConverter = sizeGroupMainTransferConverter;
        }

        /// <summary>
        /// Конвертер изображений в трансферную модель
        /// </summary>
        private readonly IClothesImageTransferConverter _clothesImageTransferConverter;

        /// <summary>
        /// Конвертер вида одежды в трансферную модель
        /// </summary>
        private readonly IGenderTransferConverter _genderTransferConverter;

        /// <summary>
        /// Конвертер вида одежды в трансферную модель
        /// </summary>
        private readonly IClothesTypeTransferConverter _clothesTypeShortTransferConverter;

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
        public override ClothesMainTransfer ToTransfer(IClothesMainDomain clothesMainDomain) =>
            new ClothesMainTransfer(clothesMainDomain,
                                    _clothesImageTransferConverter.ToTransfers(clothesMainDomain.Images),
                                    _genderTransferConverter.ToTransfer(clothesMainDomain.Gender),
                                    _clothesTypeShortTransferConverter.ToTransfer(clothesMainDomain.ClothesType),
                                    _colorTransferConverter.ToTransfers(clothesMainDomain.Colors),
                                    _sizeGroupMainTransferConverter.ToTransfers(clothesMainDomain.SizeGroups));

        /// <summary>
        /// Преобразовать одежду из трансферной модели
        /// </summary>
        public override IResultValue<IClothesMainDomain> FromTransfer(ClothesMainTransfer clothesMainTransfer) =>
              GetClothesFunc(clothesMainTransfer).
              ResultValueCurryOk(_clothesImageTransferConverter.GetDomains(clothesMainTransfer.Images)).
              ResultValueCurryOk(_genderTransferConverter.GetDomain(clothesMainTransfer.Gender)).
              ResultValueCurryOk(_clothesTypeShortTransferConverter.GetDomain(clothesMainTransfer.ClothesType)).
              ResultValueCurryOk(_colorTransferConverter.GetDomains(clothesMainTransfer.Colors)).
              ResultValueCurryOk(_sizeGroupMainTransferConverter.GetDomains(clothesMainTransfer.SizeGroups)).
              ResultValueOk(func => func.Invoke());

        /// <summary>
        /// Функция получения одежды
        /// </summary>
        private static IResultValue<ClothesFunc> GetClothesFunc(IClothesBase clothes) =>
            new ResultValue<ClothesFunc>(
                (images, gender, clothesTypeShort, colors, sizeGroups) => 
                    new ClothesMainDomain(clothes, images, gender, clothesTypeShort, colors, sizeGroups));
    }
}