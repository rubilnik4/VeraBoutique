using System;
using System.Net.Http;
using Functional.Models.Enums;
using Functional.Models.Implementations.Errors;
using Functional.Models.Implementations.Errors.AuthorizeErrors;
using Functional.Models.Implementations.Errors.Base;
using Functional.Models.Implementations.Errors.CommonErrors;
using Functional.Models.Implementations.Errors.ConvertionErrors;
using Functional.Models.Implementations.Errors.DatabaseErrors;
using Functional.Models.Implementations.Errors.RestErrors;
using Functional.Models.Interfaces.Errors.CommonErrors;
using Functional.Models.Interfaces.Errors.ConvertionErrors;
using Functional.Models.Interfaces.Errors.DatabaseErrors;
using Xunit;

namespace FunctionalXUnit.Models.Errors
{
    /// <summary>
    /// Фабрика создания типов ошибок. Тесты
    /// </summary>
    public class ErrorResultFactoryTest
    {
        /// <summary>
        /// Ошибка с неопределенным типом
        /// </summary>
        [Fact]
        public void ErrorType()
        {
            var errorResult = ErrorResultFactory.ErrorType(CommonErrorType.Unknown, "Ошибка");

            Assert.IsType<ErrorTypeResult<CommonErrorType>>(errorResult);
            Assert.IsType<ErrorTypeResult<CommonErrorType>>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Общая ошибка
        /// </summary>
        [Fact]
        public void CommonError()
        {
            var errorResult = ErrorResultFactory.CommonError(CommonErrorType.Unknown, "Ошибка");

            Assert.IsType<CommonErrorResult>(errorResult);
            Assert.IsType<CommonErrorResult>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка не найденного значения
        /// </summary>
        [Fact]
        public void ValueNotFoundError()
        {
            var errorResult = ErrorResultFactory.ValueNotFoundError(String.Empty, typeof(string));

            Assert.IsAssignableFrom<IValueNotFoundErrorResult>(errorResult);
            Assert.IsAssignableFrom<IValueNotFoundErrorResult>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка неверного значения
        /// </summary>
        [Fact]
        public void ValueNotValidError()
        {
            var errorResult = ErrorResultFactory.ValueNotValidError(String.Empty, typeof(string), "Ошибка");

            Assert.IsAssignableFrom<IValueNotValidErrorResult>(errorResult);
            Assert.IsAssignableFrom<IValueNotValidErrorResult>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка авторизации
        /// </summary>
        [Fact]
        public void AuthorizeError()
        {
            var errorResult = ErrorResultFactory.AuthorizeError(AuthorizeErrorType.Password, "Ошибка");

            Assert.IsType<AuthorizeErrorResult>(errorResult);
            Assert.IsType<AuthorizeErrorResult>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка базы данных
        /// </summary>
        [Fact]
        public void DatabaseError()
        {
            var errorResult = ErrorResultFactory.DatabaseError(DatabaseErrorType.Connection, "Ошибка");

            Assert.IsType<DatabaseErrorResult>(errorResult);
            Assert.IsType<DatabaseErrorResult>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка базы данных
        /// </summary>
        [Fact]
        public void DatabaseTableError()
        {
            var errorResult = ErrorResultFactory.DatabaseTableError("Table", "Ошибка");

            Assert.IsType<DatabaseTableErrorResult>(errorResult);
            Assert.IsType<DatabaseTableErrorResult>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка базы данных
        /// </summary>
        [Fact]
        public void DatabaseConnectionError()
        {
            var errorResult = ErrorResultFactory.DatabaseConnectionError("Table", "Ошибка");

            Assert.IsType<DatabaseConnectionErrorResult>(errorResult);
            Assert.IsType<DatabaseConnectionErrorResult>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка базы данных
        /// </summary>
        [Fact]
        public void DatabaseValueNotValidError()
        {
            var errorResult = ErrorResultFactory.DatabaseValueNotValidError(String.Empty, "Table", "Ошибка");

            Assert.IsAssignableFrom<IDatabaseValueNotValidErrorResult>(errorResult);
            Assert.IsAssignableFrom<IDatabaseValueNotValidErrorResult>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка базы данных
        /// </summary>
        [Fact]
        public void DatabaseValueNotFoundError()
        {
            var errorResult = ErrorResultFactory.DatabaseValueNotFoundError(String.Empty, "Table", "Ошибка");

            Assert.IsAssignableFrom<IDatabaseValueNotFoundErrorResult>(errorResult);
            Assert.IsAssignableFrom<IDatabaseValueNotFoundErrorResult>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка базы данных
        /// </summary>
        [Fact]
        public void DatabaseValueDuplicateError()
        {
            var errorResult = ErrorResultFactory.DatabaseValueDuplicateError(String.Empty, "Table", "Ошибка");

            Assert.IsAssignableFrom<IDatabaseValueDuplicatedErrorResult>(errorResult);
            Assert.IsAssignableFrom<IDatabaseValueDuplicatedErrorResult>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка rest сервиса
        /// </summary>
        [Fact]
        public void RestHostError()
        {
            var errorResult = ErrorResultFactory.RestError(RestErrorType.BadRequest, "host", "Ошибка");

            Assert.IsType<RestErrorResult>(errorResult);
            Assert.IsType<RestErrorResult>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка rest сервиса
        /// </summary>
        [Fact]
        public void RestMessageError()
        {
            var errorResult = ErrorResultFactory.RestError(RestErrorType.BadRequest, new HttpResponseMessage(), "Ошибка");

            Assert.IsType<RestMessageErrorResult>(errorResult);
            Assert.IsType<RestMessageErrorResult>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка сериализации
        /// </summary>
        [Fact]
        public void SerializeError()
        {
            var errorResult = ErrorResultFactory.SerializeError(ConvertionErrorType.JsonConvertion, String.Empty, "Ошибка");

            Assert.IsAssignableFrom<ISerializeErrorResult>(errorResult);
            Assert.IsAssignableFrom<ISerializeErrorResult>(errorResult.AppendException(new Exception()));
        }

        /// <summary>
        /// Ошибка десериализации
        /// </summary>
        [Fact]
        public void DeserializeError()
        {
            var errorResult = ErrorResultFactory.DeserializeError<string>(ConvertionErrorType.JsonConvertion, String.Empty, "Ошибка");

            Assert.IsAssignableFrom<IDeserializeErrorResult>(errorResult);
            Assert.IsAssignableFrom<IDeserializeErrorResult>(errorResult.AppendException(new Exception()));
        }
    }
}