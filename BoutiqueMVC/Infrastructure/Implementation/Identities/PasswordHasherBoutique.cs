using BoutiqueDAL.Models.Implementations.Entities.Identities;
using BoutiqueDAL.Models.Implementations.Identities;
using Microsoft.AspNetCore.Identity;

namespace BoutiqueMVC.Infrastructure.Implementation.Identities
{
    /// <summary>
    /// Хранилище паролей
    /// </summary>
    public class PasswordHasherBoutique : PasswordHasher<BoutiqueUserEntity>
    { }
}