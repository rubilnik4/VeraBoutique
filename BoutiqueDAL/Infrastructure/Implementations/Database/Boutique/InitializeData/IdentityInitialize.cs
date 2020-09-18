using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using BoutiqueDAL.Models.Enums.Identity;
using Functional.FunctionalExtensions.Async;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Boutique.InitializeData
{
    public static class IdentityInitialize
    {
        /// <summary>
        /// Добавить роли
        /// </summary>
        public static async Task Initialize(IdentityDbContext<IdentityUser> dbContext) =>
            await new RoleStore<IdentityRole>(dbContext).
            VoidAsync(roleStore => GetRolesToCreate(roleStore).
                                   VoidBindAsync(roles => CreateRoles(roleStore, roles)));

        /// <summary>
        /// Получить роли для добавления в базу
        /// </summary>
        private static async Task<IEnumerable<IdentityRole>> GetRolesToCreate(RoleStore<IdentityRole> roleStore) =>
            await roleStore.Roles.ToListAsync().
            MapTaskAsync(roles => roles.Select(role => role.Name)).
            MapTaskAsync(rolesNames => Enum.GetNames(typeof(IdentityRoleType)).
                                       Where(roleType => !rolesNames.Contains(roleType, StringComparer.OrdinalIgnoreCase)).
                                       Select(roleType => new IdentityRole(roleType)));

        /// <summary>
        /// Добавить роли в базу
        /// </summary>
        private static async Task CreateRoles(RoleStore<IdentityRole> roleStore, IEnumerable<IdentityRole> roles)
        {
            foreach (var role in roles)
            {
                await roleStore.CreateAsync(role);
            }
        }


        //var user = new ApplicationUser
        //{
        //    FirstName = "XXXX",
        //    LastName = "XXXX",
        //    Email = "xxxx@example.com",
        //    NormalizedEmail = "XXXX@EXAMPLE.COM",
        //    UserName = "Owner",
        //    NormalizedUserName = "OWNER",
        //    PhoneNumber = "+111111111111",
        //    EmailConfirmed = true,
        //    PhoneNumberConfirmed = true,
        //    SecurityStamp = Guid.NewGuid().ToString("D")
        //};


        //if (!context.Users.Any(u => u.UserName == user.UserName))
        //{
        //    var password = new PasswordHasher<ApplicationUser>();
        //    var hashed = password.HashPassword(user, "secret");
        //    user.PasswordHash = hashed;

        //    var userStore = new UserStore<ApplicationUser>(context);
        //    var result = userStore.CreateAsync(user);

        //}

        //AssignRoles(serviceProvider, user.Email, roles);

        //context.SaveChangesAsync();
        //  }

        //public static async Task<IdentityResult> AssignRoles(IServiceProvider services, string email, string[] roles)
        //{
        //    UserManager<ApplicationUser> _userManager = services.GetService<UserManager<ApplicationUser>>();
        //    ApplicationUser user = await _userManager.FindByEmailAsync(email);
        //    var result = await _userManager.AddToRolesAsync(user, roles);

        //    return result;
        //}
    }
}