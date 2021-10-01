using BoutiqueDAL.Models.Implementations.Identity;
using Microsoft.AspNetCore.Identity;

namespace BoutiqueMVC.Infrastructure.Implementation.Identity
{
    /// <summary>
    /// Хранилище паролей
    /// </summary>
    public class PasswordHasherBoutique : PasswordHasher<BoutiqueUser>
    { }
}