﻿using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Infrastructure.Implementation.Errors;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.GenderEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.SizeGroupEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.ClothesEntities
{
    using ClothesFunc = Func<IGenderDomain, IClothesTypeDomain,
                             IEnumerable<IColorDomain>, IEnumerable<ISizeGroupMainDomain>,
                             IClothesMainDomain>;

    /// <summary>
    /// Преобразования модели информации об одежде в модель базы данных
    /// </summary>
    public class ClothesMainEntityConverter : EntityConverter<int, IClothesMainDomain, ClothesEntity>, IClothesMainEntityConverter
    {
        public ClothesMainEntityConverter(IGenderEntityConverter genderEntityConverter,
                                          IClothesTypeEntityConverter clothesTypeEntityConverter,
                                          IColorClothesEntityConverter colorClothesEntityConverter,
                                          ISizeGroupMainEntityConverter sizeGroupMainEntityConverter)
        {
            _genderEntityConverter = genderEntityConverter;
            _clothesTypeEntityConverter = clothesTypeEntityConverter;
            _colorClothesEntityConverter = colorClothesEntityConverter;
            _sizeGroupMainEntityConverter = sizeGroupMainEntityConverter;
        }

        /// <summary>
        /// Преобразования модели вида одежды в модель базы данных
        /// </summary>
        private readonly IGenderEntityConverter _genderEntityConverter;

        /// <summary>
        /// Преобразования модели основных данных вида одежды в модель базы данных
        /// </summary>
        private readonly IClothesTypeEntityConverter _clothesTypeEntityConverter;

        /// <summary>
        /// Преобразования модели цвета одежды в модель базы данных
        /// </summary>
        private readonly IColorClothesEntityConverter _colorClothesEntityConverter;

        /// <summary>
        /// Преобразования модели категории одежды в модель базы данных
        /// </summary>
        private readonly ISizeGroupMainEntityConverter _sizeGroupMainEntityConverter;

        /// <summary>
        /// Преобразовать категорию одежды из модели базы данных
        /// </summary>
        public override IResultValue<IClothesMainDomain> FromEntity(ClothesEntity clothesEntity) =>
            GetClothesFunc(clothesEntity).
            ResultValueCurryOk(GetGender(clothesEntity.Gender)).
            ResultValueCurryOk(GetClothesType(clothesEntity.ClothesType)).
            ResultValueCurryOk(ColorClothesFromComposite(clothesEntity.ClothesColorComposites)).
            ResultValueCurryOk(SizeGroupFromComposite(clothesEntity.ClothesSizeGroupComposites)).
            ResultValueOk(func => func.Invoke());

        /// <summary>
        /// Преобразовать категорию одежды в модель базы данных
        /// </summary>
        public override ClothesEntity ToEntity(IClothesMainDomain clothesMainDomain) =>
            new ClothesEntity(clothesMainDomain, clothesMainDomain.Gender.GenderType,
                 clothesMainDomain.ClothesType.Name,
                 ColorClothesToComposite(clothesMainDomain.Colors, clothesMainDomain.Id),
                 SizeGroupToComposite(clothesMainDomain.SizeGroups, clothesMainDomain.Id));

        /// <summary>
        /// Функция получения информации об одежде
        /// </summary>
        private static IResultValue<ClothesFunc> GetClothesFunc(IClothesBase clothes) =>
            new ResultValue<ClothesFunc>(
                (gender, clothesType, colors, sizes) => new ClothesMainDomain(clothes, gender, clothesType, colors, sizes));

        /// <summary>
        /// Преобразовать связующую сущность в коллекцию цветов
        /// </summary>
        private IResultCollection<IColorDomain> ColorClothesFromComposite(IEnumerable<ClothesColorCompositeEntity>? clothesColorCompositeEntities) =>
            clothesColorCompositeEntities.
            ToResultValueNullCheck(ConverterErrors.ValueNotFoundError(nameof(clothesColorCompositeEntities))).
            ResultValueBindOkToCollection(GetColorClothes).
            ResultCollectionBindOk(colorClothesEntities => _colorClothesEntityConverter.FromEntities(colorClothesEntities));

        /// <summary>
        /// Преобразовать связующую сущность в коллекцию размеров
        /// </summary>
        private IResultCollection<ISizeGroupMainDomain> SizeGroupFromComposite(IEnumerable<ClothesSizeGroupCompositeEntity>? clothesSizeCompositeEntities) =>
            clothesSizeCompositeEntities.
            ToResultValueNullCheck(ConverterErrors.ValueNotFoundError(nameof(clothesSizeCompositeEntities))).
            ResultValueBindOkToCollection(GetColorClothes).
            ResultCollectionBindOk(sizeGroupEntities => _sizeGroupMainEntityConverter.FromEntities(sizeGroupEntities));

        /// <summary>
        /// Преобразовать цвета в связующую сущность
        /// </summary>
        private IEnumerable<ClothesColorCompositeEntity> ColorClothesToComposite(IEnumerable<IColorDomain> colorClothesDomains,
                                                                                         int clothesId) =>
            _colorClothesEntityConverter.ToEntities(colorClothesDomains).
            Select(colorClothesEntity => new ClothesColorCompositeEntity(clothesId, colorClothesEntity.Name, null, null));

        /// <summary>
        /// Преобразовать размеры в связующую сущность
        /// </summary>
        private IEnumerable<ClothesSizeGroupCompositeEntity> SizeGroupToComposite(IEnumerable<ISizeGroupMainDomain> sizeGroupMainDomains,
                                                                                  int clothesId) =>
            _sizeGroupMainEntityConverter.ToEntities(sizeGroupMainDomains).
            Select(sizeGroupEntity => new ClothesSizeGroupCompositeEntity(clothesId, sizeGroupEntity.Id, null, null));

        /// <summary>
        /// Преобразовать пол одежды в доменную модель
        /// </summary>
        private IResultValue<IGenderDomain> GetGender(GenderEntity? genderEntity) =>
            genderEntity.
            ToResultValueNullCheck(ConverterErrors.ValueNotFoundError(nameof(genderEntity))).
            ResultValueBindOk(gender => _genderEntityConverter.FromEntity(gender));

        /// <summary>
        /// Преобразовать тип одежды в доменную модель
        /// </summary>
        private IResultValue<IClothesTypeDomain> GetClothesType(ClothesTypeEntity? clothesTypeEntity) =>
            clothesTypeEntity.
            ToResultValueNullCheck(ConverterErrors.ValueNotFoundError(nameof(clothesTypeEntity))).
            ResultValueBindOk(clothesType => _clothesTypeEntityConverter.FromEntity(clothesType));

        /// <summary>
        /// Получить сущности цвета одежды
        /// </summary>
        private static IResultCollection<ColorEntity> GetColorClothes(IEnumerable<ClothesColorCompositeEntity> clothesColorEntities) =>
            clothesColorEntities.
            Select(clothesColor => clothesColor.ColorClothes.
                                   ToResultValueNullCheck(ConverterErrors.ValueNotFoundError(nameof(clothesColor.ColorClothes)))).
            ToResultCollection();

        /// <summary>
        /// Получить сущности группы размеров
        /// </summary>
        private static IResultCollection<SizeGroupEntity> GetColorClothes(IEnumerable<ClothesSizeGroupCompositeEntity> clothesSizeGroupEntities) =>
            clothesSizeGroupEntities.
            Select(sizeGroup => sizeGroup.SizeGroup.
                                ToResultValueNullCheck(ConverterErrors.ValueNotFoundError(nameof(sizeGroup.SizeGroup)))).
            ToResultCollection();
    }
}