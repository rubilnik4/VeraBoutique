using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Database;
using BoutiqueDAL.Models.Implementations.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BoutiqueMVC.Infrastructure.Implementation.Identity
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