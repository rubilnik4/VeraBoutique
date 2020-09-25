using System.Collections.Generic;
using System.Linq;
using BoutiqueCommon.Models.Enums.Clothes;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes
{
    /// <summary>
    /// Связующая сущность пола и вида одежды
    /// </summary>
    public class ClothesTypeGenderEntity
    {
        /// <summary>
        /// Конструктор для базы данных с отсутствующими сущностями
        /// </summary>
        public ClothesTypeGenderEntity(string clothesTypeId, GenderType genderTypeId)
            : this(clothesTypeId, null, genderTypeId, null)
        {
            ClothesTypeId = clothesTypeId;
            GenderTypeId = genderTypeId;
        }

        public ClothesTypeGenderEntity(string clothesTypeId, ClothesTypeEntity? clothesTypeEntity,
                                       GenderType genderTypeId, GenderEntity? genderEntity)
        {
            ClothesTypeId = clothesTypeId;
            ClothesTypeEntity = clothesTypeEntity;
            GenderTypeId = genderTypeId;
            GenderEntity = genderEntity;
        }

        /// <summary>
        /// Идентификатор вида одежды
        /// </summary>
        public string ClothesTypeId { get; }

        /// <summary>
        /// Вид одежды
        /// </summary>
        public ClothesTypeEntity? ClothesTypeEntity { get; }

        /// <summary>
        /// Идентификатор пола одежды
        /// </summary>
        public GenderType GenderTypeId { get; }

        /// <summary>
        /// Пол одежды
        /// </summary>
        public GenderEntity? GenderEntity { get; }
    }
}