using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Categories;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.GenderDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.CategoryTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.GenderTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.CategoryTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.GenderTransfers;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Functional.Models.Implementations.Results;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.GenderTransfers
{
    /// <summary>
    /// Конвертер типа пола c категорией в трансферную модель
    /// </summary>
    public class GenderCategoryTransferConverter : TransferConverter<GenderType, IGenderCategoryDomain, GenderCategoryTransfer>,
                                                   IGenderCategoryTransferConverter
    {
        public GenderCategoryTransferConverter(ICategoryClothesTypeTransferConverter categoryClothesTypeTransferConverter)
        {
            _categoryClothesTypeTransferConverter = categoryClothesTypeTransferConverter;
        }

        /// <summary>
        /// Конвертер категорий одежды с типом в трансферную модель
        /// </summary>
        private readonly ICategoryClothesTypeTransferConverter _categoryClothesTypeTransferConverter;

        /// <summary>
        /// Преобразовать категории одежды в трансферную модель
        /// </summary>
        public override GenderCategoryTransfer ToTransfer(IGenderCategoryDomain genderCategoryDomain) =>
            new GenderCategoryTransfer(genderCategoryDomain,
                                       _categoryClothesTypeTransferConverter.ToTransfers(genderCategoryDomain.Categories));

        /// <summary>
        /// Преобразовать категории одежды из трансферной модели
        /// </summary>
        public override IResultValue<IGenderCategoryDomain> FromTransfer(GenderCategoryTransfer genderCategoryTransfer) =>
            GetGenderFunc(genderCategoryTransfer).
            ResultValueCurryOk(_categoryClothesTypeTransferConverter.GetDomains(genderCategoryTransfer.Categories)).
            ResultValueOk(func => func.Invoke());

        /// <summary>
        /// Функция получения пола одежды
        /// </summary>
        private static IResultValue<Func<IEnumerable<ICategoryClothesTypeDomain>, IGenderCategoryDomain>> GetGenderFunc(IGenderBase gender) =>
            new ResultValue<Func<IEnumerable<ICategoryClothesTypeDomain>, IGenderCategoryDomain>>(
                categories => new GenderCategoryDomain(gender, categories));
    }
}