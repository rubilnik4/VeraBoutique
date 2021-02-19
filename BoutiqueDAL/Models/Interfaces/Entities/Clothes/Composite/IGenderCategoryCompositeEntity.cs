using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Models.Interfaces.Entities.Clothes.Composite
{
    /// <summary>
    /// Связующая сущность типа пола и категории одежды
    /// </summary>
    public interface IGenderCategoryCompositeEntity : IEntityModel<(GenderType, string)>
    {
        /// <summary>
        /// Идентификатор типа пола
        /// </summary>
        GenderType GenderType { get; }

        /// <summary>
        /// Идентификатор категории одежды
        /// </summary>
        string CategoryName { get; }

        /// <summary>
        /// Тип пола
        /// </summary>
        GenderEntity? Gender { get; }

        /// <summary>
        /// Категория одежды
        /// </summary>
        CategoryEntity? Category { get; }
    }
}