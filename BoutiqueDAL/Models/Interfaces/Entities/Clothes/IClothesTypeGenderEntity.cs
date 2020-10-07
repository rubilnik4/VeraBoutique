using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Models.Interfaces.Entities.Clothes
{
    /// <summary>
    /// Связующая сущность пола и вида одежды
    /// </summary>
    public interface IClothesTypeGenderEntity : IEntityModel<(string, GenderType)>
    {
        /// <summary>
        /// Идентификатор вида одежды
        /// </summary>
        string ClothesType { get; }

        /// <summary>
        /// Идентификатор пола одежды
        /// </summary>
        GenderType GenderType { get; }

        /// <summary>
        /// Вид одежды
        /// </summary>
        ClothesTypeEntity? ClothesTypeEntity { get; }

        /// <summary>
        /// Пол одежды
        /// </summary>
        GenderEntity? GenderEntity { get; }
    }
}