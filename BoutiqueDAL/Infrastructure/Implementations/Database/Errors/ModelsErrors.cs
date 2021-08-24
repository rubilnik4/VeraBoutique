using System.Collections.Generic;
using BoutiqueCommon.Models.Domain.Interfaces.Base;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using Functional.Models.Enums;
using Functional.Models.Implementations.Results;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDAL.Infrastructure.Implementations.Database.Errors
{
    /// <summary>
    /// Ошибки в сущностях
    /// </summary>
    public static class ModelsErrors
    {
        /// <summary>
        /// Ошибка некорректного поля
        /// </summary>
        public static IErrorResult FieldNotValid<TId, TDomain>(string fieldName)
            where TDomain : IDomainModel<TId>
            where TId : notnull =>
            new ErrorTypeResult<>(ErrorResultType.ValueNotValid, $"Некорректное значение {fieldName} в [{typeof(TDomain).Name}]");

        /// <summary>
        /// Ошибка некорректного поля
        /// </summary>
        public static IErrorResult FieldNotValid<TId, TDomain>(string fieldName, TDomain domain)
            where TDomain : IDomainModel<TId>
            where TId : notnull =>
            FieldNotValid<TId, TDomain>("Некорректное значение", fieldName, domain);

        /// <summary>
        /// Ошибка некорректного поля
        /// </summary>
        public static IErrorResult FieldNotValid<TId, TDomain>(int min, string fieldName, TDomain domain)
            where TDomain : IDomainModel<TId>
            where TId : notnull =>
            new ErrorTypeResult<>(ErrorResultType.ValueNotValid, $"Меньше допустимого значения {min} {fieldName} в [{domain.GetType().Name}]{domain.Id}");

        /// <summary>
        /// Ошибка некорректного поля
        /// </summary>
        public static IErrorResult FieldNotValid<TId, TDomain>(int min, int max, string fieldName, TDomain domain)
            where TDomain : IDomainModel<TId>
            where TId : notnull =>
            new ErrorTypeResult<>(ErrorResultType.ValueNotValid, $"Превышает допустимые пределы от {min} до {max} {fieldName} в [{domain.GetType().Name}]{domain.Id}");

        /// <summary>
        /// Ошибка некорректного поля
        /// </summary>
        public static IErrorResult FieldNotValid<TId, TDomain>(string description, string fieldName, TDomain domain)
            where TDomain : IDomainModel<TId>
            where TId : notnull =>
            new ErrorTypeResult<>(ErrorResultType.ValueNotValid, $"{description} {fieldName} в [{domain.GetType().Name}]{domain.Id}");
    }
}