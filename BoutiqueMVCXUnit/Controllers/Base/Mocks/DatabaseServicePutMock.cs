using System;
using System.Collections.Generic;
using BoutiqueCommonXUnit.Data;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueCommonXUnit.Data.Models.Interfaces;
using BoutiqueMVCXUnit.Data.Database.Interfaces;
using Functional.FunctionalExtensions.Sync;
using Functional.Models.Implementations.Result;
using Functional.Models.Interfaces.Result;
using Moq;

namespace BoutiqueMVCXUnit.Controllers.Base.Mocks
{
    /// <summary>
    /// Тестовые сервис обновления данных
    /// </summary>
    public class DatabaseServicePutMock
    {
        /// <summary>
        /// Получить тестовый сервис работы с базой данных
        /// </summary>
        public static Mock<ITestDatabaseService> GetTestDatabaseTable(IResultCollection<ITestDomain> testDomains) =>
            GetTestDatabaseTable(testDomains, PutOkFunc(testDomains));

        /// <summary>
        /// Получить тестовый сервис работы с базой данных
        /// </summary>
        public static Mock<ITestDatabaseService> GetTestDatabaseTable(IResultCollection<ITestDomain> testDomains,
                                                                      Func<IResultError> putFunc) =>
            new Mock<ITestDatabaseService>().
            Void(serviceMock => serviceMock.Setup(service => service.Get()).
                                            ReturnsAsync(testDomains)).
            Void(serviceMock => serviceMock.Setup(service => service.Put(It.IsAny<ITestDomain>())).
                                            ReturnsAsync(putFunc));


        /// <summary>
        /// Функция изменения
        /// </summary>
        public static Func<IResultError> PutOkFunc(IResultError testResult) =>
            () => testResult;

        /// <summary>
        /// Функция изменения. Элемент не найден
        /// </summary>
        public static Func<IResultError> PutNotFoundFunc() =>
            () => new ResultError(ErrorData.NotFoundError);
    }
}