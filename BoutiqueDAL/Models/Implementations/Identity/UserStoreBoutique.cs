using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Database;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BoutiqueDAL.Models.Implementations.Identity
{
    /// <summary>
    /// Хранилище пользователей
    /// </summary>
    public class UserStoreBoutique: UserStore<BoutiqueUser>
    {
        public UserStoreBoutique(BoutiqueDatabase dbContext)
            :base(dbContext)
        { }
    }
}