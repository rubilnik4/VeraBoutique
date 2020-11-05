using System;
using BoutiqueCommon.Infrastructure.Implementation.Errors;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeEntity;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.ClothesTypeEntities;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes
{
    /// <summary>
    /// Преобразования модели основных данных вида одежды в модель базы данных
    /// </summary>
    public class ClothesTypeShortEntityConverter : EntityConverter<string, IClothesTypeShortDomain, IClothesTypeFullEntity, ClothesTypeFullEntity>,
                                                   IClothesTypeShortEntityConverter
    {
        /// <summary>
        /// Преобразовать тип пола из модели базы данных
        /// </summary>
        public override IResultValue<IClothesTypeShortDomain> FromEntity(IClothesTypeFullEntity clothesTypeFullEntity) =>
            new ClothesTypeShortDomain(clothesTypeFullEntity.Name).
            Map(clothesType => new ResultValue<IClothesTypeShortDomain>(clothesType));

        /// <summary>
        /// Преобразовать тип пола в модель базы данных
        /// </summary>
        public override ClothesTypeFullEntity ToEntity(IClothesTypeShortDomain clothesTypeShortDomain) =>
            new ClothesTypeFullEntity(clothesTypeShortDomain.Name);

        /// <summary>
        /// Функция получения типа одежды
        /// </summary>
        private static IResultValue<Func<ICategoryDomain, IClothesTypeFullDomain>> GetClothesTypeFunc(string name) =>
            new ResultValue<Func<ICategoryDomain, IClothesTypeFullDomain>>(
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