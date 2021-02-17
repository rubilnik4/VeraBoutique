using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
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
    using ClothesFunc = Func<IGenderDomain, IClothesTypeDomain, IEnumerable<IColorDomain>, IEnumerable<ISizeGroupDomain>, IClothesFullDomain>;
   
    /// <summary>
    /// Конвертер информации об одежде в трансферную модель
    /// </summary>
    public class ClothesTransferConverter : TransferConverter<int, IClothesFullDomain, ClothesFullTransfer>,
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
        public override ClothesFullTransfer ToTransfer(IClothesFullDomain clothesFullDomain) =>
            new ClothesFullTransfer(clothesFullDomain,
                                _genderTransferConverter.ToTransfer(clothesFullDomain.Gender),
                                _clothesTypeShortTransferConverter.ToTransfer(clothesFullDomain.ClothesType),
                                _colorTransferConverter.ToTransfers(clothesFullDomain.Colors),
                                _sizeGroupTransferConverter.ToTransfers(clothesFullDomain.SizeGroups));

        /// <summary>
        /// Преобразовать одежду из трансферной модели
        /// </summary>
        public override IResultValue<IClothesFullDomain> FromTransfer(ClothesFullTransfer clothesFullTransfer) =>
              GetClothesFunc(clothesFullTransfer).
              ResultValueCurryOk(_genderTransferConverter.GetDomain(clothesFullTransfer.Gender)).
              ResultValueCurryOk(_clothesTypeShortTransferConverter.GetDomain(clothesFullTransfer.ClothesType)).
              ResultValueCurryOk(_colorTransferConverter.GetDomains(clothesFullTransfer.Colors)).
              ResultValueCurryOk(_sizeGroupTransferConverter.GetDomains(clothesFullTransfer.SizeGroups)).
              ResultValueOk(func => func.Invoke());

        /// <summary>
        /// Функция получения одежды
        /// </summary>
        private static IResultValue<ClothesFunc> GetClothesFunc(IClothesBase clothes) =>
            new ResultValue<ClothesFunc>(
                (gender, clothesTypeShort, colors, sizeGroups) => new ClothesFullDomain(clothes, gender, clothesTypeShort, 
                                                                                    colors, sizeGroups));
    }
}