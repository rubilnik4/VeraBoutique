using BoutiqueDTO.Models.Implementations.Configuration;
using BoutiqueXamarinCommon.Models.Interfaces.Configuration;
using Newtonsoft.Json;

namespace BoutiqueXamarinCommon.Models.Implementation.Configuration
{
    /// <summary>
    /// Конфигурация. Трансферная модель
    /// </summary>
    public class XamarinConfigurationTransfer : XamarinConfigurationBase<HostConfigurationTransfer>, IXamarinConfigurationTransfer
    {
        [JsonConstructor]
        public XamarinConfigurationTransfer(HostConfigurationTransfer hostConfiguration)
            : base(hostConfiguration)
        { }
    }
}