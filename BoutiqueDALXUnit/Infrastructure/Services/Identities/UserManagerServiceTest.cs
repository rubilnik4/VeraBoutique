using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueDAL.Infrastructure.Implementations.Identities;
using BoutiqueDAL.Infrastructure.Implementations.Services.Identities;
using BoutiqueDAL.Infrastructure.Interfaces.Identities;
using BoutiqueDAL.Models.Implementations.Identities;
using BoutiqueDALXUnit.Data.Identity;
using Microsoft.AspNetCore.Identity;
using MockQueryable.Moq;
using Moq;
using ResultFunctional.FunctionalExtensions.Sync;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Services.Identities
{
    /// <summary>
    /// Управление пользователями. Тесты
    /// </summary>
    public class UserManagerServiceTest
    {
        /// <summary>
        /// Получить пользователей с ролями
        /// </summary>
        [Fact]
        public async Task GetRoleUsers()
        {
            var users = IdentityData.BoutiqueIdentityUsers;
            var roleNames = IdentityData.RoleNames;
            var userManager = GetUserManager(users, roleNames);
            var userManagerService = new UserManagerService(userManager.Object);

            var roleUsers = await userManagerService.GetRoleUsers();

            Assert.Equal(users.Count, roleUsers.Count);
            Assert.True(users.Zip(roleUsers).
                              All(userRole => userRole.First.Email == userRole.Second.BoutiqueIdentityUser.Email));
        }

        /// <summary>
        /// Менеджер ролей
        /// </summary>
        private static Mock<IUserManagerBoutique> GetUserManager(IEnumerable<BoutiqueIdentityUser> users, IEnumerable<string> roles) =>
            new Mock<IUserManagerBoutique>().
            Void(userManagerMock => userManagerMock.Setup(userManager => userManager.Users).
                                                    Returns(users.AsQueryable().BuildMock().Object)).
            Void(userManagerMock => userManagerMock.Setup(userManager => userManager.GetRolesAsync(It.IsAny<BoutiqueIdentityUser>())).
                                                    ReturnsAsync(roles.ToList));
    }
}