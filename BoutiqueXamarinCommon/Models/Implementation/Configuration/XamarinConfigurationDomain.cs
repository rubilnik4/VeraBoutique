using BoutiqueCommon.Models.Domain.Interfaces.Configuration;
using BoutiqueXamarinCommon.Models.Interfaces.Configuration;

namespace BoutiqueXamarinCommon.Models.Implementation.Configuration
{
    /// <summary>
    /// Конфигурация. Доменная модель
    /// </summary>
    public class XamarinConfigurationDomain: XamarinConfigurationBase<IHostConfigurationDomain>, IXamarinConfigurationDomain
    {
        public XamarinConfigurationDomain(IHostConfigurationDomain hostConfiguration)
            :base(hostConfiguration)
        { }
    }
}