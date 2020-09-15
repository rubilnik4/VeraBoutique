using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueCommonXUnit.Data.Models.Implementations;

namespace BoutiqueCommonXUnit.Data.Models.Interfaces
{
    /// <summary>
    /// Тестовая доменная модель
    /// </summary>
    public interface ITestDomain: ITest, IDomainModel<TestEnum>
    { }
}