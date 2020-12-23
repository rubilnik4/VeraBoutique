using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueCommonXUnit.Data.Models.Implementations;

namespace BoutiqueCommonXUnit.Data.Models.Interfaces
{
    /// <summary>
    /// Тестовая доменная модель основных данных
    /// </summary>
    public interface ITestShortDomain : ITestShortBase, IDomainModel<TestEnum>
    { }
}