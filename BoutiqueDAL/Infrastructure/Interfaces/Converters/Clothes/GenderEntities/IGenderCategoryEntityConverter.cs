using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes.GenderEntities
{
    /// <summary>
    /// Преобразования модели типа пола с категорией в модель базы данных
    /// </summary>
    public interface IGenderCategoryEntityConverter : IEntityConverter<GenderType, IGenderCategoryDomain, GenderEntity>
    { }
}