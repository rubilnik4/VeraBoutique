using System;
using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Implementations.Database.Errors;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Base;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValue;
using Functional.Models.Enums;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes
{
    using ClothesInformationFunc = Func<IClothesShortDomain, IGenderDomain, IClothesTypeDomain, 
                                        IEnumerable<IColorClothesDomain>, IEnumerable<ISizeGroupDomain>, 
                                        IClothesInformationDomain>;

    /// <summary>
    /// Преобразования модели информации об одежде в модель базы данных
    /// </summary>
    public class ClothesInformationEntityConverter : 
        EntityConverter<int, IClothesInformationDomain, IClothesInformationEntity, ClothesInformationEntity>,
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
        public override IResultValue<IClothesInformationDomain> FromEntity(IClothesInformationEntity clothesInformationEntity) =>
            GetClothesInformationFunc(clothesInformationEntity.Description).
            ResultCurryOkBind(_clothesShortEntityConverter.FromEntity(clothesInformationEntity)).
            ResultCurryOkBind(GetGender(clothesInformationEntity.GenderEntity)).
            ResultCurryOkBind(GetClothedType(clothesInformationEntity.ClothesTypeEntity)).
            ResultCurryOkBind(ColorClothesDomainsFromComposite(clothesInformationEntity.ClothesColorCompositeEntities)).
            ResultCurryOkBind(SizeGroupDomainsFromComposite(clothesInformationEntity.ClothesSizeGroupCompositeEntities)).
            ResultValueOk(func => func.Invoke());
        
        /// <summary>
        /// Преобразовать категорию одежды в модель базы данных
        /// </summary>
        public override ClothesInformationEntity ToEntity(IClothesInformationDomain clothesInformationDomain) =>
            new ClothesInformationEntity(_clothesShortEntityConverter.ToEntity(clothesInformationDomain),
                                         clothesInformationDomain.Description,
                                         _genderEntityConverter.ToEntity(clothesInformationDomain.Gender),
                                         _clothesTypeEntityConverter.ToEntity(clothesInformationDomain.ClothesType),
                                         ColorClothesToCompositeEntities(clothesInformationDomain.Colors, clothesInformationDomain.Id),
                                         SizeGroupToCompositeEntities(clothesInformationDomain.SizeGroups, clothesInformationDomain.Id));

        /// <summary>
        /// Функция получения информации об одежде
        /// </summary>
        private static IResultValue<ClothesInformationFunc> GetClothesInformationFunc(string description) =>
            new ResultValue<ClothesInformationFunc>((clothesShort, gender, clothesType, colors, sizes) =>
                new ClothesInformationDomain(clothesShort, description, gender, clothesType, colors, sizes));

        /// <summary>
        /// Преобразовать связующую сущность в коллекцию цветов
        /// </summary>
        private IResultCollection<IColorClothesDomain> ColorClothesDomainsFromComposite(IEnumerable<ClothesColorCompositeEntity> clothesColorCompositeEntities) =>
            clothesColorCompositeEntities.
            Select(clothesColorCompositeEntity => clothesColorCompositeEntity.ColorClothesEntity).
            Where(colorClothesEntity => colorClothesEntity != null).
            Select(colorClothesEntity => _colorClothesEntityConverter.FromEntity(colorClothesEntity!)).
            ToResultCollection();

        /// <summary>
        /// Преобразовать связующую сущность в коллекцию размеров
        /// </summary>
        private IResultCollection<ISizeGroupDomain> SizeGroupDomainsFromComposite(IEnumerable<ClothesSizeGroupCompositeEntity> clothesSizeCompositeEntities) =>
            clothesSizeCompositeEntities.
            Select(clothesSizeCompositeEntity => clothesSizeCompositeEntity.SizeGroupEntity).
            Where(sizeGroupEntity => sizeGroupEntity != null).
            Select(sizeGroupEntity => _sizeGroupEntityConverter.FromEntity(sizeGroupEntity!)).
            ToResultCollection();

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
        private IResultValue<IClothesTypeDomain> GetClothedType(IClothesTypeEntity? clothesTypeEntity) =>
            clothesTypeEntity.
            ToResultValueNullCheck(ConverterErrors.ValueNotFoundError(nameof(clothesTypeEntity))).
            ResultValueBindOk(gender => _clothesTypeEntityConverter.FromEntity(clothesTypeEntity));

    }
}