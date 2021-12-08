using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Identities;
using BoutiqueDALXUnit.Data.Identity;
using BoutiqueMVC.Controllers.Identity;
using Moq;
using ResultFunctional.FunctionalExtensions.Sync;
using Xunit;

namespace BoutiqueMVCXUnit.Controllers.Identity
{
    /// <summary>
    /// Контроллер пользователей. Тесты
    /// </summary>
    public class RoleControllerTest
    {
        /// <summary>
        /// Получить роли
        /// </summary>
        [Fact]
        public async Task GetRoles()
        {
            var roles = IdentityEntitiesData.RoleNames;
            var roleStore = GetRoleStore(roles);
            var roleController = new RoleController(roleStore.Object);

            var rolesResult = await roleController.GetRoles();

            Assert.True(rolesResult.Value.SequenceEqual(roles));
        }

        /// <summary>
        /// Менеджер авторизации
        /// </summary>
        private static Mock<IRoleStoreService> GetRoleStore(IEnumerable<string> roles) =>
            new Mock<IRoleStoreService>().
            Void(roleMock => roleMock.Setup(roleStore => roleStore.GetRoles()).
                                      ReturnsAsync(roles.ToList()));
    }
}