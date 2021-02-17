using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes.Categories;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Models.Interfaces.Entities.Clothes
{
    /// <summary>
    /// Категория одежды. Сущность базы данных
    /// </summary>
    public interface ICategoryEntity : ICategoryBase, IEntityModel<string>
    {
        /// <summary>
        /// Связующие сущности типа пола и категории одежды
        /// </summary>
        IReadOnlyCollection<GenderCategoryCompositeEntity>? GenderCategoryComposites { get; }
    }
}