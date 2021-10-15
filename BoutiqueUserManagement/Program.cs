using System;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using BoutiqueConsole.Infrastructure.Logger;
using BoutiqueUserManagement.Infrastructure.Implementations.Services;
using BoutiqueUserManagement.Infrastructure.Implementations.UserOperations;
using BoutiqueUserManagement.Models.Enums;
using ResultFunctional.FunctionalExtensions.Async.ResultExtension.ResultValues;
using ResultFunctional.FunctionalExtensions.Sync;
using ResultFunctional.FunctionalExtensions.Sync.ResultExtension.ResultValues;
using ResultFunctional.Models.Interfaces.Results;

namespace BoutiqueUserManagement
{
    public class Program
    {
        /// <summary>
        /// Стартовый метод
        /// </summary>
        public static async Task Main() =>
           await ChoiceUser().
           ResultValueVoidBad(errors => Console.WriteLine(errors.First().Description)).
           ResultValueOkAsync(operation => BoutiqueUserService.UserOperate(operation, new ConsoleBoutiqueLogger()));

        /// <summary>
        /// Выбрать тип операции
        /// </summary>
        private static IResultValue<UserOperationType> ChoiceUser() =>
            String.Empty.
            Void(_ => Console.WriteLine("Выберите тип операции:")).
            Void(_ => Console.WriteLine("\t[1] Чтение")).
            Void(_ => Console.WriteLine("\t[2] Удаление")).
            Map(_ => Console.ReadKey().KeyChar).
            Void(_ => Console.WriteLine("")).
            Map(UserOperations.GetUserOperation);
    }
}
