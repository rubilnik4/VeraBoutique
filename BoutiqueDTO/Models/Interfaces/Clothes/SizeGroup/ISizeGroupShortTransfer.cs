using BoutiqueCommon.Models.Common.Interfaces.Clothes.SizeGroups;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTO.Models.Interfaces.Clothes.SizeGroup
{
    /// <summary>
    /// Группа размеров одежды разного типа. Базовые данные. Трансферная модель
    /// </summary>
    public interface ISizeGroupShortTransfer : ISizeGroupShortBase, ITransferModel<(ClothesSizeType, int)>
    { }
}