using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Identity;
using BoutiqueDAL.Models.Enums.Identity;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Identity;
using BoutiqueDTO.Models.Implementations.Identity;
using BoutiqueMVC.Extensions.Controllers.Sync;
using BoutiqueMVC.Infrastructure.Implementation;
using BoutiqueMVC.Infrastructure.Implementation.Validation;
using BoutiqueMVC.Models.Implementations.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ResultFunctional.FunctionalExtensions.Async;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueMVC.Controllers.Implementations.Identity
{
    /// <summary>
    /// Контроллер регистрации
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class RegisterController : ControllerBase
    {
        public RegisterController(IUserManagerBoutique userManager, AuthorizeSettings authorizeSettings,
                                  IRegisterTransferConverter registerTransferConverter)
        {
            _userManager = userManager;
            _authorizeSettings = authorizeSettings;
            _registerTransferConverter = registerTransferConverter;
        }

        /// <summary>
        /// Менеджер авторизации
        /// </summary>
        private readonly IUserManagerBoutique _userManager;

        /// <summary>
        /// Параметры авторизации
        /// </summary>
        private readonly AuthorizeSettings _authorizeSettings;

        /// <summary>
        /// Конвертер регистрации в трансферную модель
        /// </summary>
        private readonly IRegisterTransferConverter _registerTransferConverter;

        /// <summary>
        /// Зарегистрироваться
        /// </summary>
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Register(RegisterTransfer register) =>
            await _registerTransferConverter.FromTransfer(register).
            ResultValueBindOk(registerDomain => RegisterValidation.RegisterValidate(registerDomain, _authorizeSettings)).
            ResultValueBindErrorsOkAsync(registerDomain => CheckByEmail(registerDomain.Authorize.Email)).
            ResultValueOkBindAsync(registerDomain => _userManager.Register(registerDomain, IdentityRoleType.User)).
            ResultValueBindErrorsOkTaskAsync(GetRegisterErrors).
            WhereContinueTaskAsync(result => result.OkStatus,
                                   _ => new NoContentResult(),
                                   result => result.Errors.GetBadRequestByErrors());

        /// <summary>
        /// Проверить наличие пользователя
        /// </summary>
        private async Task<IResultError> CheckByEmail(string email) =>
             await _userManager.FindByEmail(email).
             WhereContinueTaskAsync(result => result is null,
                                    _ => new ResultError(),
                                    _ => ErrorResultFactory.AuthorizeError(AuthorizeErrorType.Duplicate,
                                                                           $"Пользователь {email} уже присутствует в системе").
                                         ToResult());

        /// <summary>
        /// Получить ошибки при регистрации
        /// </summary>
        private static IResultError GetRegisterErrors(IdentityResult identityResult) =>
            identityResult.Errors.
            Select(identityError => ErrorResultFactory.AuthorizeError(AuthorizeErrorType.Register,
                                                                      $"Ошибка регистрации. Код {identityError.Code} {identityError.Description}")).
            ToList().
            Map(errors => new ResultError(errors));
    }
}