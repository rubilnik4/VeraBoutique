using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTO.Data.Models.Interfaces
{
    /// <summary>
    /// Тестовая трансферная модель
    /// </summary>
    public interface ITestTransfer: ITest, ITransferModel<TestEnum>
    { }
}