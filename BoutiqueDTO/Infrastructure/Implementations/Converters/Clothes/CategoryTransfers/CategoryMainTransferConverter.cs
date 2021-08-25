using System;
using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Categories;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.SizeGroups;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.CategoryTransfers;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.GenderTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.CategoryTransfers;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Functional.Models.Implementations.Results;
using Functional.Models.Interfaces.Results;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.CategoryTransfers
{
    /// <summary>
    /// Конвертер категорий одежды в трансферную модель
    /// </summary>
    public class CategoryMainTransferConverter : TransferConverter<string, ICategoryMainDomain, CategoryMainTransfer>,
                                                ICategoryMainTransferConverter
    {
        public CategoryMainTransferConverter(IGenderTransferConverter genderTransferConverter)
        {
            _genderTransferConverter = genderTransferConverter;
        }

        /// <summary>
        /// Конвертер типа пола в трансферную модель
        /// </summary>
        private readonly IGenderTransferConverter _genderTransferConverter;

        /// <summary>
        /// Преобразовать категории одежды в трансферную модель
        /// </summary>
        public override CategoryMainTransfer ToTransfer(ICategoryMainDomain categoryMainDomain) =>
            new CategoryMainTransfer(categoryMainDomain, _genderTransferConverter.ToTransfers(categoryMainDomain.Genders));

        /// <summary>
        /// Преобразовать категории одежды из трансферной модели
        /// </summary>
        public override IResultValue<ICategoryMainDomain> FromTransfer(CategoryMainTransfer categoryMainTransfer) =>
            GetCategoryFunc(categoryMainTransfer).
            ResultValueCurryOk(_genderTransferConverter.GetDomains(categoryMainTransfer.Genders)).
            ResultValueOk(func => func.Invoke());

        /// <summary>
        /// Функция получения категории одежды
        /// </summary>
        private static IResultValue<Func<IEnumerable<IGenderDomain>, ICategoryMainDomain>> GetCategoryFunc(ICategoryBase category) =>
            new ResultValue<Func<IEnumerable<IGenderDomain>, ICategoryMainDomain>>(
                genders => new CategoryMainDomain(category, genders));
    }
}