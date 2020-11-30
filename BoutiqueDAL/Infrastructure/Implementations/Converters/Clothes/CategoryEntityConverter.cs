using System.Linq;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Infrastructure.Implementations.Converters.Base;
using BoutiqueDAL.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Implementations.Converters.Clothes
{
    /// <summary>
    /// Преобразования модели категории одежды в модель базы данных
    /// </summary>
    public class CategoryEntityConverter : EntityConverter<string, ICategoryDomain, CategoryEntity>, 
                                           ICategoryEntityConverter
    {
        /// <summary>
        /// Преобразовать категорию одежды из модели базы данных
        /// </summary>
        public override IResultValue<ICategoryDomain> FromEntity(CategoryEntity categoryEntity) =>
            new CategoryDomain(categoryEntity).
            Map(category => new ResultValue<ICategoryDomain>(category));

        /// <summary>
        /// Преобразовать категорию одежды в модель базы данных
        /// </summary>
        public override CategoryEntity ToEntity(ICategoryDomain categoryDomain) =>
            new CategoryEntity(categoryDomain);
    }
}