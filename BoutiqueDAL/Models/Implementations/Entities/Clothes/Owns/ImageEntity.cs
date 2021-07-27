using BoutiqueDAL.Models.Interfaces.Entities.Clothes.Owns;
using Microsoft.EntityFrameworkCore;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes.Owns
{
    /// <summary>
    /// Сущность изображения
    /// </summary>
    [Owned]
    public class ImageEntity: IImageEntity
    {
        public ImageEntity(int id, byte[] image)
        {
            Id = id;
            Image = image;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Изображение
        /// </summary>
        public byte[] Image { get; }
    }
}