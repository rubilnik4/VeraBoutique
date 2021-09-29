using Microsoft.AspNetCore.Identity;

namespace BoutiqueDAL.Models.Implementations.Identity
{
    /// <summary>
    /// Хранилище паролей
    /// </summary>
    public class PasswordHasherBoutique : PasswordHasher<BoutiqueUser>
    { }
}