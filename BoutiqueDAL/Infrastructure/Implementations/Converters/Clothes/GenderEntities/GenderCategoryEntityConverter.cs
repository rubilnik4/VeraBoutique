﻿using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Infrastructure.Implementation.Errors;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Categories;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.GenderDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.CategoryEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.GenderEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.GenderEntities
{
    /// <summary>
    /// Преобразования модели типа пола с категорией в модель базы данных
    /// </summary>
    public class GenderCategoryEntityConverter : EntityConverter<GenderType, IGenderCategoryDomain, GenderEntity>,
                                                 IGenderCategoryEntityConverter
    {
        public GenderCategoryEntityConverter(ICategoryClothesTypeEntityConverter categoryClothesTypeEntityConverter)
        {
            _categoryClothesTypeEntityConverter = categoryClothesTypeEntityConverter;
        }

        /// <summary>
        /// Преобразования модели категории одежды с типом в модель базы данных
        /// </summary>
        private readonly ICategoryClothesTypeEntityConverter _categoryClothesTypeEntityConverter;

        /// <summary>
        /// Преобразовать категорию одежды из модели базы данных
        /// </summary>
        public override IResultValue<IGenderCategoryDomain> FromEntity(GenderEntity genderEntity) =>
            GetGenderFunc(genderEntity).
            ResultValueCurryOk(CategoryDomainsFromComposite(genderEntity.GenderCategoryComposites)).
            ResultValueOk(func => func.Invoke());

        /// <summary>
        /// Преобразовать категорию одежды в модель базы данных
        /// </summary>
        public override GenderEntity ToEntity(IGenderCategoryDomain genderCategoryDomain) =>
            new GenderEntity(genderCategoryDomain);

        /// <summary>
        /// Функция получения типа пола одежды
        /// </summary>
        private static IResultValue<Func<IEnumerable<ICategoryClothesTypeDomain>, IGenderCategoryDomain>> GetGenderFunc(IGenderBase gender) =>
            new ResultValue<Func<IEnumerable<ICategoryClothesTypeDomain>, IGenderCategoryDomain>>(
                categories => new GenderCategoryDomain(gender, categories));

        /// <summary>
        /// Преобразовать связующую сущность в категорию одежды
        /// </summary>
        private IResultCollection<ICategoryClothesTypeDomain> CategoryDomainsFromComposite(IEnumerable<GenderCategoryCompositeEntity>? genderCategoryCompositeEntity) =>
            genderCategoryCompositeEntity.
            ToResultValueNullCheck(ConverterErrors.ValueNotFoundError(nameof(genderCategoryCompositeEntity))).
            ResultValueBindOkToCollection(GetCategories).
            ResultCollectionBindOk(categoryEntities => _categoryClothesTypeEntityConverter.FromEntities(categoryEntities));

        /// <summary>
        /// Получить сущности категории одежды
        /// </summary>
        private static IResultCollection<CategoryEntity> GetCategories(IEnumerable<GenderCategoryCompositeEntity> genderCategoryCompositeEntities) =>
            genderCategoryCompositeEntities.
            Select(composite => composite.Category.
                                ToResultValueNullCheck(ConverterErrors.ValueNotFoundError(nameof(composite.Gender)))).
            ToResultCollection();
    }
}