using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.SizeGroupEntities;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Models.Interfaces.Entities.Clothes.Composite
{
    /// <summary>
    /// Связующая сущность размера одежды с группой
    /// </summary>
    public interface ISizeGroupCompositeEntity: IEntityModel<((SizeType, string), int)>
    {
        /// <summary>
        /// Тип размера одежды
        /// </summary>
        SizeType SizeType { get; }

        /// <summary>
        /// Размер
        /// </summary>
        string SizeName { get; }

        /// <summary>
        /// Идентификатор размера одежды
        /// </summary>
        int SizeGroupId { get; }

        /// <summary>
        /// Размер одежды
        /// </summary>
        SizeEntity? Size { get; }

        /// <summary>
        /// Группа размеров одежды
        /// </summary>
        SizeGroupEntity? SizeGroup { get; }
    }
}