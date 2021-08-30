using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.ClothesTypes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.CategoryEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Functional.Models.Implementations.Errors;
using Functional.Models.Implementations.Results;
using Functional.Models.Interfaces.Results;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.ClothesTypeEntities
{
    /// <summary>
    /// Преобразования модели вида одежды в модель базы данных
    /// </summary>
    public class ClothesTypeMainEntityConverter : EntityConverter<string, IClothesTypeMainDomain, ClothesTypeEntity>,
                                                  IClothesTypeMainEntityConverter
    {
        public ClothesTypeMainEntityConverter(ICategoryEntityConverter categoryEntityConverter)
        {
            _categoryEntityConverter = categoryEntityConverter;
        }

        /// <summary>
        /// Преобразования модели категории одежды в модель базы данных
        /// </summary>
        private readonly ICategoryEntityConverter _categoryEntityConverter;

        /// <summary>
        /// Преобразовать вид одежды из модели базы данных
        /// </summary>
        public override IResultValue<IClothesTypeMainDomain> FromEntity(ClothesTypeEntity clothesTypeEntity) =>
            GetClothesTypeFunc(clothesTypeEntity).
            ResultValueCurryOk(GetCategory(clothesTypeEntity.Category)).
            ResultValueOk(func => func.Invoke());

        /// <summary>
        /// Преобразовать вид одежды в модель базы данных
        /// </summary>
        public override ClothesTypeEntity ToEntity(IClothesTypeMainDomain clothesTypeMainDomain) =>
             new (clothesTypeMainDomain, clothesTypeMainDomain.CategoryName);

        /// <summary>
        /// Функция получения типа одежды
        /// </summary>
        private static IResultValue<Func<ICategoryDomain, IClothesTypeMainDomain>> GetClothesTypeFunc(IClothesTypeBase clothesType) =>
            new ResultValue<Func<ICategoryDomain, IClothesTypeMainDomain>>(
                category => new ClothesTypeMainDomain(clothesType, category));

        /// <summary>
        /// Преобразовать пол одежды в доменную модель
        /// </summary>
        private IResultValue<ICategoryDomain> GetCategory(CategoryEntity? categoryEntity) =>
            categoryEntity.
            ToResultValueNullCheck(ErrorResultFactory.ValueNotFoundError(categoryEntity, GetType())).
            ResultValueBindOk(category => _categoryEntityConverter.FromEntity(category));
    }
}