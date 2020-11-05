using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Models.Interfaces.Entities.Clothes.ClothesTypeEntities
{
    /// <summary>
    /// Вид одежды. Базовая информация. Сущность базы данных
    /// </summary>
    public interface IClothesTypeEntity : IClothesType, IEntityModel<string>
    {
        /// <summary>
        /// Идентификатор связующей сущности категории одежды
        /// </summary>
        string CategoryName { get; }

        /// <summary>
        /// Связующая сущность категории одежды
        /// </summary>
        CategoryEntity? Category { get; }
    }
}