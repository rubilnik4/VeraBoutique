using BoutiqueDAL.Models.Implementations.Identity;
using Microsoft.AspNetCore.Identity;

namespace BoutiqueMVC.Infrastructure.Implementation.Identity
{
    /// <summary>
    /// Проверка пользователя
    /// </summary>
    public class UserConfirmationBoutique: DefaultUserConfirmation<BoutiqueIdentityUser>
    { }
}