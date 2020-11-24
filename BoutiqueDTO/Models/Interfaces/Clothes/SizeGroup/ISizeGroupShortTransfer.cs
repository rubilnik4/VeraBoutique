using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTO.Models.Interfaces.Clothes.SizeGroup
{
    /// <summary>
    /// Группа размеров одежды разного типа. Базовые данные. Трансферная модель
    /// </summary>
    public interface ISizeGroupShortTransfer : ISizeGroup, ITransferModel<(ClothesSizeType, int)>
    { }
}