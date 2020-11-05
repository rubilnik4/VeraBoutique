using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Models.Interfaces.Entities.Clothes.Composite
{
    /// <summary>
    /// Связующая сущность размера одежды с группой
    /// </summary>
    public interface ISizeGroupCompositeEntity: IEntityModel<((SizeType, string), (ClothesSizeType, int))>
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
        /// Тип одежды для определения размера
        /// </summary>
        ClothesSizeType ClothesSizeType { get; }

        /// <summary>
        /// Номинальное значение размера
        /// </summary>
        int SizeNormalize { get; }

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