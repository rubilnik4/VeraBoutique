using BoutiqueDAL.Models.Implementations.Entities.Identities;
using BoutiqueDAL.Models.Implementations.Identities;
using Microsoft.AspNetCore.Identity;

namespace BoutiqueMVC.Infrastructure.Implementation.Identities
{
    /// <summary>
    /// Проверка пользователя
    /// </summary>
    public class UserConfirmationBoutique: DefaultUserConfirmation<BoutiqueUserEntity>
    { }
}