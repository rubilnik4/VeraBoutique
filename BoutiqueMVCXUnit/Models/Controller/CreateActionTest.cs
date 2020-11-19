using System.Collections.Generic;
using System.Linq;
using BoutiqueMVC.Models.Implementations.Controller;
using Xunit;

namespace BoutiqueMVCXUnit.Models.Controller
{
    /// <summary>
    /// Информация о создаваемом объекте. Тесты
    /// </summary>
    public class CreateActionTest
    {
        /// <summary>
        /// Преобразовать в ответ контроллера о создании объекта со значением
        /// </summary>
        [Fact]
        public void CreatedActionResult_Value()
        {
            const string actionName = "action";
            const string controllerName = "controller";
            const int value = 1;
            const int id = 2;
            var createdActionValue = new CreatedActionValue<int, int>(actionName, controllerName, (id, value));

            var createdActionResult = createdActionValue.ToCreatedAtActionResult();

            Assert.Equal(actionName, createdActionResult.ActionName);
            Assert.Equal(controllerName, createdActionResult.ControllerName);
            Assert.Equal(value, createdActionResult.Value);
            Assert.Equal(id, createdActionResult.RouteValues.First().Value);
        }

        /// <summary>
        /// Преобразовать в ответ контроллера о создании объекта с коллекцией
        /// </summary>
        [Fact]
        public void CreatedActionResult_Collection()
        {
            const string actionName = "action";
            const string controllerName = "controller";
            var values = new List<int> { 1, 2, 3 };
            var ids = new List<int> { 4, 5, 6 };
            var idValues = ids.Zip(values);
            var createdActionValue = new CreatedActionCollection<int, int>(actionName, controllerName, idValues);

            var createdActionResult = createdActionValue.ToCreatedAtActionResult();

            Assert.Equal(actionName, createdActionResult.ActionName);
            Assert.Equal(controllerName, createdActionResult.ControllerName);
            Assert.True(values.SequenceEqual((IEnumerable<int>)createdActionResult.Value));
            Assert.True(ids.SequenceEqual((IEnumerable<int>)createdActionResult.RouteValues.First().Value));
        }
    }
}