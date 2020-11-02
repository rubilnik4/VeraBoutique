using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomain;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomain;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Base;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.ClothesEntity;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.ClothesTypeEntity;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes
{
    using ClothesInformationFunc = Func<IClothesShortDomain, IGenderDomain, IClothesTypeFullDomain, 
                                        IEnumerable<IColorClothesDomain>, IEnumerable<ISizeGroupDomain>, 
                                        IClothesFullDomain>;

    /// <summary>
    /// Преобразования модели информации об одежде в модель базы данных
    /// </summary>
    public class ClothesInformationEntityConverter : 
        EntityConverter<int, IClothesFullDomain, IClothesEntity, ClothesInformationEntity>,
        IClothesInformationEntityConverter
    {
        public ClothesInformationEntityConverter(IClothesShortEntityConverter clothesShortEntityConverter,
                                                 IGenderEntityConverter genderEntityConverter,
                                                 IClothesTypeEntityConverter clothesTypeEntityConverter,
                                                 IColorClothesEntityConverter colorClothesEntityConverter,
                                                 ISizeGroupEntityConverter sizeGroupEntityConverter)
        {
            _clothesShortEntityConverter = clothesShortEntityConverter;
            _genderEntityConverter = genderEntityConverter;
            _clothesTypeEntityConverter = clothesTypeEntityConverter;
            _colorClothesEntityConverter = colorClothesEntityConverter;
            _sizeGroupEntityConverter = sizeGroupEntityConverter;
        }

        /// <summary>
        /// Преобразования модели одежды в модель базы данных
        /// </summary>
        private readonly IClothesShortEntityConverter _clothesShortEntityConverter;

        /// <summary>
        /// Преобразования модели вида одежды в модель базы данных
        /// </summary>
        private readonly IGenderEntityConverter _genderEntityConverter;

        /// <summary>
        /// Преобразования модели вида одежды в модель базы данных
        /// </summary>
        private readonly IClothesTypeEntityConverter _clothesTypeEntityConverter;

        /// <summary>
        /// Преобразования модели цвета одежды в модель базы данных
        /// </summary>
        private readonly IColorClothesEntityConverter _colorClothesEntityConverter;

        /// <summary>
        /// Преобразования модели категории одежды в модель базы данных
        /// </summary>
        private readonly ISizeGroupEntityConverter _sizeGroupEntityConverter;

        /// <summary>
        /// Преобразовать категорию одежды из модели базы данных
        /// </summary>
        public override IResultValue<IClothesFullDomain> FromEntity(IClothesEntity clothesEntity) =>
            GetClothesInformationFunc(clothesEntity.Description).
            ResultCurryOkBind(_clothesShortEntityConverter.FromEntity(clothesEntity)).
            ResultCurryOkBind(GetGender(clothesEntity.GenderEntity)).
            ResultCurryOkBind(GetClothedType(clothesEntity.ClothesTypeEntity)).
            ResultCurryOkBind(ColorClothesDomainsFromComposite(clothesEntity.ClothesColorCompositeEntities)).
            ResultCurryOkBind(SizeGroupDomainsFromComposite(clothesEntity.ClothesSizeGroupCompositeEntities)).
            ResultValueOk(func => func.Invoke());
        
        /// <summary>
        /// Преобразовать категорию одежды в модель базы данных
        /// </summary>
        public override ClothesInformationEntity ToEntity(IClothesFullDomain clothesFullDomain) =>
            new ClothesInformationEntity(_clothesShortEntityConverter.ToEntity(clothesFullDomain),
                                         clothesFullDomain.Description,
                                         _genderEntityConverter.ToEntity(clothesFullDomain.Gender),
                                         _clothesTypeEntityConverter.ToEntity(clothesFullDomain.ClothesTypeFull),
                                         ColorClothesToCompositeEntities(clothesFullDomain.Colors, clothesFullDomain.Id),
                                         SizeGroupToCompositeEntities(clothesFullDomain.SizeGroups, clothesFullDomain.Id));

        /// <summary>
        /// Функция получения информации об одежде
        /// </summary>
        private static IResultValue<ClothesInformationFunc> GetClothesInformationFunc(string description) =>
            new ResultValue<ClothesInformationFunc>(
                (clothesShort, gender, clothesType, colors, sizes) => new ClothesFullDomain(clothesShort, description,
                                                                                                   gender, clothesType, colors, sizes));

        /// <summary>
        /// Преобразовать связующую сущность в коллекцию цветов
        /// </summary>
        private IResultCollection<IColorClothesDomain> ColorClothesDomainsFromComposite(IEnumerable<ClothesColorCompositeEntity>? clothesColorCompositeEntities) =>
            clothesColorCompositeEntities.
            ToResultValueNullCheck(ConverterErrors.ValueNotFoundError(nameof(clothesColorCompositeEntities))).
            ResultValueBindOkToCollection(GetColorClothes).
            ResultCollectionBindOk(colorClothesEntities => _colorClothesEntityConverter.FromEntities(colorClothesEntities));

        /// <summary>
        /// Преобразовать связующую сущность в коллекцию размеров
        /// </summary>
        private IResultCollection<ISizeGroupDomain> SizeGroupDomainsFromComposite(IEnumerable<ClothesSizeGroupCompositeEntity>? clothesSizeCompositeEntities) =>
            clothesSizeCompositeEntities.
            ToResultValueNullCheck(ConverterErrors.ValueNotFoundError(nameof(clothesSizeCompositeEntities))).
            ResultValueBindOkToCollection(GetColorClothes).
            ResultCollectionBindOk(sizeGroupEntities => _sizeGroupEntityConverter.FromEntities(sizeGroupEntities));

        /// <summary>
        /// Преобразовать цвета в связующую сущность
        /// </summary>
        private IEnumerable<ClothesColorCompositeEntity> ColorClothesToCompositeEntities(IEnumerable<IColorClothesDomain> colorClothesDomains, 
                                                                                         int clothesId) =>
            _colorClothesEntityConverter.ToEntities(colorClothesDomains).
            Select(colorClothesEntity => new ClothesColorCompositeEntity(clothesId, colorClothesEntity.Name, null,
                                                                         new ColorClothesEntity(colorClothesEntity.Name)));

        /// <summary>
        /// Преобразовать размеры в связующую сущность
        /// </summary>
        private IEnumerable<ClothesSizeGroupCompositeEntity> SizeGroupToCompositeEntities(IEnumerable<ISizeGroupDomain> sizeGroupDomains,
                                                                                          int clothesId) =>
            _sizeGroupEntityConverter.ToEntities(sizeGroupDomains).
            Select(sizeGroupEntity => new ClothesSizeGroupCompositeEntity(clothesId, sizeGroupEntity.ClothesSizeType, sizeGroupEntity.SizeNormalize,
                                                                          null, sizeGroupEntity));

        /// <summary>
        /// Преобразовать пол одежды в доменную модель
        /// </summary>
        private IResultValue<IGenderDomain> GetGender(IGenderEntity? genderEntity) =>
            genderEntity.
            ToResultValueNullCheck(ConverterErrors.ValueNotFoundError(nameof(genderEntity))).
            ResultValueBindOk(gender => _genderEntityConverter.FromEntity(gender));

        /// <summary>
        /// Преобразовать тип одежды в доменную модель
        /// </summary>
        private IResultValue<IClothesTypeFullDomain> GetClothedType(IClothesTypeEntity? clothesTypeEntity) =>
            clothesTypeEntity.
            ToResultValueNullCheck(ConverterErrors.ValueNotFoundError(nameof(clothesTypeEntity))).
            ResultValueBindOk(clothesType => _clothesTypeEntityConverter.FromEntity(clothesType));

        /// <summary>
        /// Получить сущности цвета одежды
        /// </summary>
        private static IResultCollection<IColorClothesEntity> GetColorClothes(IEnumerable<ClothesColorCompositeEntity> clothesColorEntities) =>
            clothesColorEntities.
            Select(clothesColor => clothesColor.ColorClothesEntity.
                                   ToResultValueNullCheck(ConverterErrors.ValueNotFoundError(nameof(clothesColor.ColorClothesEntity)))).
            ToResultCollection();

        /// <summary>
        /// Получить сущности группы размеров
        /// </summary>
        private static IResultCollection<ISizeGroupEntity> GetColorClothes(IEnumerable<ClothesSizeGroupCompositeEntity> clothesSizeGroupEntities) =>
            clothesSizeGroupEntities.
            Select(sizeGroup => sizeGroup.SizeGroupEntity.
                                ToResultValueNullCheck(ConverterErrors.ValueNotFoundError(nameof(sizeGroup.SizeGroupEntity)))).
            ToResultCollection();
    }
}