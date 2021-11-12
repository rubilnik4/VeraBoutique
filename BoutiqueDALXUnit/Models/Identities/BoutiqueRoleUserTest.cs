using System.Linq;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueCommonXUnit.Data.Authorize;
using BoutiqueDAL.Models.Implementations.Entities.Identities;
using BoutiqueDALXUnit.Data.Identity;
using Xunit;

namespace BoutiqueDALXUnit.Models.Identities
{
    /// <summary>
    /// Пользователь. Тесты
    /// </summary>
    public class BoutiqueRoleUserTest
    {
        /// <summary>
        /// Обновить личные данные
        /// </summary>
        [Fact]
        public void UpdatePersonal()
        {
            var userEntity = IdentityEntitiesData.BoutiqueIdentityUsers.First();
            var user = IdentityData.BoutiqueUsers.Last();
            var userUpdated = userEntity.UpdatePersonal(user);

            Assert.Equal(userUpdated.Email, userEntity.Email);
            Assert.Equal(userUpdated.PasswordHash, userEntity.PasswordHash);
            Assert.Equal(userUpdated.Name, user.Name);
            Assert.Equal(userUpdated.Surname, user.Surname);
            Assert.Equal(userUpdated.Address, user.Address);
            Assert.Equal(userUpdated.PhoneNumber, user.Phone);
        }
    }
}