using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Infrastructure.Implementation.Errors;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.ClothesTypeEntities;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.ClothesTypeEntities
{
    /// <summary>
    /// Преобразования модели основных данных вида одежды в модель базы данных
    /// </summary>
    public class ClothesTypeShortEntityConverter : EntityConverter<string, IClothesTypeShortDomain, IClothesTypeShortEntity, ClothesTypeShortEntity>,
                                                   IClothesTypeShortEntityConverter
    {
        public ClothesTypeShortEntityConverter(ICategoryEntityConverter categoryEntityConverter)
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
        public override IResultValue<IClothesTypeShortDomain> FromEntity(IClothesTypeShortEntity clothesTypeShortEntity) =>
            GetClothesTypeFunc(clothesTypeShortEntity.Name).
            ResultCurryOkBind(GetCategory(clothesTypeShortEntity.Category)).
            ResultValueOk(func => func.Invoke());

        /// <summary>
        /// Преобразовать вид одежды в модель базы данных
        /// </summary>
        public override ClothesTypeShortEntity ToEntity(IClothesTypeShortDomain clothesTypeFullDomain) =>
            new ClothesTypeShortEntity(clothesTypeFullDomain.Name,
                                       clothesTypeFullDomain.Category.Name,
                                       _categoryEntityConverter.ToEntity(clothesTypeFullDomain.Category),
                                       Enumerable.Empty<ClothesEntity>());

        /// <summary>
        /// Функция получения типа одежды
        /// </summary>
        private static IResultValue<Func<ICategoryDomain, IClothesTypeShortDomain>> GetClothesTypeFunc(string name) =>
            new ResultValue<Func<ICategoryDomain, IClothesTypeShortDomain>>(
                categoryDomain => new ClothesTypeShortDomain(name, categoryDomain));

        /// <summary>
        /// Преобразовать пол одежды в доменную модель
        /// </summary>
        private IResultValue<ICategoryDomain> GetCategory(ICategoryEntity? categoryEntity) =>
            categoryEntity.
            ToResultValueNullCheck(ConverterErrors.ValueNotFoundError(nameof(categoryEntity))).
            ResultValueBindOk(category => _categoryEntityConverter.FromEntity(category));
    }
}