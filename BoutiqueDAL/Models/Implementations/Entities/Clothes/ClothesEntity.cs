using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes.Clothes;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes
{
    /// <summary>
    /// Одежда. Информация. Сущность базы данных
    /// </summary>
    public class ClothesEntity : ClothesBase, IClothesEntity
    {
        public ClothesEntity(int id, string name, string description, decimal price, byte[] image,
                             GenderType genderType, string clothesTypeName)
            :this(id, name, description, price, image, genderType, clothesTypeName, null, null, null, null)
        { }

        public ClothesEntity(IClothesBase clothes,
                             GenderType genderType, string clothesTypeName,
                             IEnumerable<ClothesColorCompositeEntity> clothesColorComposites,
                             IEnumerable<ClothesSizeGroupCompositeEntity> clothesSizeGroupComposites)
           : this(clothes.Id, clothes.Name, clothes.Description, clothes.Price, clothes.Image,
                  genderType, clothesTypeName, null, null,
                  clothesColorComposites, clothesSizeGroupComposites)
        { }

        public ClothesEntity(IClothesBase clothes,
                             GenderEntity gender, ClothesTypeEntity clothesType,
                             IEnumerable<ClothesColorCompositeEntity> clothesColorComposites,
                             IEnumerable<ClothesSizeGroupCompositeEntity> clothesSizeGroupComposites)
            : this(clothes.Id, clothes.Name, clothes.Description, clothes.Price, clothes.Image,
                   gender.GenderType,  clothesType.Name, gender, clothesType,
                   clothesColorComposites, clothesSizeGroupComposites)
        { }

        public ClothesEntity(int id, string name, string description, decimal price, byte[] image,
                             GenderType genderType, string clothesTypeName,
                             GenderEntity? gender, ClothesTypeEntity? clothesType,
                             IEnumerable<ClothesColorCompositeEntity>? clothesColorComposites,
                             IEnumerable<ClothesSizeGroupCompositeEntity>? clothesSizeGroupComposites)
          : base(id, name, description, price, image, genderType, clothesTypeName)
        {
            Gender = gender;
            ClothesType = clothesType;
            ClothesColorComposites = clothesColorComposites?.ToList();
            ClothesSizeGroupComposites = clothesSizeGroupComposites?.ToList();
        }
        
        /// <summary>
        /// Связующая сущность типа одежды
        /// </summary>
        public GenderEntity? Gender { get; }

        /// <summary>
        /// Связующая сущность типа одежды
        /// </summary>
        public ClothesTypeEntity? ClothesType { get; }

        /// <summary>
        /// Связующая сущность одежды и цвета
        /// </summary>
        public IReadOnlyCollection<ClothesColorCompositeEntity>? ClothesColorComposites { get; }

        /// <summary>
        /// Связующая сущность одежды и цвета
        /// </summary>
        public IReadOnlyCollection<ClothesSizeGroupCompositeEntity>? ClothesSizeGroupComposites { get; }
    }
}