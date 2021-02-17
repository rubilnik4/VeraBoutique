using BoutiqueCommon.Models.Common.Interfaces.Clothes.Genders;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTO.Models.Interfaces.Clothes.GenderTransfers
{
    /// <summary>
    /// Тип пола. Трансферная модель
    /// </summary>
    public interface IGenderTransfer: IGenderBase, ITransferModel<GenderType>
    { }
}