using System;
using BoutiqueCommon.Models.Domain.Interfaces.Configuration;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using BoutiqueDTO.Infrastructure.Interfaces.Configuration;
using BoutiqueLoader.Models.Interfaces.Configuration;

namespace BoutiqueLoader.Infrastructure.Interfaces.Configuration
{
    /// <summary>
    /// Доступ к файлам авторизации консольного приложения
    /// </summary>
    public interface IAuthorizeConfigurationManager : IConfigurationManager<(string, string), IAuthorizeDomain>
    { }
}