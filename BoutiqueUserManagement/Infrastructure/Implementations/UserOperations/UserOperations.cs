using System;
using BoutiqueUserManagement.Models.Enums;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Implementations.Errors;
using ResultFunctional.Models.Implementations.Results;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueUserManagement.Infrastructure.Implementations.UserOperations
{
    /// <summary>
    /// Операции с пользователями
    /// </summary>
    public static class UserOperations
    {
        /// <summary>
        /// Получить тип операции
        /// </summary>
        public static IResultValue<UserOperationType> GetUserOperation(char operationUser) =>
            operationUser.ToString().
            WhereContinue(operation => Int32.TryParse(operation, out _),
                          operation => new ResultValue<int>(Int32.Parse(operation)),
                          operation => ErrorResultFactory.ValueNotValidError(operation, typeof(UserOperations), "Неправильный тип операции").
                                       ToResultValue<int>()).
            ResultValueOk(operation => operation - 1).
            ResultValueBindOk(GetUserOperation);

        /// <summary>
        /// Получить тип операции
        /// </summary>
        private static IResultValue<UserOperationType> GetUserOperation(int operationUser) =>
            operationUser.ToString().
            WhereContinue(operation => Enum.TryParse<UserOperationType>(operation, out _),
                          operation => new ResultValue<UserOperationType>(Enum.Parse<UserOperationType>(operation)),
                          operation => ErrorResultFactory.ValueNotValidError(operation, typeof(UserOperations), "Невозможно преобразовать в тип операции").
                                       ToResultValue<UserOperationType>());


    }
}