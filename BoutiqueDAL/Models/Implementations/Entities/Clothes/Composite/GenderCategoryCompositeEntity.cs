using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.Composite;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite
{
    /// <summary>
    /// Связующая сущность типа пола и категории одежды
    /// </summary>
    public class GenderCategoryCompositeEntity : IGenderCategoryCompositeEntity
    {
        public GenderCategoryCompositeEntity(GenderType genderType, string categoryName)
            : this(genderType, categoryName, null, null)
        { }

        public GenderCategoryCompositeEntity(GenderType genderType, string categoryName,
                                             GenderEntity? gender, CategoryEntity? category)
        {
            GenderType = genderType;
            CategoryName = categoryName;
            Gender = gender;
            Category = category;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public (GenderType, string) Id => (GenderType, CategoryName);

        /// <summary>
        /// Идентификатор типа пола
        /// </summary>
        public GenderType GenderType { get; }

        /// <summary>
        /// Идентификатор категории одежды
        /// </summary>
        public string CategoryName { get; }

        /// <summary>
        /// Тип пола
        /// </summary>
        public GenderEntity? Gender { get; }

        /// <summary>
        /// Категория одежды
        /// </summary>
        public CategoryEntity? Category { get; }
    }
}