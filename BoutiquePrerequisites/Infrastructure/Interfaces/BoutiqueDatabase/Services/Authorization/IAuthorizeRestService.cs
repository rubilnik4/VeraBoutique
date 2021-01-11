using BoutiqueCommon.Models.Common.Interfaces.Identity;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiquePrerequisites.Infrastructure.Interfaces.BoutiqueDatabase.Services.Base;

namespace BoutiquePrerequisites.Infrastructure.Interfaces.BoutiqueDatabase.Services.Authorization
{
    /// <summary>
    /// Сервис авторизации
    /// </summary>
    public interface IAuthorizeRestService : IRestServiceBase<string, IIdentityLoginBase>
    { }
}