using System;
using System.Collections.Generic;
using BoutiqueCommon.Infrastructure.Implementation.Errors;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Models.Implementations.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.SizeGroup;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.ClothesTypeTransfers
{
    /// <summary>
    /// Конвертер вида одежды в трансферную модель
    /// </summary>
    public class ClothesTypeTransferConverter : TransferConverter<string, IClothesTypeDomain, ClothesTypeTransfer>,
                                                IClothesTypeTransferConverter
    {
        public ClothesTypeTransferConverter(ICategoryTransferConverter categoryTransferConverter,
                                            IGenderTransferConverter genderTransferConverter)
        {
            _categoryTransferConverter = categoryTransferConverter;
            _genderTransferConverter = genderTransferConverter;
        }

        /// <summary>
        /// Конвертер категорий одежды в трансферную модель
        /// </summary>
        private readonly ICategoryTransferConverter _categoryTransferConverter;


        /// <summary>
        /// Конвертер типа пола в трансферную модель
        /// </summary>
        private readonly IGenderTransferConverter _genderTransferConverter;

        /// <summary>
        /// Преобразовать тип одежды в трансферную модель
        /// </summary>
        public override ClothesTypeTransfer ToTransfer(IClothesTypeDomain clothesTypeFullDomain) =>
            new ClothesTypeTransfer(clothesTypeFullDomain,
                                    _categoryTransferConverter.ToTransfer(clothesTypeFullDomain.Category),
                                    _genderTransferConverter.ToTransfers(clothesTypeFullDomain.Genders));

        ///// <summary>
        ///// Преобразовать тип одежды из трансферной модели
        ///// </summary>
        public override IResultValue<IClothesTypeDomain> FromTransfer(ClothesTypeTransfer clothesTypeTransfer) =>
            GetClothesTypeFunc(clothesTypeTransfer).
            ResultCurryOkBind(_categoryTransferConverter.GetDomain(clothesTypeTransfer.Category)).
            ResultCurryOkBind(_genderTransferConverter.GetDomains(clothesTypeTransfer.Genders)).
            ResultValueOk(func => func.Invoke());

        /// <summary>
        /// Функция получения типа одежды
        /// </summary>
        private static IResultValue<Func<ICategoryDomain, IEnumerable<IGenderDomain>, IClothesTypeDomain>> GetClothesTypeFunc(IClothesType clothesType) =>
            new ResultValue<Func<ICategoryDomain, IEnumerable<IGenderDomain>, IClothesTypeDomain>>(
                (category, genders) => new ClothesTypeDomain(clothesType, category, genders));
    }
}