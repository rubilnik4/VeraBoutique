using BoutiqueDTO.Models.Implementations.Configuration;
using BoutiqueLoader.Models.Interfaces.Configuration;
using Newtonsoft.Json;

namespace BoutiqueLoader.Models.Implementations.Configuration
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