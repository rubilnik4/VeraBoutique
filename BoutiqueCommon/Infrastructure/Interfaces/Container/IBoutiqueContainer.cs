using System;

namespace BoutiqueCommon.Infrastructure.Interfaces.Container
{
    /// <summary>
    /// Контейнер зависимостей
    /// </summary>
    public interface IBoutiqueContainer
    {
        /// <summary>
        /// Регистрация зависимости
        /// </summary>
        void Register<TIn, TOut>()
            where TIn : class
            where TOut : TIn;

        /// <summary>
        /// Регистрация зависимости через сущность
        /// </summary>
        void Register<T>(T instance)
            where T : class;

        /// <summary>
        /// Регистрация зависимости через функцию
        /// </summary>
        void Register<T>(Func<IBoutiqueContainer, T> registration)
            where T : class;

        /// <summary>
        /// Регистрация зависимости одиночки
        /// </summary>
        void RegisterSingleton<TIn, TOut>()
            where TIn : class
            where TOut : TIn;

        /// <summary>
        /// Получить зависимость
        /// </summary>
        T Resolve<T>()
             where T : class;
    }
}