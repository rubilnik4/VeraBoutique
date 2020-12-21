using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.SizeGroupTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTransfers;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTransfers
{
    using ClothesFunc = Func<IGenderDomain, IClothesTypeShortDomain, IEnumerable<IColorDomain>, IEnumerable<ISizeGroupDomain>, IClothesDomain>;
   
    /// <summary>
    /// Конвертер информации об одежде в трансферную модель
    /// </summary>
    public class ClothesTransferConverter : TransferConverter<int, IClothesDomain, ClothesTransfer>,
                                            IClothesTransferConverter
    {
        public ClothesTransferConverter(IGenderTransferConverter genderTransferConverter,
                                        IClothesTypeShortTransferConverter clothesTypeShortTransferConverter,
                                        IColorTransferConverter colorTransferConverter,
                                        ISizeGroupTransferConverter sizeGroupTransferConverter)
        {
            _genderTransferConverter = genderTransferConverter;
            _clothesTypeShortTransferConverter = clothesTypeShortTransferConverter;
            _colorTransferConverter = colorTransferConverter;
            _sizeGroupTransferConverter = sizeGroupTransferConverter;
        }

        /// <summary>
        /// Конвертер вида одежды в трансферную модель
        /// </summary>
        private readonly IGenderTransferConverter _genderTransferConverter;

        /// <summary>
        /// Конвертер вида одежды в трансферную модель
        /// </summary>
        private readonly IClothesTypeShortTransferConverter _clothesTypeShortTransferConverter;

        /// <summary>
        /// Конвертер цвета одежды в трансферную модель
        /// </summary>
        private readonly IColorTransferConverter _colorTransferConverter;

        /// <summary>
        /// Конвертер цвета одежды в трансферную модель
        /// </summary>
        private readonly ISizeGroupTransferConverter _sizeGroupTransferConverter;

        /// <summary>
        /// Преобразовать одежду в трансферную модель
        /// </summary>
        public override ClothesTransfer ToTransfer(IClothesDomain clothesDomain) =>
            new ClothesTransfer(clothesDomain,
                                _genderTransferConverter.ToTransfer(clothesDomain.Gender),
                                _clothesTypeShortTransferConverter.ToTransfer(clothesDomain.ClothesTypeShort),
                                _colorTransferConverter.ToTransfers(clothesDomain.Colors),
                                _sizeGroupTransferConverter.ToTransfers(clothesDomain.SizeGroups));

        /// <summary>
        /// Преобразовать одежду из трансферной модели
        /// </summary>
        public override IResultValue<IClothesDomain> FromTransfer(ClothesTransfer clothesTransfer) =>
              GetClothesFunc(clothesTransfer).
              ResultCurryOkBind(_genderTransferConverter.GetDomain(clothesTransfer.Gender)).
              ResultCurryOkBind(_clothesTypeShortTransferConverter.GetDomain(clothesTransfer.ClothesTypeShort)).
              ResultCurryOkBind(_colorTransferConverter.GetDomains(clothesTransfer.Colors)).
              ResultCurryOkBind(_sizeGroupTransferConverter.GetDomains(clothesTransfer.SizeGroups)).
              ResultValueOk(func => func.Invoke());

        /// <summary>
        /// Функция получения одежды
        /// </summary>
        private static IResultValue<ClothesFunc> GetClothesFunc(IClothesShortBase clothes) =>
            new ResultValue<ClothesFunc>(
                (gender, clothesTypeShort, colors, sizeGroups) => new ClothesDomain(clothes, gender, clothesTypeShort, 
                                                                                    colors, sizeGroups));
    }
}