using BoutiqueCommon.Models.Domain.Interfaces.Configuration;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Models.Implementations.Configuration;

namespace BoutiqueDTO.Infrastructure.Interfaces.Converters.Configuration
{
    /// <summary>
    /// Преобразование параметров сервера в трансферную модель
    /// </summary>
    public interface IHostConfigurationTransferConverter : ITransferConverter<string, IHostConfigurationDomain, HostConfigurationTransfer>
    { }
}