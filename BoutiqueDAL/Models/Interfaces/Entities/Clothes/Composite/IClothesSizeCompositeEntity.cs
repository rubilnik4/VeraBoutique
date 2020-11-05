using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesEntities;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Models.Interfaces.Entities.Clothes.Composite
{
    /// <summary>
    /// Связующая сущность одежды с размером
    /// </summary>
    public interface IClothesSizeCompositeEntity : IEntityModel<(int, (ClothesSizeType, int))>
    {
        /// <summary>
        /// Идентификатор одежды
        /// </summary>
        int ClothesId { get; }

        /// <summary>
        /// Тип одежды для определения размера
        /// </summary>
        ClothesSizeType ClothesSizeType { get; }

        /// <summary>
        /// Номинальное значение размера
        /// </summary>
        int SizeNormalize { get; }

        /// <summary>
        /// Одежда. Информация
        /// </summary>
        ClothesEntity? Clothes { get; }

        /// <summary>
        /// Группа размеров одежды
        /// </summary>
        SizeGroupEntity? SizeGroup { get; }
    }
}