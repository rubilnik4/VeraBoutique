using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueDAL.Infrastructure.Implementations.Database.Base;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Identity;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.InitializeData.Identity;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Mapping;
using BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique;
using BoutiqueDAL.Infrastructure.Interfaces.Database.Boutique.Table.Clothes;
using BoutiqueDAL.Models.Implementations.Entities.Clothes;
using BoutiqueDAL.Models.Implementations.Identity;
using ResultFunctional.Models.Interfaces.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.Database
{
    /// <summary>
    /// База данных магазина Entity Framework
    /// </summary>
    public partial class BoutiqueDatabase : EntityDatabase, IBoutiqueDatabase
    {
        public BoutiqueDatabase(DbContextOptions options)
            : base(options)
        { }

        /// <summary>
        /// Обновить схемы базы данных
        /// </summary>
        public async Task UpdateSchema(UserManagerBoutique userManager, IRoleStore<IdentityRole> roleStore,
                                       IReadOnlyCollection<BoutiqueRoleUser> defaultUsers, IReadOnlyCollection<string> roleNames)
        {
            await Database.EnsureCreatedAsync();
            await Database.MigrateAsync();
            await IdentityInitialize.Initialize(userManager, roleStore, defaultUsers, roleNames);
        }

        /// <summary>
        /// Записать параметры конфигурации
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder builder) =>
            DatabaseConfiguration.ConfigureEnumsMapping();

        /// <summary>
        /// Записать схемы базы данных
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            DatabaseConfiguration.ApplyConfiguration(modelBuilder);
        }
    }
}