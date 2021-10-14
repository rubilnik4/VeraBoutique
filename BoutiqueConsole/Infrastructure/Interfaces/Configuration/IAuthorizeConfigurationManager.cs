using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueDTO.Infrastructure.Interfaces.Configuration;

namespace BoutiqueConsole.Infrastructure.Interfaces.Configuration
{
    /// <summary>
    /// Доступ к файлам авторизации консольного приложения
    /// </summary>
    public interface IAuthorizeConfigurationManager : IConfigurationManager<string, IAuthorizeDomain>
    { }
}