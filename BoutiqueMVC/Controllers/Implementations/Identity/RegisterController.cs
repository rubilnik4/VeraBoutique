using System.Net.Mime;
using System.Threading.Tasks;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Identity;
using BoutiqueDTO.Models.Implementations.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public RegisterController(IRegisterTransferConverter registerTransferConverter)
        {
            _registerTransferConverter = registerTransferConverter;
        }

        /// <summary>
        /// Конвертер регистрации в трансферную модель
        /// </summary>
        private readonly IRegisterTransferConverter _registerTransferConverter;

        ///// <summary>
        ///// Авторизоваться через JWT токен
        ///// </summary>
        //[HttpPost]
        //[Consumes(MediaTypeNames.Application.Json)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<string>> Register(RegisterTransfer register) =>
        //    await _registerTransferConverter.FromTransfer(register);
    }
}