using BoutiqueCommon.Extensions.ReflectionExtensions;
using BoutiqueCommon.Extensions.StringExtensions;
using BoutiqueDTO.Infrastructure.Implementations.Services.Api.Base;

namespace BoutiqueDTO.Infrastructure.Interfaces.Services.Api.Base
{
    public interface IApiServiceBase
    {
        /// <summary>
        /// Наименование контроллера
        /// </summary>
        string ControllerName { get; }
    }
}