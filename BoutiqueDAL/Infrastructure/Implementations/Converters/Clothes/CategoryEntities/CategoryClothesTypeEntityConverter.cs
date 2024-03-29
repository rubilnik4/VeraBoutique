﻿using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Categories;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.CategoryEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.GenderEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.CategoryEntities
{
    public class CategoryClothesTypeEntityConverter : EntityConverter<string, ICategoryClothesTypeDomain, CategoryEntity>,
                                                      ICategoryClothesTypeEntityConverter
    {
        public CategoryClothesTypeEntityConverter(IClothesTypeEntityConverter clothesTypeEntityConverter)
        {
            _clothesTypeEntityConverter = clothesTypeEntityConverter;
        }

        /// <summary>
        /// Преобразования модели основных данных вида одежды в модель базы данных
        /// </summary>
        private readonly IClothesTypeEntityConverter _clothesTypeEntityConverter;

        /// <summary>
        /// Преобразовать категорию одежды из модели базы данных
        /// </summary>
        public override IResultValue<ICategoryClothesTypeDomain> FromEntity(CategoryEntity categoryEntity) =>
            GetCategoryFunc(categoryEntity).
            ResultValueCurryOk(GetClothesTypes(categoryEntity.ClothesTypes)).
            ResultValueOk(func => func.Invoke());

        /// <summary>
        /// Преобразовать категорию одежды в модель базы данных
        /// </summary>
        public override CategoryEntity ToEntity(ICategoryClothesTypeDomain categoryClothesTypeDomain) =>
            new (categoryClothesTypeDomain);

        /// <summary>
        /// Функция получения категории одежды
        /// </summary>
        private static IResultValue<Func<IEnumerable<IClothesTypeDomain>, ICategoryClothesTypeDomain>> GetCategoryFunc(ICategoryBase category) =>
            new ResultValue<Func<IEnumerable<IClothesTypeDomain>, ICategoryClothesTypeDomain>>(
                clothesTypes => new CategoryClothesTypeDomain(category, clothesTypes));

        /// <summary>
        /// Получить типы одежды
        /// </summary>
        private IResultCollection<IClothesTypeDomain> GetClothesTypes(IReadOnlyCollection<ClothesTypeEntity>? clothesTypeEntities) =>
            clothesTypeEntities.
            ToResultValueNullCheck(ErrorResultFactory.ValueNotFoundError(clothesTypeEntities, GetType())).
            ToResultCollection().
            ResultCollectionBindOk(clothesTypes => _clothesTypeEntityConverter.FromEntities(clothesTypes));
    }
}