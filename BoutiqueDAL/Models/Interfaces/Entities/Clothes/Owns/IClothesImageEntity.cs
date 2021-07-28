using BoutiqueCommon.Models.Common.Interfaces.Clothes.Images;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Models.Interfaces.Entities.Clothes.Owns
{
    /// <summary>
    /// Сущность изображения
    /// </summary>
    public interface IClothesImageEntity : IClothesImageBase, IEntityModel<int>
    { }
}