﻿using BoutiqueCommon.Infrastructure.Implementation.Errors;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes
{
    /// <summary>
    /// Конвертер категорий одежды в трансферную модель
    /// </summary>
    public class CategoryTransferConverter : TransferConverter<string, ICategoryDomain, CategoryTransfer>,
                                             ICategoryTransferConverter
    {
        /// <summary>
        /// Преобразовать категории одежды в трансферную модель
        /// </summary>
        public override CategoryTransfer ToTransfer(ICategoryDomain categoryDomain) =>
            new CategoryTransfer(categoryDomain.Name);

        /// <summary>
        /// Преобразовать категории одежды из трансферной модели
        /// </summary>
        public override IResultValue<ICategoryDomain> FromTransfer(CategoryTransfer categoryTransfer) =>
            new CategoryDomain(categoryTransfer.Name).
            Map(categoryDomain => new ResultValue<ICategoryDomain>(categoryDomain));
    }
}