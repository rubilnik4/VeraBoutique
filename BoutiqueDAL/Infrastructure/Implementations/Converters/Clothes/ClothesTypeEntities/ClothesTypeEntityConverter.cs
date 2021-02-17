using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Infrastructure.Implementation.Errors;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
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
    public class ClothesTypeEntityConverter : EntityConverter<string, IClothesTypeDomain, ClothesTypeEntity>,
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
        public override IResultValue<IClothesTypeDomain> FromEntity(ClothesTypeEntity clothesTypeEntity) =>
            GetClothesTypeFunc(clothesTypeEntity).
            ResultValueCurryOk(GetCategory(clothesTypeEntity.Category)).
            ResultValueCurryOk(GenderFromComposites(clothesTypeEntity.ClothesTypeGenderComposites)).
            ResultValueOk(func => func.Invoke());

        /// <summary>
        /// Преобразовать вид одежды в модель базы данных
        /// </summary>
        public override ClothesTypeEntity ToEntity(IClothesTypeDomain clothesTypeDomain) =>
             new ClothesTypeEntity(clothesTypeDomain, clothesTypeDomain.CategoryName,
                  GenderToComposites(clothesTypeDomain.Genders, clothesTypeDomain));

        /// <summary>
        /// Функция получения типа одежды
        /// </summary>
        private static IResultValue<Func<ICategoryDomain, IEnumerable<IGenderDomain>, IClothesTypeDomain>> GetClothesTypeFunc(IClothesTypeBase clothesType) =>
            new ResultValue<Func<ICategoryDomain, IEnumerable<IGenderDomain>, IClothesTypeDomain>>(
                (category, genderDomains) => new ClothesTypeDomain(clothesType, category, genderDomains));

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
        private static IResultCollection<GenderEntity> GetGenders(IEnumerable<ClothesTypeGenderCompositeEntity> clothesTypeGenderComposites) =>
            clothesTypeGenderComposites.
            Select(clothesTypeGender => clothesTypeGender.Gender.
                                        ToResultValueNullCheck(ConverterErrors.ValueNotFoundError(nameof(clothesTypeGender.Gender)))).
            ToResultCollection();

        /// <summary>
        /// Преобразовать пол одежды в доменную модель
        /// </summary>
        private IResultValue<ICategoryDomain> GetCategory(CategoryEntity? categoryEntity) =>
            categoryEntity.
            ToResultValueNullCheck(ConverterErrors.ValueNotFoundError(nameof(categoryEntity))).
            ResultValueBindOk(category => _categoryEntityConverter.FromEntity(category));

        /// <summary>
        /// Получить связующие сущности пола и вида одежды
        /// </summary>
        private static IEnumerable<ClothesTypeGenderCompositeEntity> GenderToComposites(IEnumerable<IGenderDomain> genders,
                                                                                        IClothesTypeBase clothesType) =>
            genders.Select(gender => new ClothesTypeGenderCompositeEntity(clothesType.Name, gender.GenderType,
                                                                          null, null));
    }
}