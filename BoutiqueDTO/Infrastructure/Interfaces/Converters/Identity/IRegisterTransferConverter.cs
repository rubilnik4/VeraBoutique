using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Base;
using BoutiqueDTO.Models.Implementations.Identity;

namespace BoutiqueDTO.Infrastructure.Interfaces.Converters.Identity
{
    /// <summary>
    /// Конвертер регистрации в трансферную модель
    /// </summary>
    public interface IRegisterTransferConverter : ITransferConverter<string, IRegisterDomain, RegisterTransfer>
    { }
}