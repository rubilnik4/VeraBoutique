using System.Collections.Generic;
using System.Linq;
using BoutiqueDAL.Infrastructure.Interfaces.Identities;
using BoutiqueDAL.Models.Implementations.Identities;
using Microsoft.AspNetCore.Identity;
using MockQueryable.Moq;
using Moq;
using ResultFunctional.FunctionalExtensions.Sync;

namespace BoutiqueDALXUnit.Infrastructure.Mocks.Identities
{
    /// <summary>
    /// Хранилище пользователей
    /// </summary>
    public static class UserManagerMock
    {
        /// <summary>
        /// Менеджер ролей
        /// </summary>
        public static Mock<IUserManagerBoutique> GetUserManagerRoleUser(IEnumerable<string> roles) =>
            GetUserManagerRoleUsers(Enumerable.Empty<BoutiqueIdentityUser>(), roles);

        /// <summary>
        /// Менеджер ролей
        /// </summary>
        public static Mock<IUserManagerBoutique> GetUserManagerRoleUsers(IEnumerable<BoutiqueIdentityUser> users,
                                                                        IEnumerable<string> roles) =>
            GetUserManager(roles, users, null!, null!, null!, null!, null!);

        /// <summary>
        /// Менеджер ролей
        /// </summary>
        public static Mock<IUserManagerBoutique> GetUserManagerFindByEmail(BoutiqueIdentityUser user) =>
            GetUserManagerFindByEmail(user, Enumerable.Empty<string>());

        /// <summary>
        /// Менеджер ролей
        /// </summary>
        public static Mock<IUserManagerBoutique> GetUserManagerFindByEmail(BoutiqueIdentityUser user, IEnumerable<string> roles) =>
            GetUserManager(roles, Enumerable.Empty<BoutiqueIdentityUser>(), user, null!, null!, null!, null!);

        /// <summary>
        /// Менеджер ролей
        /// </summary>
        public static Mock<IUserManagerBoutique> GetUserManagerCreate(IdentityResult resultCreate, IdentityResult resultRole) =>
            GetUserManagerCreate(resultCreate, resultRole, null!);

        /// <summary>
        /// Менеджер ролей
        /// </summary>
        public static Mock<IUserManagerBoutique> GetUserManagerCreate(IdentityResult resultCreate, IdentityResult resultRole,
                                                                      BoutiqueIdentityUser user) =>
            GetUserManager(null!, Enumerable.Empty<BoutiqueIdentityUser>(), user, resultCreate, resultRole, null!, null!);


        /// <summary>
        /// Менеджер ролей
        /// </summary>
        public static Mock<IUserManagerBoutique> GetUserManagerDelete(IdentityResult resultDelete, IdentityResult resultRole,
                                                                      IEnumerable<string> roles) =>
            GetUserManagerDelete(resultDelete, resultRole, roles, null!);

        /// <summary>
        /// Менеджер ролей
        /// </summary>
        public static Mock<IUserManagerBoutique> GetUserManagerDelete(IdentityResult resultDelete, IdentityResult resultRole,
                                                                      IEnumerable<string> roles, BoutiqueIdentityUser user) =>
            GetUserManager(roles, Enumerable.Empty<BoutiqueIdentityUser>(), user, null!, null!, resultDelete, resultRole);

        /// <summary>
        /// Менеджер ролей
        /// </summary>
        private static Mock<IUserManagerBoutique> GetUserManager(IEnumerable<string> roles, IEnumerable<BoutiqueIdentityUser> users,
                                                                 BoutiqueIdentityUser user,
                                                                 IdentityResult resultCreate, IdentityResult resultCreateRole,
                                                                 IdentityResult resultDelete, IdentityResult resultDeleteRole) =>
            new Mock<IUserManagerBoutique>().
            Void(userManagerMock => userManagerMock.Setup(userManager => userManager.Users).
                                                    Returns(users.AsQueryable().BuildMock().Object)).
            Void(userManagerMock => userManagerMock.Setup(userManager => userManager.GetRolesAsync(It.IsAny<BoutiqueIdentityUser>())).
                                                    ReturnsAsync(roles.ToList)).
            Void(userManagerMock => userManagerMock.Setup(userManager => userManager.FindByEmailAsync(It.IsAny<string>())).
                                                    ReturnsAsync(user)).
            Void(userManagerMock => userManagerMock.Setup(userManager => userManager.CreateAsync(It.IsAny<BoutiqueIdentityUser>())).
                                                    ReturnsAsync(resultCreate)).
            Void(userManagerMock => userManagerMock.Setup(userManager => userManager.AddToRoleAsync(It.IsAny<BoutiqueIdentityUser>(),
                                                                                                    It.IsAny<string>())).
                                                    ReturnsAsync(resultCreateRole)).
            Void(userManagerMock => userManagerMock.Setup(userManager => userManager.DeleteAsync(It.IsAny<BoutiqueIdentityUser>())).
                                                    ReturnsAsync(resultDelete)).
            Void(userManagerMock => userManagerMock.Setup(userManager => userManager.RemoveFromRoleAsync(It.IsAny<BoutiqueIdentityUser>(),
                                                                                                         It.IsAny<string>())).
                                                    ReturnsAsync(resultDeleteRole));


    }
}