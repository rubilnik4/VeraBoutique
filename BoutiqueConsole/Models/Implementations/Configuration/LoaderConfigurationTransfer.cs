using BoutiqueConsole.Models.Interfaces.Configuration;
using BoutiqueDTO.Models.Implementations.Configuration;
using Newtonsoft.Json;

namespace BoutiqueConsole.Models.Implementations.Configuration
{
    /// <summary>
    /// Конфигурация консольного загрузчика . Трансферная модель
    /// </summary>
    public class LoaderConfigurationTransfer : LoaderConfigurationBase<HostConfigurationTransfer>, ILoaderConfigurationTransfer
    {
        [JsonConstructor]
        public LoaderConfigurationTransfer(HostConfigurationTransfer hostConfiguration)
            : base(hostConfiguration)
        { }
    }
}