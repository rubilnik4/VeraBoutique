using BoutiqueCommon.Models.Domain.Interfaces.Configuration;
using BoutiqueConsole.Models.Interfaces.Configuration;

namespace BoutiqueConsole.Models.Implementations.Configuration
{
    /// <summary>
    /// Конфигурация консольного загрузчика . Доменная модель
    /// </summary>
    public class LoaderConfigurationDomain: LoaderConfigurationBase<IHostConfigurationDomain>, ILoaderConfigurationDomain
    {
        public LoaderConfigurationDomain(IHostConfigurationDomain hostConfiguration)
            :base(hostConfiguration)
        { }
    }
}