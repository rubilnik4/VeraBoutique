﻿using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Infrastructure.Implementation.Errors;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeEntity;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.ClothesTypeEntities;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.ClothesType
{
    /// <summary>
    /// Преобразования модели вида одежды в модель базы данных
    /// </summary>
    public class ClothesTypeEntityConverter : EntityConverter<string, IClothesTypeFullDomain, IClothesTypeEntity, ClothesTypeEntity>,
                                              IClothesTypeEntityConverter
    {
        public ClothesTypeEntityConverter(ICategoryEntityConverter categoryEntityConverter, 
                                          IGenderEntityConverter genderEntityConverter)
        {
            _categoryEntityConverter = categoryEntityConverter;
            _genderEntityConverter = genderEntityConverter;
        }

        /// <summary>
        /// Преобразования модели категории одежды в модель базы данных
        /// </summary>
        private readonly ICategoryEntityConverter _categoryEntityConverter;

        /// <summary>
        /// Преобразования модели типа пола и модель базы данных
        /// </summary>
        private readonly IGenderEntityConverter _genderEntityConverter;

        /// <summary>
        /// Преобразовать вид одежды из модели базы данных
        /// </summary>
        public override IResultValue<IClothesTypeFullDomain> FromEntity(IClothesTypeEntity clothesTypeEntity) =>
            GetClothesTypeFunc(clothesTypeEntity.Name).
            ResultCurryOkBind(GetCategory(clothesTypeEntity.Category)).
            ResultCurryOkBind(GenderFromComposites(clothesTypeEntity.ClothesTypeGenderComposites)).
            ResultValueOk(func => func.Invoke());

        /// <summary>
        /// Преобразовать вид одежды в модель базы данных
        /// </summary>
        public override ClothesTypeEntity ToEntity(IClothesTypeFullDomain clothesTypeFullDomain) =>
            new ClothesTypeEntity(clothesTypeFullDomain.Name,
                                      clothesTypeFullDomain.Category.Name,
                                      _categoryEntityConverter.ToEntity(clothesTypeFullDomain.Category),
                                      GenderToComposites(clothesTypeFullDomain.Genders, clothesTypeFullDomain.Name),
                                      Enumerable.Empty<ClothesEntity>());

        /// <summary>
        /// Преобразовать в базовую модель вида одежды из модели базы данных
        /// </summary>
        public IResultValue<IClothesTypeShortDomain> FromEntityShort(IClothesTypeEntity clothesTypeEntity) =>
            FromEntity(clothesTypeEntity).
            ResultValueBindOk(clothesTypeFullDomain => clothesTypeFullDomain.ToClothesTypeShort());

        /// <summary>
        /// Преобразовать в базовые модели вида одежды из моделей базы данных
        /// </summary>
        public IResultCollection<IClothesTypeShortDomain> FromEntityShorts(IEnumerable<IClothesTypeEntity> clothesTypeFullEntities) =>
            clothesTypeFullEntities.
            Select(FromEntityShort).
            ToResultCollection();

        /// <summary>
        /// Функция получения типа одежды
        /// </summary>
        private static IResultValue<Func<ICategoryDomain, IEnumerable<IGenderDomain>, IClothesTypeFullDomain>> GetClothesTypeFunc(string name) =>
            new ResultValue<Func<ICategoryDomain, IEnumerable<IGenderDomain>, IClothesTypeFullDomain>>(
                (categoryDomain, genderDomains) => new ClothesTypeDomain(name, categoryDomain, genderDomains));

        /// <summary>
        /// Преобразовать пол одежды в доменную модель
        /// </summary>
        private IResultValue<ICategoryDomain> GetCategory(ICategoryEntity? categoryEntity) =>
            categoryEntity.
            ToResultValueNullCheck(ConverterErrors.ValueNotFoundError(nameof(categoryEntity))).
            ResultValueBindOk(category => _categoryEntityConverter.FromEntity(category));

        /// <summary>
        /// Получить связующие сущности пола и вида одежды
        /// </summary>>
        private static IEnumerable<ClothesTypeGenderCompositeEntity> GenderToComposites(IEnumerable<IGenderDomain> genders,
                                                                                                  string clothesType) =>
            genders.Select(gender => new ClothesTypeGenderCompositeEntity(clothesType, gender.GenderType));

        /// <summary>
        /// Преобразовать связующую сущность в коллекцию типа пола
        /// </summary>
        private IResultCollection<IGenderDomain> GenderFromComposites(IEnumerable<ClothesTypeGenderCompositeEntity>? clothesTypeGenderComposites) =>
            clothesTypeGenderComposites.
            ToResultValueNullCheck(ConverterErrors.ValueNotFoundError(nameof(clothesTypeGenderComposites))).
            ResultValueBindOkToCollection(GetGenders).
            ResultCollectionBindOk(genders => _genderEntityConverter.FromEntities(genders));

        /// <summary>
        /// Получить сущности размера типа пола
        /// </summary>
        private static IResultCollection<IGenderEntity> GetGenders(IEnumerable<ClothesTypeGenderCompositeEntity> clothesTypeGenderComposites) =>
            clothesTypeGenderComposites.
            Select(clothesTypeGender => clothesTypeGender.Gender.
                                        ToResultValueNullCheck(ConverterErrors.ValueNotFoundError(nameof(clothesTypeGender.Gender)))).
            ToResultCollection();
    }
}