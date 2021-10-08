using System;
using BoutiqueCommon.Infrastructure.Implementation.Validation.Common;
using BoutiqueCommon.Models.Domain.Implementations.Identities;
using BoutiqueCommon.Models.Domain.Interfaces.Identities;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueCommon.Infrastructure.Implementation.Validation.Identities
{
    /// <summary>
    /// Проверка личных данных
    /// </summary>
    public static class PersonalValidation
    {
        /// <summary>
        /// Проверить личные данные
        /// </summary>
        public static IResultValue<IPersonalDomain> PersonalValidate(IPersonalDomain personal) =>
            new ResultValue<Func<string, string, string, string, IPersonalDomain>>(GetPersonal).
            ResultValueCurryOk(NameValidate(personal.Name)).
            ResultValueCurryOk(SurnameValidate(personal.Surname)).
            ResultValueCurryOk(AddressValidate(personal.Address)).
            ResultValueCurryOk(PhoneValidate(personal.Phone)).
            ResultValueOk(getPersonal => getPersonal.Invoke());

        /// <summary>
        /// Получить личные данные
        /// </summary>
        private static IPersonalDomain GetPersonal(string name, string surname, string address, string phone) =>
            new PersonalDomain(name, surname, address, phone);

        /// <summary>
        /// Проверка имени
        /// </summary>
        private static IResultValue<string> NameValidate(string name) =>
            name.ToResultValueWhere(TextValidation.IsValid,
                                    _ => ErrorResultFactory.ValueNotValidError(name, typeof(RegisterValidation), "Некорректное имя пользователя"));

        /// <summary>
        /// Проверка фамилии
        /// </summary>
        private static IResultValue<string> SurnameValidate(string surname) =>
            surname.ToResultValueWhere(TextValidation.IsValid,
                                       _ => ErrorResultFactory.ValueNotValidError(surname, typeof(RegisterValidation), "Некорректная фамилия пользователя"));

        /// <summary>
        /// Проверка адреса
        /// </summary>
        private static IResultValue<string> AddressValidate(string address) =>
            address.ToResultValueWhere(EmptyValidation.IsValid,
                                       _ => ErrorResultFactory.ValueNotValidError(address, typeof(RegisterValidation), "Некорректный адрес"));

        /// <summary>
        /// Проверка телефона
        /// </summary>
        private static IResultValue<string> PhoneValidate(string phone) =>
            phone.ToResultValueWhere(PhoneValidation.IsValid,
                                     _ => ErrorResultFactory.ValueNotValidError(phone, typeof(RegisterValidation), "Некорректный телефон"));
    }
}