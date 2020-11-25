using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTOXUnit.Data.Models.Interfaces
{
    /// <summary>
    /// Тестовая вложенная трансферная модель
    /// </summary>
    public interface ITestIncludeTransfer : ITestInclude, ITransferModel<string>
    { }
}