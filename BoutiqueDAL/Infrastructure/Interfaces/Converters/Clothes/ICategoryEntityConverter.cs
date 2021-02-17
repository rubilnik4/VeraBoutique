using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes
{
    /// <summary>
    /// Преобразования модели категории одежды в модель базы данных
    /// </summary>
    public interface ICategoryEntityConverter : IEntityConverter<string, ICategoryDomain, CategoryEntity>
    { }
}