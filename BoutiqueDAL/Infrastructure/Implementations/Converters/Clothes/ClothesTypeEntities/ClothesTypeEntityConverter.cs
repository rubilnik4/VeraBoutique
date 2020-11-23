﻿using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Infrastructure.Implementation.Errors;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.ClothesTypeEntities;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.ClothesTypeEntities
{
    /// <summary>
    /// Преобразования модели вида одежды в модель базы данных
    /// </summary>
    public class ClothesTypeEntityConverter : EntityConverter<string, IClothesTypeDomain, IClothesTypeEntity, ClothesTypeEntity>,
                                              IClothesTypeEntityConverter
    {
        public ClothesTypeEntityConverter(IClothesTypeShortEntityConverter clothesTypeShortEntityConverter,
                                          IGenderEntityConverter genderEntityConverter)
        {
            _clothesTypeShortEntityConverter = clothesTypeShortEntityConverter;
            _genderEntityConverter = genderEntityConverter;
        }

        /// <summary>
        /// Преобразования модели категории одежды в модель базы данных
        /// </summary>
        private readonly IClothesTypeShortEntityConverter _clothesTypeShortEntityConverter;

        /// <summary>
        /// Преобразования модели типа пола и модель базы данных
        /// </summary>
        private readonly IGenderEntityConverter _genderEntityConverter;

        /// <summary>
        /// Преобразовать вид одежды из модели базы данных
        /// </summary>
        public override IResultValue<IClothesTypeDomain> FromEntity(IClothesTypeEntity clothesTypeEntity) =>
            GetClothesTypeFunc().
            ResultCurryOkBind(_clothesTypeShortEntityConverter.FromEntity(clothesTypeEntity)).
            ResultCurryOkBind(GenderFromComposites(clothesTypeEntity.ClothesTypeGenderComposites)).
            ResultValueOk(func => func.Invoke());

        /// <summary>
        /// Преобразовать вид одежды в модель базы данных
        /// </summary>
        public override ClothesTypeEntity ToEntity(IClothesTypeDomain clothesTypeDomain) =>
            _clothesTypeShortEntityConverter.ToEntity(clothesTypeDomain).
            Map(clothesTypeShort => new ClothesTypeEntity(clothesTypeShort,
                                                          GenderToComposites(clothesTypeDomain.Genders,
                                                                             clothesTypeDomain.Name,
                                                                             clothesTypeDomain.Category.Name)));

        /// <summary>
        /// Функция получения типа одежды
        /// </summary>
        private static IResultValue<Func<IClothesTypeShortDomain, IEnumerable<IGenderDomain>, IClothesTypeDomain>> GetClothesTypeFunc() =>
            new ResultValue<Func<IClothesTypeShortDomain, IEnumerable<IGenderDomain>, IClothesTypeDomain>>(
                (clothesTypeShort, genderDomains) => new ClothesTypeDomain(clothesTypeShort, genderDomains));
        
        /// <summary>
        /// Получить связующие сущности пола и вида одежды
        /// </summary>>
        private static IEnumerable<ClothesTypeGenderCompositeEntity> GenderToComposites(IEnumerable<IGenderDomain> genders,
                                                                                        string clothesTypeName, 
                                                                                        string categoryName) =>
            genders.Select(gender => new ClothesTypeGenderCompositeEntity(clothesTypeName, gender.GenderType, 
                                                                          new ClothesTypeEntity(clothesTypeName, categoryName), 
                                                                          new GenderEntity(gender.GenderType, gender.Name)));

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