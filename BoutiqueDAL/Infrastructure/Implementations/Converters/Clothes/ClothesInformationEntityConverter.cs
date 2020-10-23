using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes
{
    /// <summary>
    /// Преобразования модели информации об одежде в модель базы данных
    /// </summary>
    public class ClothesInformationEntityConverter : 
        EntityConverter<int, IClothesInformationDomain, IClothesInformationEntity, ClothesInformationEntity>,
        IClothesInformationEntityConverter
    {
        public ClothesInformationEntityConverter(IClothesShortEntityConverter clothesShortEntityConverter,
                                                 IClothesTypeEntityConverter clothesTypeEntityConverter,
                                                 IColorClothesEntityConverter colorClothesEntityConverter,
                                                 ISizeGroupEntityConverter sizeGroupEntityConverter)
        {
            _clothesShortEntityConverter = clothesShortEntityConverter;
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
        public override IClothesInformationDomain FromEntity(IClothesInformationEntity clothesInformationEntity) =>
            new ClothesInformationDomain(_clothesShortEntityConverter.FromEntity(clothesInformationEntity),
                                         clothesInformationEntity.Description,
                                         _clothesTypeEntityConverter.FromEntity(clothesInformationEntity.ClothesTypeEntity!),
                                         ColorClothesDomainsFromComposite(clothesInformationEntity.ClothesColorCompositeEntities),
                                         SizeGroupDomainsFromComposite(clothesInformationEntity.ClothesSizeGroupCompositeEntities));

        /// <summary>
        /// Преобразовать категорию одежды в модель базы данных
        /// </summary>
        public override ClothesInformationEntity ToEntity(IClothesInformationDomain clothesInformationDomain) =>
            new ClothesInformationEntity(_clothesShortEntityConverter.ToEntity(clothesInformationDomain),
                                         clothesInformationDomain.Description,
                                         _clothesTypeEntityConverter.ToEntity(clothesInformationDomain.ClothesType),
                                         ColorClothesToCompositeEntities(clothesInformationDomain.Colors, clothesInformationDomain.Id),
                                         SizeGroupToCompositeEntities(clothesInformationDomain.SizeGroups, clothesInformationDomain.Id));

        /// <summary>
        /// Преобразовать связующую сущность в коллекцию цветов
        /// </summary>
        private IEnumerable<IColorClothesDomain> ColorClothesDomainsFromComposite(IEnumerable<ClothesColorCompositeEntity> clothesColorCompositeEntities) =>
            clothesColorCompositeEntities.
            Select(clothesColorCompositeEntity => clothesColorCompositeEntity.ColorClothesEntity).
            Where(colorClothesEntity => colorClothesEntity != null).
            Select(colorClothesEntity => _colorClothesEntityConverter.FromEntity(colorClothesEntity!));

        /// <summary>
        /// Преобразовать связующую сущность в коллекцию размеров
        /// </summary>
        private IEnumerable<ISizeGroupDomain> SizeGroupDomainsFromComposite(IEnumerable<ClothesSizeGroupCompositeEntity> clothesSizeCompositeEntities) =>
            clothesSizeCompositeEntities.
            Select(clothesSizeCompositeEntity => clothesSizeCompositeEntity.SizeGroupEntity).
            Where(sizeGroupEntity => sizeGroupEntity != null).
            Select(sizeGroupEntity => _sizeGroupEntityConverter.FromEntity(sizeGroupEntity!));

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


    }
}