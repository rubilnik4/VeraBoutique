using System.Collections.Generic;
using System.Linq;
using BoutiqueDAL.Infrastructure.Interfaces.Identities;
using BoutiqueDAL.Models.Implementations.Entities.Identities;
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
            GetUserManagerRoleUsers(Enumerable.Empty<BoutiqueUserEntity>(), roles);

        /// <summary>
        /// Менеджер ролей
        /// </summary>
        public static Mock<IUserManagerBoutique> GetUserManagerRoleUsers(IEnumerable<BoutiqueUserEntity> users,
                                                                         IEnumerable<string> roles) =>
            GetUserManager(roles, users, null!, null!, null!, null!, null!);

        /// <summary>
        /// Менеджер ролей
        /// </summary>
        public static Mock<IUserManagerBoutique> GetUserManagerFindByEmail(BoutiqueUserEntity userEntity) =>
            GetUserManagerFindByEmail(userEntity, Enumerable.Empty<string>());

        /// <summary>
        /// Менеджер ролей
        /// </summary>
        public static Mock<IUserManagerBoutique> GetUserManagerFindByEmail(BoutiqueUserEntity userEntity, IEnumerable<string> roles) =>
            GetUserManager(roles, Enumerable.Empty<BoutiqueUserEntity>(), userEntity, null!, null!, null!, null!);

        /// <summary>
        /// Менеджер ролей
        /// </summary>
        public static Mock<IUserManagerBoutique> GetUserManagerCreate(IdentityResult resultCreate, IdentityResult resultRole) =>
            GetUserManagerCreate(resultCreate, resultRole, null!);

        /// <summary>
        /// Менеджер ролей
        /// </summary>
        public static Mock<IUserManagerBoutique> GetUserManagerCreate(IdentityResult resultCreate, IdentityResult resultRole,
                                                                      BoutiqueUserEntity userEntity) =>
            GetUserManager(null!, Enumerable.Empty<BoutiqueUserEntity>(), userEntity, resultCreate, resultRole, null!, null!);

        /// <summary>
        /// Менеджер ролей
        /// </summary>
        public static Mock<IUserManagerBoutique> GetUserManagerDelete(IdentityResult resultDelete, IdentityResult resultRole,
                                                                      IEnumerable<string> roles, BoutiqueUserEntity userEntity) =>
            GetUserManager(roles, Enumerable.Empty<BoutiqueUserEntity>(), userEntity, null!, null!, resultDelete, resultRole);

        /// <summary>
        /// Менеджер ролей
        /// </summary>
        private static Mock<IUserManagerBoutique> GetUserManager(IEnumerable<string> roles, IEnumerable<BoutiqueUserEntity> users,
                                                                 BoutiqueUserEntity userEntity,
                                                                 IdentityResult resultCreate, IdentityResult resultCreateRole,
                                                                 IdentityResult resultDelete, IdentityResult resultDeleteRole) =>
            new Mock<IUserManagerBoutique>().
            Void(userManagerMock => userManagerMock.Setup(userManager => userManager.GetUsers()).
                                                    ReturnsAsync(users.ToList)).
            Void(userManagerMock => userManagerMock.Setup(userManager => userManager.GetRolesAsync(It.IsAny<BoutiqueUserEntity>())).
                                                    ReturnsAsync(roles.ToList)).
            Void(userManagerMock => userManagerMock.Setup(userManager => userManager.FindByEmailAsync(It.IsAny<string>())).
                                                    ReturnsAsync(userEntity)).
            Void(userManagerMock => userManagerMock.Setup(userManager => userManager.CreateAsync(It.IsAny<BoutiqueUserEntity>())).
                                                    ReturnsAsync(resultCreate)).
            Void(userManagerMock => userManagerMock.Setup(userManager => userManager.AddToRoleAsync(It.IsAny<BoutiqueUserEntity>(),
                                                                                                    It.IsAny<string>())).
                                                    ReturnsAsync(resultCreateRole)).
            Void(userManagerMock => userManagerMock.Setup(userManager => userManager.DeleteAsync(It.IsAny<BoutiqueUserEntity>())).
                                                    ReturnsAsync(resultDelete)).
            Void(userManagerMock => userManagerMock.Setup(userManager => userManager.RemoveFromRoleAsync(It.IsAny<BoutiqueUserEntity>(),
                                                                                                         It.IsAny<string>())).
                                                    ReturnsAsync(resultDeleteRole));


    }
}