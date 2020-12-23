using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueCommonXUnit.Data.Models.Implementations;

namespace BoutiqueCommonXUnit.Data.Models.Interfaces
{
    /// <summary>
    /// Тестовая включенная доменная модель
    /// </summary>
    public interface ITestIncludeDomain : ITestIncludeBase, IDomainModel<string>
    { }
}