using BoutiqueCommon.Models.Common.Implementations.Identities;
using BoutiqueCommon.Models.Common.Interfaces.Identities;
using BoutiqueCommon.Models.Domain.Implementations.Identities;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueCommon.Models.Enums.Identities;
using BoutiqueDAL.Models.Implementations.Entities.Identities;

namespace BoutiqueDAL.Models.Implementations.Identities
{
    /// <summary>
    /// Пользователь с ролью
    /// </summary>
    public class BoutiqueRoleUser
    {
        public BoutiqueRoleUser(IdentityRoleType identityRoleType, BoutiqueUserEntity boutiqueUserEntity)
        {
            IdentityRoleType = identityRoleType;
            BoutiqueUserEntity = boutiqueUserEntity;
        }

        /// <summary>
        /// Тип роли
        /// </summary>
        public IdentityRoleType IdentityRoleType { get; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public BoutiqueUserEntity BoutiqueUserEntity { get; }

        /// <summary>
        /// Получить пользователя
        /// </summary>
        public IBoutiqueUserDomain ToBoutiqueUser() =>
            BoutiqueUserEntity.ToBoutiqueUser(IdentityRoleType);

        /// <summary>
        /// Получить пользователя
        /// </summary>
        public static BoutiqueRoleUser GetBoutiqueUser(IBoutiqueUserDomain user) =>
            new(user.IdentityRoleType, BoutiqueUserEntity.GetBoutiqueUser(user));
    }
}