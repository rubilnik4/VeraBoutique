using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using BoutiqueCommon.Infrastructure.Implementation.Validation;
using BoutiqueCommon.Models.Common.Implementations.Identity;
using BoutiqueCommon.Models.Domain.Implementations.Identity;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using BoutiqueDAL.Models.Enums.Identity;
using BoutiqueDAL.Models.Implementations.Identity;
using BoutiqueMVC.Infrastructure.Implementation;
using BoutiqueMVC.Infrastructure.Implementation.Validation;
using BoutiqueMVC.Models.Implementations.Environment;
using BoutiqueMVC.Models.Implementations.Identity;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Enums;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Implementations.Errors.Base;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;
using Microsoft.AspNetCore.Identity;

namespace BoutiqueMVC.Factories.Identity
{
    /// <summary>
    /// Получить пользователя администратора
    /// </summary>
    public static class AuthorizeFactory
    {
        /// <summary>
        /// Пользователи по умолчанию
        /// </summary>
        public static IReadOnlyCollection<BoutiqueRoleUser> DefaultUsers =>
            DefaultAdminUser.OkStatus 
                ? new List<BoutiqueRoleUser> { DefaultAdminUser.Value }
                : throw new ArgumentNullException(nameof(DefaultUsers));

        /// <summary>
        /// Роли
        /// </summary>
        public static IReadOnlyCollection<string> RoleNames =>
            Enum.GetValues<IdentityRoleType>().
            Select(roleType => roleType.ToString()).
            ToList();

        /// <summary>
        /// Пользователь администратор по умолчанию
        /// </summary>
        private static IResultValue<BoutiqueRoleUser> DefaultAdminUser =>
            new ResultValue<Func<IdentityRoleType, IAuthorizeDomain, IPersonalDomain, BoutiqueRoleUser>>(GetDefaultUser).
            ResultValueCurryOk(IdentityRoleType).
            ResultValueCurryOk(AuthorizeValidation.AuthorizeValidate(new AuthorizeDomain(Email, Password), AuthorizeSettings)).
            ResultValueCurryOk(PersonalValidation.PersonalValidate(new PersonalDomain(Name, Surname, Address, Phone))).
            ResultValueOk(getDefaultUser => getDefaultUser.Invoke());

        /// <summary>
        /// Получить пользователя о умолчанию
        /// </summary>
        private static BoutiqueRoleUser GetDefaultUser(IdentityRoleType identityRoleType, IAuthorizeDomain authorize,
                                                       IPersonalDomain personal) =>
            new BoutiqueUser(authorize.Email, authorize.Password, personal.Name,
                             personal.Surname, personal.Address, personal.Phone).
            Map(boutiqueUser => new BoutiqueRoleUser(identityRoleType, boutiqueUser));

        /// <summary>
        /// Почта
        /// </summary>
        private static IResultValue<IdentityRoleType> IdentityRoleType =>
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
        private static AuthorizeSettings AuthorizeSettings =>
            new (8, true, false, true);
    }
}