using BoutiqueCommon.Models.Domain.Interfaces.Configuration;
using BoutiqueDTO.Models.Implementations.Configuration;
using BoutiqueDTO.Models.Interfaces.Configuration;
using BoutiqueXamarin.Models.Interfaces.Configuration;
using Newtonsoft.Json;

namespace BoutiqueXamarin.Models.Implementation.Configuration
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