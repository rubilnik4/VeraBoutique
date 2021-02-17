using System.Linq;
using BoutiqueCommon.Infrastructure.Interfaces.Container;
using BoutiqueCommon.Models.Domain.Implementations.Clothes;
using BoutiqueCommon.Models.Domain.Implementations.Clothes.GenderDomains;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes;
using BoutiqueCommon.Models.Domain.Interfaces.Clothes.Genders;
using BoutiqueCommonXUnit.Data.Clothes;
using BoutiqueXamarinCommon.Infrastructure.Implementations.Containers;
using BoutiqueXamarinCommonXUnit.Data.Models.Implementations;
using BoutiqueXamarinCommonXUnit.Data.Models.Interfaces;
using Unity;
using Xunit;

namespace BoutiqueXamarinCommonXUnit.Infrastructure.Containers
{
    public class BoutiqueUnityContainerTest
    {
        /// <summary>
        /// Регистрация зависимости
        /// </summary>
        [Fact]
        public void Register()
        {
            var boutiqueContainer = BoutiqueContainer;

            boutiqueContainer.Register<ITestContainer, TestContainer>();
            var testContainer = boutiqueContainer.Resolve<ITestContainer>();

            Assert.IsType<TestContainer>(testContainer);
        }

        /// <summary>
        /// Регистрация зависимости
        /// </summary>
        [Fact]
        public void RegisterSingleton()
        {
            var boutiqueContainer = BoutiqueContainer;

            boutiqueContainer.RegisterSingleton<ITestContainer, TestContainer>();
            var testContainer = boutiqueContainer.Resolve<ITestContainer>();

            Assert.IsType<TestContainer>(testContainer);
        }

        /// <summary>
        /// Регистрация зависимости через сущность
        /// </summary>
        [Fact]
        public void RegisterInstance()
        {
            var gender = GenderData.GenderDomains.First();
            var boutiqueContainer = BoutiqueContainer;

            boutiqueContainer.Register(gender);
            var genderContainer = boutiqueContainer.Resolve<IGenderDomain>();

            Assert.IsType<GenderDomain>(genderContainer);
            Assert.True(genderContainer.Equals(gender));
        }

        /// <summary>
        /// Регистрация зависимости через функцию
        /// </summary>
        [Fact]
        public void RegisterFactory()
        {
            var gender = GenderData.GenderDomains.First();
            const string genderName = "genderName";
            var boutiqueContainer = BoutiqueContainer;

            boutiqueContainer.Register<ITestContainer, TestContainer>();
            boutiqueContainer.Register(container => container.Resolve<ITestContainer>().CopyGender(gender, genderName));
            var genderContainer = boutiqueContainer.Resolve<IGenderDomain>();

            Assert.IsType<GenderDomain>(genderContainer);
            Assert.Equal(genderName, genderContainer.Name);
        }


        /// <summary>
        /// Контейнер зависимостей
        /// </summary>
        private static IBoutiqueContainer BoutiqueContainer =>
            new BoutiqueUnityContainer(new UnityContainer());
    }
}