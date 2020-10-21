using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes
{
    /// <summary>
    /// Одежда. Информация. Сущность базы данных
    /// </summary>
    public class ClothesInformationEntity : ClothesInformation, IClothesInformationEntity
    {
        public ClothesInformationEntity(int id, string name, string description, 
                                        decimal price, byte[]? image)
            : this(id, name, description, 
                   Enumerable.Empty<ClothesColorCompositeEntity>(),
                   Enumerable.Empty<ClothesSizeGroupCompositeEntity>(),
                   price, image)
        { }

        public ClothesInformationEntity(int id, string name, string description,
                                        IEnumerable<ClothesColorCompositeEntity> clothesColorCompositeEntities,
                                        IEnumerable<ClothesSizeGroupCompositeEntity> clothesSizeGroupCompositeEntities,
                                        decimal price, byte[]? image)
          : base(id, name, description, price, image)
        {
            ClothesColorCompositeEntities = clothesColorCompositeEntities.ToList();
            ClothesSizeGroupCompositeEntities = clothesSizeGroupCompositeEntities.ToList();
        }

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