using System.Linq;
using System.Threading.Tasks;
using BoutiqueCommon.Extensions.TaskExtensions;
using BoutiqueCommonXUnit.Mocks.Interfaces;
using Moq;
using Xunit;

namespace BoutiqueCommonXUnit.Extensions.TaskExtensions
{
    /// <summary>
    /// Методы расширения для задач. Тесты
    /// </summary>
    public class TastExtensionsTest
    {
        /// <summary>
        /// Выполнить все асинхронные функции и дождаться последней
        /// </summary>
        [Fact]
        public async Task WaitAllFunc_TaskComplete()
        {
            const int sequenceCount = 3;
            var tasks = Enumerable.Range(0, sequenceCount).Select(_ => Task.FromResult(1));

            var awaitedTasks = await tasks.WaitAll();

            var equalSequence = Enumerable.Range(0, sequenceCount).Select(_ => 1).ToList();
            Assert.True(awaitedTasks.SequenceEqual(equalSequence));
        }

        /// <summary>
        /// Выполнить все асинхронные действия и дождаться последней
        /// </summary>
        [Fact]
        public async Task WaitAllAction_TaskComplete()
        {
            const int sequenceCount = 3;
            var voidObjectMock = new Mock<IVoidObject>();
            var tasks = Enumerable.Range(0, sequenceCount).Select(_ => Task.Run(voidObjectMock.Object.TestVoid));

            await tasks.WaitAll();

            voidObjectMock.Verify(voidObject => voidObject.TestVoid(), Times.Exactly(sequenceCount));
        }
    }
}