using System;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes
{
    /// <summary>
    /// Преобразования модели вида одежды в модель базы данных
    /// </summary>
    public class ClothesTypeEntityConverter : EntityConverter<string, IClothesTypeDomain, IClothesTypeEntity, ClothesTypeEntity>,
                                              IClothesTypeEntityConverter
    {
        public ClothesTypeEntityConverter(ICategoryEntityConverter categoryEntityConverter)
        {
            _categoryEntityConverter = categoryEntityConverter;
        }

        /// <summary>
        /// Преобразования модели категории одежды в модель базы данных
        /// </summary>
        private readonly ICategoryEntityConverter _categoryEntityConverter;

        /// <summary>
        /// Преобразовать тип пола из модели базы данных
        /// </summary>
        public override IResultValue<IClothesTypeDomain> FromEntity(IClothesTypeEntity clothesTypeEntity) =>
            GetClothesTypeFunc(clothesTypeEntity.Name).
            ResultCurryOkBind(GetCategory(clothesTypeEntity.CategoryEntity)).
            ResultValueOk(func => func.Invoke());

        /// <summary>
        /// Преобразовать тип пола в модель базы данных
        /// </summary>
        public override ClothesTypeEntity ToEntity(IClothesTypeDomain clothesTypeDomain) =>
            new ClothesTypeEntity(clothesTypeDomain.Name,
                                  _categoryEntityConverter.ToEntity(clothesTypeDomain.CategoryDomain));

        /// <summary>
        /// Функция получения типа одежды
        /// </summary>
        private static IResultValue<Func<ICategoryDomain, IClothesTypeDomain>> GetClothesTypeFunc(string name) =>
            new ResultValue<Func<ICategoryDomain, IClothesTypeDomain>>(
                categoryDomain => new ClothesTypeDomain(name, categoryDomain));

        /// <summary>
        /// Преобразовать пол одежды в доменную модель
        /// </summary>
        private IResultValue<ICategoryDomain> GetCategory(ICategoryEntity? categoryEntity) =>
            categoryEntity.
            ToResultValueNullCheck(ConverterErrors.ValueNotFoundError(nameof(categoryEntity))).
            ResultValueBindOk(category => _categoryEntityConverter.FromEntity(category));
    }
}