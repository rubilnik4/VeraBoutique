using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Models.Interfaces.Entities.Clothes.Composite
{
    /// <summary>
    /// Связующая сущность пола и вида одежды
    /// </summary>
    public interface IClothesTypeGenderCompositeEntity : IEntityModel<(string, GenderType)>
    {
        /// <summary>
        /// Идентификатор вида одежды
        /// </summary>
        string ClothesTypeName { get; }

        /// <summary>
        /// Идентификатор пола одежды
        /// </summary>
        GenderType GenderType { get; }

        /// <summary>
        /// Вид одежды
        /// </summary>
        ClothesTypeEntity? ClothesType { get; }

        /// <summary>
        /// Пол одежды
        /// </summary>
        GenderEntity? Gender { get; }
    }
}