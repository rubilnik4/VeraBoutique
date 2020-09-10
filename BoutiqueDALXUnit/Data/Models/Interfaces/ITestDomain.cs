using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueDALXUnit.Data.Models.Implementation;

namespace BoutiqueDALXUnit.Data.Models.Interfaces
{
    /// <summary>
    /// Тестовая доменная модель
    /// </summary>
    public interface ITestDomain: ITest, IDomainModel<TestEnum>
    { }
}