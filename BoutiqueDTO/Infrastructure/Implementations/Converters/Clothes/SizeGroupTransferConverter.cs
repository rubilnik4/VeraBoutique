using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;

namespace BoutiqueDTO.Infrastructure.Implementations.Converters.Clothes
{
    /// <summary>
    /// Конвертер группы размеров одежды в трансферную модель
    /// </summary>
    public class SizeGroupTransferConverter : TransferConverter<string, ISizeGroupDomain, SizeGroupTransfer>,
                                                     ISizeGroupTransferConverter
    {
        /// <summary>
        /// Преобразовать группу размеров одежды в трансферную модель
        /// </summary>
        public override SizeGroupTransfer ToTransfer(ISizeGroupDomain sizeGroupDomain) =>
            new SizeGroupTransfer(sizeGroupDomain.ClothesSizeBase, sizeGroupDomain.Sizes);

        /// <summary>
        /// Преобразовать размеры одежды из трансферной модели
        /// </summary>
        public override ISizeGroupDomain FromTransfer(SizeGroupTransfer sizeGroupTransfer) =>
            new SizeGroupDomain(sizeGroupTransfer.ClothesSizeBase, sizeGroupTransfer.Sizes);
    }
}