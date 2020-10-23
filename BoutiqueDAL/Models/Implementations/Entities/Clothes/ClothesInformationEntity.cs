using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes
{
    /// <summary>
    /// Одежда. Информация. Сущность базы данных
    /// </summary>
    public class ClothesInformationEntity : ClothesInformation, IClothesInformationEntity
    {
        public ClothesInformationEntity(IClothesShort clothesShort, string description,
                                        GenderType genderType, string clothesTypeName)
            : this(clothesShort.Id, clothesShort.Name, clothesShort.Price, clothesShort.Image,
                   description, genderType, clothesTypeName)
        { }

        public ClothesInformationEntity(int id, string name, decimal price, byte[]? image,
                                        string description, GenderType genderType, string clothesTypeName)
            : this(id, name,price, image, description,
                   genderType, null, clothesTypeName, null,
                   Enumerable.Empty<ClothesColorCompositeEntity>(),
                   Enumerable.Empty<ClothesSizeGroupCompositeEntity>())
        { }

        public ClothesInformationEntity(IClothesShort clothesShort, string description,
                                        GenderEntity genderEntity,
                                        ClothesTypeEntity clothesTypeEntity,
                                        IEnumerable<ClothesColorCompositeEntity> clothesColorCompositeEntities,
                                        IEnumerable<ClothesSizeGroupCompositeEntity> clothesSizeGroupCompositeEntities)
       : this(clothesShort.Id, clothesShort. Name, clothesShort.Price, clothesShort.Image, description, 
              genderEntity.GenderType, genderEntity, clothesTypeEntity.Name, clothesTypeEntity,
              clothesColorCompositeEntities, clothesSizeGroupCompositeEntities)
        { }

        public ClothesInformationEntity(int id, string name, decimal price, byte[]? image,
                                        string description,
                                        GenderType genderType, GenderEntity? genderEntity,
                                        string clothesTypeName, ClothesTypeEntity? clothesTypeEntity,
                                        IEnumerable<ClothesColorCompositeEntity> clothesColorCompositeEntities,
                                        IEnumerable<ClothesSizeGroupCompositeEntity> clothesSizeGroupCompositeEntities)
          : base(id, name, description, price, image)
        {
            GenderType = genderType;
            GenderEntity = genderEntity;
            ClothesTypeName = clothesTypeName;
            ClothesTypeEntity = clothesTypeEntity;
            ClothesColorCompositeEntities = clothesColorCompositeEntities.ToList();
            ClothesSizeGroupCompositeEntities = clothesSizeGroupCompositeEntities.ToList();
        }

        /// <summary>
        /// Идентификатор связующей сущности типа одежды
        /// </summary>
        public GenderType GenderType { get; }

        /// <summary>
        /// Связующая сущность типа одежды
        /// </summary>
        public GenderEntity? GenderEntity { get; }

        /// <summary>
        /// Идентификатор связующей сущности типа одежды
        /// </summary>
        public string ClothesTypeName { get; }

        /// <summary>
        /// Связующая сущность типа одежды
        /// </summary>
        public ClothesTypeEntity? ClothesTypeEntity { get; }

        /// <summary>
        /// Связующая сущность одежды и цвета
        /// </summary>
        public IReadOnlyCollection<ClothesColorCompositeEntity> ClothesColorCompositeEntities { get; }

        /// <summary>
        /// Связующая сущность одежды и цвета
        /// </summary>
        public IReadOnlyCollection<ClothesSizeGroupCompositeEntity> ClothesSizeGroupCompositeEntities { get; }
    }
}