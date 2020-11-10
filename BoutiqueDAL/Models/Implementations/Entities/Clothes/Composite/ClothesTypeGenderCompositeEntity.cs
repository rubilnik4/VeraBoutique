using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.Composite;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite
{
    /// <summary>
    /// Связующая сущность пола и вида одежды
    /// </summary>
    public class ClothesTypeGenderCompositeEntity: IClothesTypeGenderCompositeEntity
    {
        /// <summary>
        /// Конструктор для базы данных с отсутствующими сущностями
        /// </summary>
        public ClothesTypeGenderCompositeEntity(string clothesTypeName, GenderType genderType)
            : this(clothesTypeName, genderType, null, null)
        { }

        public ClothesTypeGenderCompositeEntity(string clothesTypeName, GenderType genderType,
                                                ClothesTypeEntity? clothesType, GenderEntity? gender)
        {
            ClothesTypeName = clothesTypeName;
            ClothesType = clothesType;
            GenderType = genderType;
            Gender = gender;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public (string, GenderType) Id => (ClothesTypeName,  GenderType);

        /// <summary>
        /// Идентификатор вида одежды
        /// </summary>
        public string ClothesTypeName { get; }
        
        /// <summary>
        /// Идентификатор пола одежды
        /// </summary>
        public GenderType GenderType { get; }

        /// <summary>
        /// Вид одежды
        /// </summary>
        public ClothesTypeEntity? ClothesType { get; }

        /// <summary>
        /// Пол одежды
        /// </summary>
        public GenderEntity? Gender { get; }
    }
}