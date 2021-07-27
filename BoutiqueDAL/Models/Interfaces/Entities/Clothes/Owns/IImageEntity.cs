using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Models.Interfaces.Entities.Clothes.Owns
{
    /// <summary>
    /// Сущность изображения
    /// </summary>
    public interface IImageEntity : IEntityModel<int>
    {
        /// <summary>
        /// Изображение
        /// </summary>
        byte[] Image { get; }
    }
}