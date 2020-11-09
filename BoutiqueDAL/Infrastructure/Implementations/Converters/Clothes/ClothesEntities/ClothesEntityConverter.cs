using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Infrastructure.Implementation.Errors;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.ClothesTypeDomains;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesEntities;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesEntities;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.ClothesEntities;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.ClothesTypeEntities;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultCollection;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes.ClothesEntities
{
    using ClothesInformationFunc = Func<IClothesShortDomain, IGenderDomain, IClothesTypeDomain, 
                                        IEnumerable<IColorClothesDomain>, IEnumerable<ISizeGroupDomain>, 
                                        IClothesDomain>;

    /// <summary>
    /// Преобразования модели информации об одежде в модель базы данных
    /// </summary>
    public class ClothesEntityConverter : EntityConverter<int, IClothesDomain, IClothesEntity, ClothesEntity>,
                                          IClothesEntityConverter
    {
        public ClothesEntityConverter(IClothesShortEntityConverter clothesShortEntityConverter,
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
        public override IResultValue<IClothesDomain> FromEntity(IClothesEntity clothesEntity) =>
            GetClothesInformationFunc(clothesEntity.Description).
            ResultCurryOkBind(_clothesShortEntityConverter.FromEntity(clothesEntity)).
            ResultCurryOkBind(GetGender(clothesEntity.Gender)).
            ResultCurryOkBind(GetClothedType(clothesEntity.ClothesType)).
            ResultCurryOkBind(ColorClothesDomainsFromComposite(clothesEntity.ClothesColorComposites)).
            ResultCurryOkBind(SizeGroupDomainsFromComposite(clothesEntity.ClothesSizeGroupComposites)).
            ResultValueOk(func => func.Invoke());
        
        /// <summary>
        /// Преобразовать категорию одежды в модель базы данных
        /// </summary>
        public override ClothesEntity ToEntity(IClothesDomain clothesDomain) =>
            new ClothesEntity(_clothesShortEntityConverter.ToEntity(clothesDomain),
                              clothesDomain.Description,
                              _genderEntityConverter.ToEntity(clothesDomain.),
                              _clothesTypeEntityConverter.ToEntity(clothesDomain.ClothesTypeFull),
                              ColorClothesToCompositeEntities(clothesDomain.Colors, clothesDomain.Id),
                              SizeGroupToCompositeEntities(clothesDomain.SizeGroups, clothesDomain.Id));

        /// <summary>
        /// Функция получения информации об одежде
        /// </summary>
        private static IResultValue<ClothesInformationFunc> GetClothesInformationFunc(string description) =>
            new ResultValue<ClothesInformationFunc>(
                (clothesShort, gender, clothesType, colors, sizes) => new ClothesDomain(clothesShort, description,
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
            Select(clothesColor => clothesColor.ColorClothes.
                                   ToResultValueNullCheck(ConverterErrors.ValueNotFoundError(nameof(clothesColor.ColorClothes)))).
            ToResultCollection();

        /// <summary>
        /// Получить сущности группы размеров
        /// </summary>
        private static IResultCollection<ISizeGroupEntity> GetColorClothes(IEnumerable<ClothesSizeGroupCompositeEntity> clothesSizeGroupEntities) =>
            clothesSizeGroupEntities.
            Select(sizeGroup => sizeGroup.SizeGroup.
                                ToResultValueNullCheck(ConverterErrors.ValueNotFoundError(nameof(sizeGroup.SizeGroup)))).
            ToResultCollection();
    }
}