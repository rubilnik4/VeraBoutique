using System.Linq;
using BoutiqueCommon.Models.Common.Implementations.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.ClothesTypeEntities;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes.ClothesTypeEntities
{
    /// <summary>
    /// Вид одежды. Базовая информация. Сущность базы данных
    /// </summary>
    public abstract class ClothesTypeEntity: ClothesType, IClothesTypeEntity
    {
        protected ClothesTypeEntity(string name, string categoryName)
            :this(name, categoryName, null)
        { }

        protected ClothesTypeEntity(string name, CategoryEntity category)
            :this(name, category.Name, category)
        { }

        protected ClothesTypeEntity(string name, string categoryName, CategoryEntity? category)
            :base(name)
        {
            CategoryName = categoryName;
            Category = category;
        }

        /// <summary>
        /// Идентификатор связующей сущности категории одежды
        /// </summary>
        public string CategoryName { get; }

        /// <summary>
        /// Связующая сущность категории одежды
        /// </summary>
        public CategoryEntity? Category { get; }
    }
}