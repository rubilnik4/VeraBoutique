using BoutiqueCommon.Models.Domain.Interfaces.Configuration;
using BoutiqueXamarin.Models.Interfaces.Configuration;

namespace BoutiqueXamarin.Models.Implementation.Configuration
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