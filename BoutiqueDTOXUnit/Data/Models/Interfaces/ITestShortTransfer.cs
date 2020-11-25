using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTOXUnit.Data.Models.Interfaces
{
    /// <summary>
    /// Тестовая трансферная модель основных данных
    /// </summary>
    public interface ITestShortTransfer : ITest, ITransferModel<TestEnum>
    {
        
    }
}