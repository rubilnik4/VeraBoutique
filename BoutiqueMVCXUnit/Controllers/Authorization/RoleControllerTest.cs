using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Identities;
using BoutiqueDAL.Models.Implementations.Identities;
using BoutiqueDALXUnit.Data.Identity;
using BoutiqueDTOXUnit.Infrastructure.Mocks.Converters.Identity;
using BoutiqueMVC.Controllers.Implementations.Identity;
using Moq;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Results;
using Xunit;

namespace BoutiqueMVCXUnit.Controllers.Authorization
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
            var roles = IdentityData.RoleNames;
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