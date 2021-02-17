using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDAL.Models.Interfaces.Entities.Clothes.Composite;

namespace BoutiqueDAL.Models.Implementations.Entities.Clothes.Composite
{
    /// <summary>
    /// Связующая сущность типа пола и категории одежды
    /// </summary>
    public class GenderCategoryCompositeEntity : IGenderCategoryCompositeEntity
    {
        public GenderCategoryCompositeEntity(GenderType genderId, string categoryId)
            : this(genderId, categoryId, null, null)
        { }

        public GenderCategoryCompositeEntity(GenderType genderId, string categoryId,
                                             GenderEntity? gender, CategoryEntity? category)
        {
            GenderId = genderId;
            CategoryId = categoryId;
            Gender = gender;
            Category = category;
        }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public (GenderType, string) Id => (GenderId, CategoryId);

        /// <summary>
        /// Идентификатор типа пола
        /// </summary>
        public GenderType GenderId { get; }

        /// <summary>
        /// Идентификатор категории одежды
        /// </summary>
        public string CategoryId { get; }

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