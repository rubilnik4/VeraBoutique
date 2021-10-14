using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueConsole.Infrastructure.Interfaces.Configuration;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Identity;
using BoutiqueDTO.Models.Implementations.Identities;

namespace BoutiqueConsole.Infrastructure.Implementations.Configuration
{
    /// <summary>
    /// Доступ к файлам авторизации консольного приложения
    /// </summary>
    public class AuthorizeConfigurationManager : ConsoleConfigurationManager<string, IAuthorizeDomain, AuthorizeTransfer>,
                                                 IAuthorizeConfigurationManager
    {
        public AuthorizeConfigurationManager(IAuthorizeTransferConverter authorizeTransferConverter)
          : base(authorizeTransferConverter)
        { }

        /// <summary>
        /// Имя файла конфигурации
        /// </summary>
        protected override string ConfigurationFileName => "loginsettings.json";
    }
}