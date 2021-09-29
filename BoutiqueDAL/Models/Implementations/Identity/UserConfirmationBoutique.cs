using Microsoft.AspNetCore.Identity;

namespace BoutiqueDAL.Models.Implementations.Identity
{
    /// <summary>
    /// Проверка пользователя
    /// </summary>
    public class UserConfirmationBoutique: DefaultUserConfirmation<BoutiqueUser>
    { }
}