using System;
using System.Collections.Generic;
using BoutiqueCommon.Infrastructure.Implementation.Errors;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.CategoryTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Functional.Models.Implementations.Results;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTypeTransfers
{
    /// <summary>
    /// Конвертер вида одежды в трансферную модель
    /// </summary>
    public class ClothesTypeMainTransferConverter : TransferConverter<string, IClothesTypeMainDomain, ClothesTypeMainTransfer>,
                                                    IClothesTypeMainTransferConverter
    {
        public ClothesTypeMainTransferConverter(ICategoryTransferConverter categoryTransferConverter)
        {
            _categoryTransferConverter = categoryTransferConverter;
        }

        /// <summary>
        /// Конвертер категорий одежды в трансферную модель
        /// </summary>
        private readonly ICategoryTransferConverter _categoryTransferConverter;

        /// <summary>
        /// Преобразовать тип одежды в трансферную модель
        /// </summary>
        public override ClothesTypeMainTransfer ToTransfer(IClothesTypeMainDomain clothesTypeMainDomain) =>
            new ClothesTypeMainTransfer(clothesTypeMainDomain,
                                        _categoryTransferConverter.ToTransfer(clothesTypeMainDomain.Category));

        ///// <summary>
        ///// Преобразовать тип одежды из трансферной модели
        ///// </summary>
        public override IResultValue<IClothesTypeMainDomain> FromTransfer(ClothesTypeMainTransfer clothesTypeMainTransfer) =>
            GetClothesTypeFunc(clothesTypeMainTransfer).
            ResultValueCurryOk(_categoryTransferConverter.GetDomain(clothesTypeMainTransfer.Category)).
            ResultValueOk(func => func.Invoke());

        /// <summary>
        /// Функция получения типа одежды
        /// </summary>
        private static IResultValue<Func<ICategoryDomain, IClothesTypeMainDomain>> GetClothesTypeFunc(IClothesTypeBase clothesType) =>
            new ResultValue<Func<ICategoryDomain, IClothesTypeMainDomain>>(
                category => new ClothesTypeMainDomain(clothesType, category));
    }
}