using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTO.Models.Interfaces.Clothes
{
    /// <summary>
    /// Трансферная модель типа пола
    /// </summary>
    public interface IGenderTransfer: IGender, ITransferModel<GenderType>
    { }
}