using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;

namespace BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.ClothesTypeEntities
{
    /// <summary>
    /// Преобразования модели основных данных вида одежды в модель базы данных
    /// </summary>
    public interface IClothesTypeShortEntityConverter : 
        IEntityConverter<string, IClothesTypeShortDomain, ClothesTypeShortEntity>
    { }
}