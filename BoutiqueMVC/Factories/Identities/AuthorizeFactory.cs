using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using BoutiqueCommon.Infrastructure.Implementation.Validation.Identities;
using BoutiqueCommon.Models.Domain.Implementations.Identities;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using BoutiqueCommon.Models.Enums.Identities;
using BoutiqueDAL.Models.Implementations.Entities.Identities;
using BoutiqueDAL.Models.Implementations.Identities;
using BoutiqueMVC.Models.Implementations.Environment;
using BoutiqueMVC.Models.Implementations.Identity;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueMVC.Factories.Identities
{
    /// <summary>
    /// Получить пользователя администратора
    /// </summary>
    public static class AuthorizeFactory
    {
        /// <summary>
        /// Пользователи по умолчанию
        /// </summary>
        public static IReadOnlyCollection<IRegisterRoleDomain> DefaultUsers =>
            DefaultAdminUser.OkStatus
                ? new List<IRegisterRoleDomain> { DefaultAdminUser.Value }
                : throw new ArgumentNullException(nameof(DefaultUsers));

        /// <summary>
        /// Роли
        /// </summary>
        public static IReadOnlyCollection<IdentityRoleType> RoleNames =>
            Enum.GetValues<IdentityRoleType>().
            Where(role => role != IdentityRoleType.Unknown).
            ToList();

        /// <summary>
        /// Пользователь администратор по умолчанию
        /// </summary>
        private static IResultValue<IRegisterRoleDomain> DefaultAdminUser =>
            new ResultValue<Func<IAuthorizeDomain, IPersonalDomain, IdentityRoleType, IRegisterRoleDomain>>(GetDefaultUser).
            ResultValueCurryOk(AuthorizeValidation.AuthorizeValidate(new AuthorizeDomain(Email, Password),
                                                                     IdentitySettings.ToPasswordSettings())).
            ResultValueCurryOk(PersonalValidation.PersonalValidate(new PersonalDomain(Name, Surname, Address, Phone))).
              ResultValueCurryOk(GetIdentityRoleType()).
            ResultValueOk(getDefaultUser => getDefaultUser.Invoke());

        /// <summary>
        /// Получить пользователя о умолчанию
        /// </summary>
        private static IRegisterRoleDomain GetDefaultUser(IAuthorizeDomain authorize, IPersonalDomain personal,
                                                          IdentityRoleType identityRoleType) =>
            new RegisterRoleDomain(authorize, personal, identityRoleType);

        /// <summary>
        /// Почта
        /// </summary>
        private static IResultValue<IdentityRoleType> GetIdentityRoleType() =>
            Environment.GetEnvironmentVariable(IdentityUserEnvironment.IDENTITY_ROLE_TYPE).
            ToResultValueWhereNull(roleType => Enum.TryParse(roleType, true, out IdentityRoleType _),
                               _ => ErrorResultFactory.ValueNotFoundError(IdentityUserEnvironment.IDENTITY_ROLE_TYPE, typeof(AuthorizeFactory))).
            ResultValueOk(roleType => Enum.Parse<IdentityRoleType>(roleType, true));

        /// <summary>
        /// Почта
        /// </summary>
        private static string Email =>
            Environment.GetEnvironmentVariable(IdentityUserEnvironment.EMAIL)
            ?? throw new ConfigurationErrorsException();

        /// <summary>
        /// Пароль
        /// </summary>
        private static string Password =>
            Environment.GetEnvironmentVariable(IdentityUserEnvironment.PASSWORD)
            ?? throw new ConfigurationErrorsException();

        /// <summary>
        /// Имя
        /// </summary>
        private static string Name =>
            Environment.GetEnvironmentVariable(IdentityUserEnvironment.NAME)
            ?? throw new ConfigurationErrorsException();

        /// <summary>
        /// Фамилия
        /// </summary>
        private static string Surname =>
            Environment.GetEnvironmentVariable(IdentityUserEnvironment.SURNAME)
            ?? throw new ConfigurationErrorsException();

        /// <summary>
        /// Адрес
        /// </summary>
        private static string Address =>
            Environment.GetEnvironmentVariable(IdentityUserEnvironment.ADDRESS)
            ?? throw new ConfigurationErrorsException();

        /// <summary>
        /// Телефон
        /// </summary>
        private static string Phone =>
            Environment.GetEnvironmentVariable(IdentityUserEnvironment.PHONE)
            ?? throw new ConfigurationErrorsException();

        /// <summary>
        /// Параметры авторизации
        /// </summary>
        private static IdentitySettings IdentitySettings =>
            new(8, true, false, true);
    }
}