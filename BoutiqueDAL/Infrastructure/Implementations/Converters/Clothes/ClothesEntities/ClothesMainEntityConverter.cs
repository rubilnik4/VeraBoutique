﻿using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Images;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.SizeGroupDomain;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.CategoryEntities;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.GenderEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ImageEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.SizeGroupEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultCollections;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.ClothesEntities
{
    using ClothesFunc = Func<IEnumerable<IClothesImageDomain>, IGenderDomain, IClothesTypeDomain,
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
                                          ISizeGroupMainEntityConverter sizeGroupMainEntityConverter,
                                          IClothesImageEntityConverter clothesImageEntityConverter)
        {
            _genderEntityConverter = genderEntityConverter;
            _clothesTypeEntityConverter = clothesTypeEntityConverter;
            _colorClothesEntityConverter = colorClothesEntityConverter;
            _sizeGroupMainEntityConverter = sizeGroupMainEntityConverter;
            _clothesImageEntityConverter = clothesImageEntityConverter;
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
        /// Преобразования модели изображения в модель базы данных
        /// </summary>
        private readonly IClothesImageEntityConverter _clothesImageEntityConverter;

        /// <summary>
        /// Преобразовать категорию одежды из модели базы данных
        /// </summary>
        public override IResultValue<IClothesMainDomain> FromEntity(ClothesEntity clothesEntity) =>
            GetClothesFunc(clothesEntity).
            ResultValueCurryOk(GetImages(clothesEntity.ClothesImages)).
            ResultValueCurryOk(GetGender(clothesEntity.Gender)).
            ResultValueCurryOk(GetClothesType(clothesEntity.ClothesType)).
            ResultValueCurryOk(ColorClothesFromComposite(clothesEntity.ClothesColorComposites, _colorClothesEntityConverter)).
            ResultValueCurryOk(SizeGroupFromComposite(clothesEntity.ClothesSizeGroupComposites, _sizeGroupMainEntityConverter)).
            ResultValueOk(func => func.Invoke());

        /// <summary>
        /// Преобразовать категорию одежды в модель базы данных
        /// </summary>
        public override ClothesEntity ToEntity(IClothesMainDomain clothesMainDomain) =>
            new(clothesMainDomain,
                _clothesImageEntityConverter.ToEntities(clothesMainDomain.Images),
                ColorClothesToComposite(clothesMainDomain.Colors, clothesMainDomain.Id, _colorClothesEntityConverter),
                SizeGroupToComposite(clothesMainDomain.SizeGroups, clothesMainDomain.Id, _sizeGroupMainEntityConverter));

        /// <summary>
        /// Функция получения информации об одежде
        /// </summary>
        private static IResultValue<ClothesFunc> GetClothesFunc(IClothesBase clothes) =>
            new ResultValue<ClothesFunc>(
                (images, gender, clothesType, colors, sizes) => new ClothesMainDomain(clothes, images, gender, clothesType, colors, sizes));

        /// <summary>
        /// Преобразовать связующую сущность в коллекцию цветов
        /// </summary>
        public static IResultCollection<IColorDomain> ColorClothesFromComposite(IReadOnlyCollection<ClothesColorCompositeEntity>? clothesColorCompositeEntities,
                                                                                IColorClothesEntityConverter colorClothesEntityConverter) =>
            clothesColorCompositeEntities.
            ToResultValueNullCheck(ErrorResultFactory.ValueNotFoundError(clothesColorCompositeEntities, typeof(ClothesMainEntityConverter))).
            ResultValueBindOkToCollection(GetColorClothes).
            ResultCollectionBindOk(colorClothesEntityConverter.FromEntities);

        /// <summary>
        /// Преобразовать связующую сущность в коллекцию размеров
        /// </summary>
        public static IResultCollection<ISizeGroupMainDomain> SizeGroupFromComposite(IReadOnlyCollection<ClothesSizeGroupCompositeEntity>? clothesSizeCompositeEntities,
                                                                                     ISizeGroupMainEntityConverter sizeGroupMainEntityConverter) =>
            clothesSizeCompositeEntities.
            ToResultValueNullCheck(ErrorResultFactory.ValueNotFoundError(clothesSizeCompositeEntities, typeof(ClothesMainEntityConverter))).
            ResultValueBindOkToCollection(GetColorClothes).
            ResultCollectionBindOk(sizeGroupMainEntityConverter.FromEntities);

        /// <summary>
        /// Преобразовать цвета в связующую сущность
        /// </summary>
        public static IEnumerable<ClothesColorCompositeEntity> ColorClothesToComposite(IEnumerable<IColorDomain> colorClothesDomains,
                                                                                         int clothesId,
                                                                                         IColorClothesEntityConverter colorClothesEntityConverter) =>
            colorClothesEntityConverter.ToEntities(colorClothesDomains).
            Select(colorClothesEntity => new ClothesColorCompositeEntity(clothesId, colorClothesEntity.Name, null, null));

        /// <summary>
        /// Преобразовать размеры в связующую сущность
        /// </summary>
        public static IEnumerable<ClothesSizeGroupCompositeEntity> SizeGroupToComposite(IEnumerable<ISizeGroupMainDomain> sizeGroupMainDomains,
                                                                                        int clothesId,
                                                                                        ISizeGroupMainEntityConverter sizeGroupMainEntityConverter) =>
            sizeGroupMainEntityConverter.ToEntities(sizeGroupMainDomains).
            Select(sizeGroupEntity => new ClothesSizeGroupCompositeEntity(clothesId, sizeGroupEntity.Id, null, null));

        /// <summary>
        /// Преобразовать пол одежды в доменную модель
        /// </summary>
        private IResultValue<IGenderDomain> GetGender(GenderEntity? genderEntity) =>
            genderEntity.
            ToResultValueNullCheck(ErrorResultFactory.ValueNotFoundError(genderEntity, GetType())).
            ResultValueBindOk(gender => _genderEntityConverter.FromEntity(gender));

        /// <summary>
        /// Преобразовать тип одежды в доменную модель
        /// </summary>
        private IResultValue<IClothesTypeDomain> GetClothesType(ClothesTypeEntity? clothesTypeEntity) =>
            clothesTypeEntity.
            ToResultValueNullCheck(ErrorResultFactory.ValueNotFoundError(clothesTypeEntity, GetType())).
            ResultValueBindOk(clothesType => _clothesTypeEntityConverter.FromEntity(clothesType));

        /// <summary>
        /// Получить изображения
        /// </summary>
        private IResultCollection<IClothesImageDomain> GetImages(IReadOnlyCollection<ClothesImageEntity>? imageEntities) =>
            imageEntities.
            ToResultValueNullCheck(ErrorResultFactory.ValueNotFoundError(imageEntities, GetType())).
            ToResultCollection().
            ResultCollectionBindOk(images => _clothesImageEntityConverter.FromEntities(images));

        /// <summary>
        /// Получить сущности цвета одежды
        /// </summary>
        private static IResultCollection<ColorEntity> GetColorClothes(IReadOnlyCollection<ClothesColorCompositeEntity> clothesColorEntities) =>
            clothesColorEntities.
            Select(clothesColor => clothesColor.ColorClothes.
                                   ToResultValueNullCheck(ErrorResultFactory.ValueNotFoundError(clothesColorEntities, typeof(ClothesMainEntityConverter)))).
            ToResultCollection();

        /// <summary>
        /// Получить сущности группы размеров
        /// </summary>
        private static IResultCollection<SizeGroupEntity> GetColorClothes(IReadOnlyCollection<ClothesSizeGroupCompositeEntity> clothesSizeGroupEntities) =>
            clothesSizeGroupEntities.
            Select(sizeGroup => sizeGroup.SizeGroup.
                                ToResultValueNullCheck(ErrorResultFactory.ValueNotFoundError(clothesSizeGroupEntities, typeof(ClothesMainEntityConverter)))).
            ToResultCollection();
    }
}