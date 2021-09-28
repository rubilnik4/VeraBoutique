using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Database;
using BoutiqueDAL.Models.Implementations.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BoutiqueMVC.Models.Implementations.Identity
{
    /// <summary>
    /// Хранилище паролей
    /// </summary>
    public class PasswordHasherBoutique : PasswordHasher<BoutiqueUser>
    { }
}