using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Infrastructure.Implementation.Errors;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Categories;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.SizeGroups;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.SizeGroupDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.CategoryEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.GenderEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Functional.Models.Implementations.Results;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.CategoryEntities
{
    /// <summary>
    /// Преобразования модели категории одежды в модель базы данных
    /// </summary>
    public class CategoryMainEntityConverter : EntityConverter<string, ICategoryMainDomain, CategoryEntity>, 
                                               ICategoryMainEntityConverter
    {
        public CategoryMainEntityConverter(IGenderEntityConverter genderEntityConverter)
        {
            _genderEntityConverter = genderEntityConverter;
        }

        /// <summary>
        /// Преобразования модели типа пола и модель базы данных
        /// </summary>
        private readonly IGenderEntityConverter _genderEntityConverter;

        /// <summary>
        /// Преобразовать категорию одежды из модели базы данных
        /// </summary>
        public override IResultValue<ICategoryMainDomain> FromEntity(CategoryEntity categoryEntity) =>
            GetCategoryFunc(categoryEntity).
            ResultValueCurryOk(GenderDomainsFromComposite(categoryEntity.GenderCategoryComposites)).
            ResultValueOk(func => func.Invoke());

        /// <summary>
        /// Преобразовать категорию одежды в модель базы данных
        /// </summary>
        public override CategoryEntity ToEntity(ICategoryMainDomain categoryDomain) =>
            new CategoryEntity(categoryDomain,
                               CategoryToCompositeEntities(categoryDomain.Genders, categoryDomain));

        /// <summary>
        /// Функция получения категории одежды
        /// </summary>
        private static IResultValue<Func<IEnumerable<IGenderDomain>, ICategoryMainDomain>> GetCategoryFunc(ICategoryBase category) =>
            new ResultValue<Func<IEnumerable<IGenderDomain>, ICategoryMainDomain>>(
                genders => new CategoryMainDomain(category, genders));

        /// <summary>
        /// Преобразовать связующую сущность в тип пола
        /// </summary>
        private IResultCollection<IGenderDomain> GenderDomainsFromComposite(IEnumerable<GenderCategoryCompositeEntity>? genderCategoryCompositeEntity) =>
            genderCategoryCompositeEntity.
            ToResultValueNullCheck(ConverterErrors.ValueNotFoundError(nameof(genderCategoryCompositeEntity))).
            ResultValueBindOkToCollection(GetGenders).
            ResultCollectionBindOk(genderEntities => _genderEntityConverter.FromEntities(genderEntities));

        /// <summary>
        /// Получить сущности типа пола одежды
        /// </summary>
        private static IResultCollection<GenderEntity> GetGenders(IEnumerable<GenderCategoryCompositeEntity> genderCategoryCompositeEntities) =>
            genderCategoryCompositeEntities.
            Select(composite => composite.Gender.
                                ToResultValueNullCheck(ConverterErrors.ValueNotFoundError(nameof(composite.Gender)))).
            ToResultCollection();

        /// <summary>
        /// Преобразовать типы пола в связующую сущность
        /// </summary>
        private IEnumerable<GenderCategoryCompositeEntity> CategoryToCompositeEntities(IEnumerable<IGenderDomain> genders,
                                                                                       ICategoryBase category) =>
            _genderEntityConverter.ToEntities(genders).
            Select(gender => new GenderCategoryCompositeEntity(gender.GenderType, category.Name));
    }
}