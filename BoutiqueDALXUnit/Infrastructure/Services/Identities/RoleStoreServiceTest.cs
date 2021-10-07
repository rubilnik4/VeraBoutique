using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Identities;
using BoutiqueDAL.Infrastructure.Implementations.Services.Identities;
using BoutiqueDAL.Infrastructure.Interfaces.Services.Identities;
using BoutiqueDAL.Models.Implementations.Identities;
using BoutiqueDALXUnit.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Moq;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.AuthorizeErrors;
using ResultFunctional.Models.Interfaces.Results;
using Xunit;

namespace BoutiqueDALXUnit.Infrastructure.Services.Identities
{
    /// <summary>
    /// Сервис управления ролями. Тесты
    /// </summary>
    public class RoleStoreServiceTest
    {
        /// <summary>
        /// Создать роль
        /// </summary>
        [Fact]
        public async Task CreateRole()
        {
            const IdentityRoleType identityRoleType = IdentityRoleType.User;
            var identityResult = IdentityResult.Success;
            var roleStore = GetRoleStore(identityResult, null);
            var roleStoreService = new RoleStoreService(roleStore.Object);

            var roleResult = await roleStoreService.CreateRole(identityRoleType);

            Assert.True(roleResult.OkStatus);
            Assert.Equal(identityRoleType, roleResult.Value);
        }

        /// <summary>
        /// Создать роль
        /// </summary>
        [Fact]
        public async Task CreateRole_Fail()
        {
            const IdentityRoleType identityRoleType = IdentityRoleType.User;
            var identityError = new IdentityError { Code =  IdentityData.DuplicateRoleName };
            var identityResult = IdentityResult.Failed(identityError);
            var roleStore = GetRoleStore(identityResult, null);
            var roleStoreService = new RoleStoreService(roleStore.Object);

            var roleResult = await roleStoreService.CreateRole(identityRoleType);

            Assert.True(roleResult.HasErrors);
            Assert.IsAssignableFrom<AuthorizeErrorResult>(roleResult.Errors.First());
            Assert.Equal(AuthorizeErrorType.Duplicate.ToString(), roleResult.Errors.First().Id);
        }

        /// <summary>
        /// Создать роль
        /// </summary>
        [Fact]
        public async Task CreateRole_Duplicate()
        {
            const IdentityRoleType identityRoleType = IdentityRoleType.User;
            var identityRole = RoleStoreService.GetIdentityRole(identityRoleType);
            var identityResult = IdentityResult.Success;
            var roleStore = GetRoleStore(identityResult, identityRole.Id);
            var roleStoreService = new RoleStoreService(roleStore.Object);

            var roleResult = await roleStoreService.CreateRole(identityRoleType);

            Assert.True(roleResult.HasErrors);
            Assert.IsAssignableFrom<AuthorizeErrorResult>(roleResult.Errors.First());
            Assert.Equal(AuthorizeErrorType.Duplicate.ToString(), roleResult.Errors.First().Id);
        }

        /// <summary>
        /// Менеджер ролей
        /// </summary>
        private static Mock<IRoleStore<IdentityRole>> GetRoleStore(IdentityResult identityResult, string? roleId) =>
            new Mock<IRoleStore<IdentityRole>>().
            Void(roleStoreMock => roleStoreMock.Setup(roleStore => roleStore.CreateAsync(It.IsAny<IdentityRole>(), default)).
                                                ReturnsAsync(identityResult)).
            Void(roleStoreMock => roleStoreMock.Setup(roleStore => roleStore.GetRoleIdAsync(It.IsAny<IdentityRole>(), default)).
                                                ReturnsAsync(roleId));
    }
}