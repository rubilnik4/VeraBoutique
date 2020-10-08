using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Base;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes
{
    /// <summary>
    /// Связующая сущность пола и вида одежды
    /// </summary>
    public class ClothesTypeGenderEntity: IClothesTypeGenderEntity
    {
        /// <summary>
        /// Конструктор для базы данных с отсутствующими сущностями
        /// </summary>
        public ClothesTypeGenderEntity(string clothesType, GenderType genderType)
            : this(clothesType, genderType, null, null)
        {
            ClothesType = clothesType;
            GenderType = genderType;
        }

        public ClothesTypeGenderEntity(string clothesType, GenderType genderType,
                                        ClothesTypeEntity? clothesTypeEntity, GenderEntity? genderEntity)
        {
            ClothesType = clothesType;
            ClothesTypeEntity = clothesTypeEntity;
            GenderType = genderType;
            GenderEntity = genderEntity;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public (string, GenderType) Id => (ClothesType,  GenderType);

        /// <summary>
        /// Идентификатор вида одежды
        /// </summary>
        public string ClothesType { get; }
        
        /// <summary>
        /// Идентификатор пола одежды
        /// </summary>
        public GenderType GenderType { get; }

        /// <summary>
        /// Вид одежды
        /// </summary>
        public ClothesTypeEntity? ClothesTypeEntity { get; }

        /// <summary>
        /// Пол одежды
        /// </summary>
        public GenderEntity? GenderEntity { get; }
    }
}