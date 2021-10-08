using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Database;
using BoutiqueDAL.Models.Implementations.Entities.Identities;
using BoutiqueDAL.Models.Implementations.Identities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BoutiqueMVC.Infrastructure.Implementation.Identities
{
    /// <summary>
    /// Хранилище пользователей
    /// </summary>
    public class UserStoreBoutique: UserStore<BoutiqueUserEntity>
    {
        public UserStoreBoutique(BoutiqueDatabase dbContext)
            :base(dbContext)
        { }
    }
}