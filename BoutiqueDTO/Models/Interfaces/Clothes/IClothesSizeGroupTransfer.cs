using BoutiqueCommon.Models.Common.Interfaces.Clothes;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTO.Models.Interfaces.Clothes
{
    /// <summary>
    /// Группа размеров одежды разного типа. Трансферная модель
    /// </summary>
    public interface IClothesSizeGroupTransfer : ISizeGroup, ITransferModel<string>
    { }
}