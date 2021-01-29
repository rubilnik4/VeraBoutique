using BoutiqueCommon.Models.Common.Interfaces.Configuration;
using BoutiqueDTO.Models.Interfaces.Base;

namespace BoutiqueDTO.Models.Interfaces.Configuration
{
    /// <summary>
    /// Параметры подключения к серверу. Трансферная модель
    /// </summary>
    public interface IHostConfigurationTransfer: IHostConfigurationBase, ITransferModel<string>
    { }
}