using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes
{
    /// <summary>
    /// Конвертер категорий одежды в трансферную модель
    /// </summary>
    public class CategoryTransferConverter : TransferConverter<string, ICategoryDomain, CategoryTransfer>,
                                             ICategoryTransferConverter
    {
        /// <summary>
        /// Преобразовать категории одежды в трансферную модель
        /// </summary>
        public override CategoryTransfer ToTransfer(ICategoryDomain categoryDomain) =>
            new CategoryTransfer(categoryDomain.Name);

        /// <summary>
        /// Преобразовать категории одежды из трансферной модели
        /// </summary>
        public override ICategoryDomain FromTransfer(CategoryTransfer categoryTransfer) =>
            new CategoryDomain(categoryTransfer.Name);
    }
}