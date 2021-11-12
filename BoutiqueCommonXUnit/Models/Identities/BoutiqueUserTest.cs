using System.Linq;
using BoutiqueCommonXUnit.Data.Authorize;
using Xunit;

namespace BoutiqueCommonXUnit.Models.Identities
{
    public class BoutiqueUserTest
    {
        /// <summary>
        /// Проверка идентичности
        /// </summary>
        [Fact]
        public void BoutiqueUser_Equal_BoutiqueUser()
        {
            var first = IdentityData.BoutiqueUsers.First();
            var second = IdentityData.BoutiqueUsers.Last();

            Assert.True(first.Equals(second));
        }

        /// <summary>
        /// Обновить поля
        /// </summary>
        [Fact]
        public void BoutiqueUser_Update()
        {
            var user = IdentityData.BoutiqueUsers.First();
            const string name = "name";
            const string surname = "surname";
            const string address = "address";
            const string phone = "+79224725788";
            var userUpdated = user.UpdatePersonal(name, surname, address, phone);

            Assert.Equal(user.Email, userUpdated.Email);
            Assert.Equal(name, userUpdated.Name);
            Assert.Equal(surname, userUpdated.Surname);
            Assert.Equal(address, userUpdated.Address);
            Assert.Equal(phone, userUpdated.Phone);
        }
    }
}