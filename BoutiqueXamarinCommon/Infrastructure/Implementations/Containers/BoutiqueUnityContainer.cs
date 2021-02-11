using System;
using BoutiqueCommon.Infrastructure.Interfaces.Container;
using Unity;

namespace BoutiqueXamarinCommon.Infrastructure.Implementations.Containers
{
    /// <summary>
    /// Обертка Unity контейнера
    /// </summary>
    public class BoutiqueUnityContainer: IBoutiqueContainer
    {
        public BoutiqueUnityContainer(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        /// <summary>
        /// Контейнер Unity
        /// </summary>
        private readonly IUnityContainer _unityContainer;

        /// <summary>
        /// Регистрация зависимости
        /// </summary>
        public void Register<TIn, TOut>()
            where TIn : class
            where TOut : TIn =>
            _unityContainer.RegisterType<TIn, TOut>();

        /// <summary>
        /// Регистрация зависимости через сущность
        /// </summary>
        public void Register<T>(T instance)
            where T : class =>
            _unityContainer.RegisterInstance(instance);

        /// <summary>
        /// Регистрация зависимости через функцию
        /// </summary>
        public void Register<T>(Func<IBoutiqueContainer, T> registration)
            where T : class =>
            _unityContainer.RegisterFactory<T>(unity => registration(new BoutiqueUnityContainer(unity)));

        /// <summary>
        /// Регистрация зависимости одиночки
        /// </summary>
        public void RegisterSingleton<TIn, TOut>()
            where TIn : class
            where TOut : TIn =>
            _unityContainer.RegisterSingleton<TIn, TOut>();

        /// <summary>
        /// Получить зависимость
        /// </summary>
        public T Resolve<T>()
             where T : class =>
            _unityContainer.Resolve<T>();
    }
}