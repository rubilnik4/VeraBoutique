using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Identities;
using BoutiqueCommonXUnit.Data.Authorize;
using BoutiqueDAL.Infrastructure.Implementations.Identities;
using BoutiqueDAL.Infrastructure.Implementations.Services.Identities;
using BoutiqueDAL.Infrastructure.Interfaces.Identities;
using BoutiqueDAL.Models.Implementations.Identities;
using BoutiqueDALXUnit.Data.Identity;
using BoutiqueDALXUnit.Infrastructure.Mocks.Identities;
using Microsoft.AspNetCore.Identity;
using MockQueryable.Moq;
using Moq;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors.AuthorizeErrors;
using ResultFunctional.Models.Interfaces.Errors.CommonErrors;
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
            var userManager = UserManagerMock.GetUserManagerRoleUsers(users, roleNames);
            var userManagerService = new UserManagerService(userManager.Object);

            var roleUsers = await userManagerService.GetRoleUsers();
            var usersZip = users.Zip(roleUsers);

            Assert.Equal(users.Count, roleUsers.Count);
            Assert.True(usersZip.All(userRole => userRole.First.Email == userRole.Second.BoutiqueIdentityUser.Email &&
                                                 userRole.Second.IdentityRoleType.ToString() == roleNames.First()));
        }

        /// <summary>
        /// Получить роли для пользователя
        /// </summary>
        [Fact]
        public async Task GetRoleUser()
        {
            var user = IdentityData.BoutiqueIdentityUsers.First();
            var roleNames = IdentityData.RoleNames;
            var userManager = UserManagerMock.GetUserManagerRoleUser(roleNames);
            var userManagerService = new UserManagerService(userManager.Object);

            var roleUser = await userManagerService.GetRoleUser(user);

            Assert.Equal(roleUser.BoutiqueIdentityUser.Email, user.Email);
            Assert.Equal(roleNames.First(), roleUser.IdentityRoleType.ToString());
        }

        /// <summary>
        /// Найти пользователя по почте
        /// </summary>
        [Fact]
        public async Task FindUserByEmail()
        {
            var user = IdentityData.BoutiqueIdentityUsers.First();
            var userManager = UserManagerMock.GetUserManagerFindByEmail(user);
            var userManagerService = new UserManagerService(userManager.Object);

            var userFound = await userManagerService.FindUserByEmail(user.Email);

            Assert.True(userFound.OkStatus);
            Assert.Equal(user.Email, userFound.Value.Email);
        }

        /// <summary>
        /// Найти пользователя по почте
        /// </summary>
        [Fact]
        public async Task FindUserByEmail_NotFound()
        {
            var user = IdentityData.BoutiqueIdentityUsers.First();
            var userManager = UserManagerMock.GetUserManagerFindByEmail(null!);
            var userManagerService = new UserManagerService(userManager.Object);

            var userFound = await userManagerService.FindUserByEmail(user.Email);

            Assert.True(userFound.HasErrors);
            Assert.IsAssignableFrom<IValueNotFoundErrorResult>(userFound.Errors.First());
            Assert.Equal(CommonErrorType.ValueNotFound.ToString(), userFound.Errors.First().Id);
        }

        /// <summary>
        /// Найти пользователя по почте
        /// </summary>
        [Fact]
        public async Task FindRoleUserByEmail()
        {
            var user = IdentityData.BoutiqueIdentityUsers.First();
            var roleNames = IdentityData.RoleNames;
            var userManager = UserManagerMock.GetUserManagerFindByEmail(user, roleNames);
            var userManagerService = new UserManagerService(userManager.Object);

            var userFound = await userManagerService.FindRoleUserByEmail(user.Email);

            Assert.True(userFound.OkStatus);
            Assert.Equal(user.Email, userFound.Value.BoutiqueIdentityUser.Email);
            Assert.Equal(roleNames.First(), userFound.Value.IdentityRoleType.ToString());
        }

        /// <summary>
        /// Найти пользователя по почте
        /// </summary>
        [Fact]
        public async Task FindRoleUserByEmail_NotFound()
        {
            var user = IdentityData.BoutiqueIdentityUsers.First();
            var roleNames = IdentityData.RoleNames;
            var userManager = UserManagerMock.GetUserManagerFindByEmail(null!, roleNames);
            var userManagerService = new UserManagerService(userManager.Object);

            var userFound = await userManagerService.FindRoleUserByEmail(user.Email);

            Assert.True(userFound.HasErrors);
            Assert.IsAssignableFrom<IValueNotFoundErrorResult>(userFound.Errors.First());
            Assert.Equal(CommonErrorType.ValueNotFound.ToString(), userFound.Errors.First().Id);
        }

        /// <summary>
        /// Добавить пользователя
        /// </summary>
        [Fact]
        public async Task CreateRoleUser()
        {
            var register = RegisterData.RegisterDomains.First();
            const IdentityRoleType roleType = IdentityRoleType.User;
            var resultCreate = IdentityResult.Success;
            var resultRole = IdentityResult.Success;
            var userManager = UserManagerMock.GetUserManagerCreate(resultCreate, resultRole);
            var userManagerService = new UserManagerService(userManager.Object);

            var result = await userManagerService.CreateRoleUser(register, roleType);

            Assert.True(result.OkStatus);
            Assert.Equal(register.Id, result.Value);
        }

        /// <summary>
        /// Добавить пользователя
        /// </summary>
        [Fact]
        public async Task CreateRoleUser_CreateFail()
        {
            var register = RegisterData.RegisterDomains.First();
            const IdentityRoleType roleType = IdentityRoleType.User;
            var identityError = new IdentityError { Code = IdentityData.DuplicateRoleName };
            var resultCreate = IdentityResult.Failed(identityError);
            var resultRole = IdentityResult.Success;
            var userManager = UserManagerMock.GetUserManagerCreate(resultCreate, resultRole);
            var userManagerService = new UserManagerService(userManager.Object);

            var result = await userManagerService.CreateRoleUser(register, roleType);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<AuthorizeErrorResult>(result.Errors.First());
            Assert.Equal(AuthorizeErrorType.Duplicate.ToString(), result.Errors.First().Id);
        }

        /// <summary>
        /// Добавить пользователя
        /// </summary>
        [Fact]
        public async Task CreateRoleUser_RoleFail()
        {
            var register = RegisterData.RegisterDomains.First();
            const IdentityRoleType roleType = IdentityRoleType.User;
            var identityError = new IdentityError { Code = IdentityData.DuplicateRoleName };
            var resultCreate = IdentityResult.Success;
            var resultRole = IdentityResult.Failed(identityError);
            var userManager = UserManagerMock.GetUserManagerCreate(resultCreate, resultRole);
            var userManagerService = new UserManagerService(userManager.Object);

            var result = await userManagerService.CreateRoleUser(register, roleType);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<AuthorizeErrorResult>(result.Errors.First());
            Assert.Equal(AuthorizeErrorType.Duplicate.ToString(), result.Errors.First().Id);
        }

        /// <summary>
        /// Добавить пользователя
        /// </summary>
        [Fact]
        public async Task CreateRoleUser_Duplicate()
        {
            var register = RegisterData.RegisterDomains.First();
            var user = IdentityData.BoutiqueIdentityUsers.First();
            const IdentityRoleType roleType = IdentityRoleType.User;
            var resultCreate = IdentityResult.Success;
            var resultRole = IdentityResult.Success;
            var userManager = UserManagerMock.GetUserManagerCreate(resultCreate, resultRole, user);
            var userManagerService = new UserManagerService(userManager.Object);

            var result = await userManagerService.CreateRoleUser(register, roleType);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<AuthorizeErrorResult>(result.Errors.First());
            Assert.Equal(AuthorizeErrorType.Duplicate.ToString(), result.Errors.First().Id);
        }

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        [Fact]
        public async Task DeleteRoleUser()
        {
            var user = IdentityData.BoutiqueIdentityUsers.First();
            var resultDelete = IdentityResult.Success;
            var resultRole = IdentityResult.Success;
            var roleNames = IdentityData.RoleNames;
            var userManager = UserManagerMock.GetUserManagerDelete(resultDelete, resultRole, roleNames);
            var userManagerService = new UserManagerService(userManager.Object);

            var result = await userManagerService.DeleteRoleUser(user);

            Assert.True(result.OkStatus);
            Assert.Equal(user.Email, result.Value);
        }

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        [Fact]
        public async Task DeleteRoleUser_DeleteFail()
        {
            var user = IdentityData.BoutiqueIdentityUsers.First();
            var identityError = new IdentityError { Code = IdentityData.DuplicateRoleName };
            var resultDelete = IdentityResult.Failed(identityError);
            var resultRole = IdentityResult.Success;
            var roleNames = IdentityData.RoleNames;
            var userManager = UserManagerMock.GetUserManagerDelete(resultDelete, resultRole, roleNames);
            var userManagerService = new UserManagerService(userManager.Object);

            var result = await userManagerService.DeleteRoleUser(user);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<AuthorizeErrorResult>(result.Errors.First());
            Assert.Equal(AuthorizeErrorType.Duplicate.ToString(), result.Errors.First().Id);
        }

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        [Fact]
        public async Task DeleteRoleUser_RoleFail()
        {
            var user = IdentityData.BoutiqueIdentityUsers.First();
            var identityError = new IdentityError { Code = IdentityData.DuplicateRoleName };
            var resultDelete = IdentityResult.Success; 
            var resultRole = IdentityResult.Failed(identityError);
            var roleNames = IdentityData.RoleNames;
            var userManager = UserManagerMock.GetUserManagerDelete(resultDelete, resultRole, roleNames);
            var userManagerService = new UserManagerService(userManager.Object);

            var result = await userManagerService.DeleteRoleUser(user);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<AuthorizeErrorResult>(result.Errors.First());
            Assert.Equal(AuthorizeErrorType.Duplicate.ToString(), result.Errors.First().Id);
        }

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        [Fact]
        public async Task DeleteRoleUserByEmail()
        {
            var user = IdentityData.BoutiqueIdentityUsers.First();
            var resultDelete = IdentityResult.Success;
            var resultRole = IdentityResult.Success;
            var roleNames = IdentityData.RoleNames;
            var userManager = UserManagerMock.GetUserManagerDelete(resultDelete, resultRole, roleNames, user);
            var userManagerService = new UserManagerService(userManager.Object);

            var result = await userManagerService.DeleteRoleUser(user.Email);

            Assert.True(result.OkStatus);
            Assert.Equal(user.Email, result.Value);
        }

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        [Fact]
        public async Task DeleteRoleUserByEmail_NotFound()
        {
            var user = IdentityData.BoutiqueIdentityUsers.First();
            var resultDelete = IdentityResult.Success;
            var resultRole = IdentityResult.Success;
            var roleNames = IdentityData.RoleNames;
            var userManager = UserManagerMock.GetUserManagerDelete(resultDelete, resultRole, roleNames, null!);
            var userManagerService = new UserManagerService(userManager.Object);

            var result = await userManagerService.DeleteRoleUser(user.Email);

            Assert.True(result.HasErrors);
            Assert.IsAssignableFrom<IValueNotFoundErrorResult>(result.Errors.First());
        }
    }
}