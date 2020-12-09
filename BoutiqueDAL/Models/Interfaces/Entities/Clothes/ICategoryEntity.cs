using System.Collections.Generic;
using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities;
using BoutiqueDAL.Models.Interfaces.Entities.Base;

namespace BoutiqueDAL.Models.Interfaces.Entities.Clothes
{
    /// <summary>
    /// Категория одежды. Сущность базы данных
    /// </summary>
    public interface ICategoryEntity : ICategoryBase, IEntityModel<string>
    {
        /// <summary>
        /// Связующие сущности категории и вида одежды
        /// </summary>
        public IReadOnlyCollection<ClothesTypeEntity>? ClothesTypes { get; }
    }
}