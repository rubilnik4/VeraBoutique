using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes
{
    /// <summary>
    /// Преобразования модели категории одежды в модель базы данных
    /// </summary>
    public class CategoryEntityConverter : EntityConverter<string, ICategoryDomain, ICategoryEntity, CategoryEntity>, 
                                           ICategoryEntityConverter
    {
        /// <summary>
        /// Преобразовать категорию одежды из модели базы данных
        /// </summary>
        public override ICategoryDomain FromEntity(ICategoryEntity categoryEntity) =>
            new CategoryDomain(categoryEntity.Name);

        /// <summary>
        /// Преобразовать категорию одежды в модель базы данных
        /// </summary>
        public override CategoryEntity ToEntity(ICategoryDomain genderDomain) =>
            new CategoryEntity(genderDomain.Name);
    }
}