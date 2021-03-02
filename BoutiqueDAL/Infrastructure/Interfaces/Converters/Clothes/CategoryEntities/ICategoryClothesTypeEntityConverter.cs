using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.CategoryEntities
{
    /// <summary>
    /// Преобразования модели категории одежды с типом в модель базы данных
    /// </summary>
    public interface ICategoryClothesTypeEntityConverter : IEntityConverter<string, ICategoryClothesTypeDomain, CategoryEntity>
    { }
}