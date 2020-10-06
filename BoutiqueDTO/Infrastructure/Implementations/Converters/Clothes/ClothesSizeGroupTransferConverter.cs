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
    public class ClothesSizeGroupTransferConverter : TransferConverter<string, IClothesSizeGroupDomain, ClothesSizeGroupTransfer>,
                                                     IClothesSizeGroupTransferConverter
    {
        /// <summary>
        /// Преобразовать группу размеров одежды в трансферную модель
        /// </summary>
        public override ClothesSizeGroupTransfer ToTransfer(IClothesSizeGroupDomain clothesSizeGroupDomain) =>
            new ClothesSizeGroupTransfer(clothesSizeGroupDomain.ClothesSizeBase, clothesSizeGroupDomain.Sizes);

        /// <summary>
        /// Преобразовать размеры одежды из трансферной модели
        /// </summary>
        public override IClothesSizeGroupDomain FromTransfer(ClothesSizeGroupTransfer clothesSizeGroupTransfer) =>
            new ClothesSizeGroupDomain(clothesSizeGroupTransfer.ClothesSizeBase, clothesSizeGroupTransfer.Sizes);
    }
}