using BoutiqueDAL.Models.Implementations.Identity;
using Microsoft.AspNetCore.Identity;

namespace BoutiqueMVC.Models.Implementations.Identity
{
    /// <summary>
    /// Проверка пользователя
    /// </summary>
    public class UserConfirmationBoutique: DefaultUserConfirmation<BoutiqueUser>
    { }
}