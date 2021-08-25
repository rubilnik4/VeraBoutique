using BoutiqueCommon.Models.Domain.Implementations.Clothes.CategoryDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.CategoryDomains;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes.CategoryTransfers;
using BoutiqueDTO.Models.Implementations.Clothes.CategoryTransfers;
using Functional.FunctionalExtensions.Sync;
using Functional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using Functional.Models.Interfaces.Results;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes.CategoryTransfers
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
            new CategoryTransfer(categoryDomain);

        /// <summary>
        /// Преобразовать категории одежды из трансферной модели
        /// </summary>
        public override IResultValue<ICategoryDomain> FromTransfer(CategoryTransfer categoryTransfer) =>
            new CategoryDomain(categoryTransfer).
            ToResultValue();
    }
}