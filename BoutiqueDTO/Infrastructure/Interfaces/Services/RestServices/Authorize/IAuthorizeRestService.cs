﻿using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using ResultFunctional.Models.Implementations.Errors.AuthorizeErrors;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueDTO.Infrastructure.Interfaces.Services.RestServices.Authorize
{
    /// <summary>
    /// Сервис авторизации
    /// </summary>
    public interface IAuthorizeRestService
    {
        /// <summary>
        /// Авторизироваться в сервисе
        /// </summary>
        Task<IResultValue<string>> AuthorizeJwt(IAuthorizeDomain authorizeDomain);
    }
}