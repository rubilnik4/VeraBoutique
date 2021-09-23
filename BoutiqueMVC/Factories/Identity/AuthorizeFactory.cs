using System;
using System.Collections.Generic;
using System.Configuration;
using BoutiqueCommon.Infrastructure.Implementation.Validation;
using BoutiqueCommon.Models.Common.Implementations.Identity;
using BoutiqueCommon.Models.Domain.Implementations.Identity;
using BoutiqueCommon.Models.Domain.Interfaces.Identity;
using BoutiqueDAL.Models.Enums.Identity;
using BoutiqueDAL.Models.Implementations.Identity;
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
        public static IResultCollection<BoutiqueRoleUser> DefaultUsers =>
            DefaultAdminUser.ResultValueOkToCollection(adminUser => new List<BoutiqueRoleUser> { adminUser });

        /// <summary>
        /// Пользователь администратор по умолчанию
        /// </summary>
        private static IResultValue<BoutiqueRoleUser> DefaultAdminUser =>
            new ResultValue<Func<IdentityRoleType, IAuthorizeDomain, IPersonalDomain, BoutiqueRoleUser>>(GetDefaultUser).
            ResultValueCurryOk(IdentityRoleType).
            ResultValueCurryOk(Authorize).
            ResultValueCurryOk(Personal).
            ResultValueOk(getDefaultUser => getDefaultUser.Invoke());

        /// <summary>
        /// Имя пользователя и пароль
        /// </summary>
        private static IResultValue<IAuthorizeDomain> Authorize =>
            new ResultValue<Func<string, string, IAuthorizeDomain>>(GetAuthorize).
            ResultValueCurryOk(Email).
            ResultValueCurryOk(Password).
            ResultValueOk(getAuthorize => getAuthorize.Invoke());

        /// <summary>
        /// Личные данные
        /// </summary>
        private static IResultValue<IPersonalDomain> Personal =>
            new ResultValue<Func<string, string, string, string, IPersonalDomain>>(GetPersonal).
            ResultValueCurryOk(Name).
            ResultValueCurryOk(Surname).
            ResultValueCurryOk(Address).
            ResultValueCurryOk(Phone).
            ResultValueOk(getPersonal => getPersonal.Invoke());

        /// <summary>
        /// Получить пользователя о умолчанию
        /// </summary>
        private static BoutiqueRoleUser GetDefaultUser(IdentityRoleType identityRoleType, IAuthorizeDomain authorize,
                                                       IPersonalDomain personal) =>
            new BoutiqueUser(authorize.Email, authorize.Password, personal.Name,
                             personal.Surname, personal.Address, personal.Phone).
            Map(boutiqueUser => new BoutiqueRoleUser(identityRoleType, boutiqueUser));

        /// <summary>
        /// Получить имя пользователя и пароль
        /// </summary>
        private static IAuthorizeDomain GetAuthorize(string email, string password) =>
            new AuthorizeDomain(email, password);

        /// <summary>
        /// Получить личные данные
        /// </summary>
        private static IPersonalDomain GetPersonal(string name, string surname, string address, string phone) =>
            new PersonalDomain(name, surname, address, phone);

        /// <summary>
        /// Почта
        /// </summary>
        private static IResultValue<IdentityRoleType> IdentityRoleType =>
            Environment.GetEnvironmentVariable(IdentityUserEnvironment.IDENTITY_ROLE_TYPE).
            ToResultValueWhereNull(roleType => Enum.TryParse(roleType, out IdentityRoleType _),
                               _ => ErrorResultFactory.ValueNotFoundError(IdentityUserEnvironment.IDENTITY_ROLE_TYPE, typeof(AuthorizeFactory))).
            ResultValueOk(Enum.Parse<IdentityRoleType>);

        /// <summary>
        /// Почта
        /// </summary>
        private static IResultValue<string> Email =>
            Environment.GetEnvironmentVariable(IdentityUserEnvironment.EMAIL).
            ToResultValueWhereNull(EmailValidation.IsValid,
                                   _ => ErrorResultFactory.AuthorizeError(AuthorizeErrorType.Email, "Почта умолчанию не найдена"));

        /// <summary>
        /// Пароль
        /// </summary>
        private static IResultValue<string> Password =>
            Environment.GetEnvironmentVariable(IdentityUserEnvironment.PASSWORD).
            ToResultValueWhereNull(password => PasswordValidation.IsValid(password, 8, true),
                                   _ => ErrorResultFactory.AuthorizeError(AuthorizeErrorType.Password, "Пароль умолчанию не найден"));

        /// <summary>
        /// Имя
        /// </summary>
        private static IResultValue<string> Name =>
            Environment.GetEnvironmentVariable(IdentityUserEnvironment.NAME).
            ToResultValueWhereNull(EmptyValidation.IsValid, 
                                   _ => ErrorResultFactory.ValueNotFoundError(IdentityUserEnvironment.NAME, typeof(AuthorizeFactory)));

        /// <summary>
        /// Фамилия
        /// </summary>
        private static IResultValue<string> Surname =>
            Environment.GetEnvironmentVariable(IdentityUserEnvironment.SURNAME).
            ToResultValueWhereNull(EmptyValidation.IsValid,
                                   _ => ErrorResultFactory.ValueNotFoundError(IdentityUserEnvironment.SURNAME, typeof(AuthorizeFactory)));

        /// <summary>
        /// Адрес
        /// </summary>
        private static IResultValue<string> Address =>
            Environment.GetEnvironmentVariable(IdentityUserEnvironment.ADDRESS).
            ToResultValueWhereNull(EmptyValidation.IsValid,
                                   _ => ErrorResultFactory.ValueNotFoundError(IdentityUserEnvironment.ADDRESS, typeof(AuthorizeFactory)));

        /// <summary>
        /// Телефон
        /// </summary>
        private static IResultValue<string> Phone =>
            Environment.GetEnvironmentVariable(IdentityUserEnvironment.PHONE).
            ToResultValueWhereNull(PhoneValidation.IsValid,
                                   _ => ErrorResultFactory.AuthorizeError(AuthorizeErrorType.Phone, "Телефон умолчанию не найден"));
    }
}