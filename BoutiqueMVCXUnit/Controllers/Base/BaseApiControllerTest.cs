using System;
using System.Linq;
using System.Threading.Tasks;
using BoutiqueMVC.Controllers.Implementations.Base;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace BoutiqueMVCXUnit.Controllers.Base
{
    ///// <summary>
    ///// Базовый контроллер для Api. Тесты
    ///// </summary>
    //public class BaseApiControllerTest : ApiController
    //{
    //    /// <summary>
    //    /// Получить информацию о создаваемом объекте на основе контроллера
    //    /// </summary>
    //    [Fact]
    //    public void GetCreateAction_ByGenderController()
    //    {
    //        var values = Enumerable.Range(1, 3).ToList();

    //        var createdActionCollection = GetCreateAction(values);

    //        Assert.Equal(nameof(GetById), createdActionCollection.ActionGetName);
    //        Assert.Equal(nameof(BaseApiControllerTest), createdActionCollection.ControllerName);
    //        Assert.True(values.SequenceEqual(createdActionCollection.Values));
    //    }

    //    /// <summary>
    //    /// Базовый метод получения данных
    //    /// </summary>
    //    public override Task<IActionResult> Get() => throw new NotImplementedException();

    //    /// <summary>
    //    /// Базовый метод отправки данных
    //    /// </summary>
    //    public override Task<IActionResult> GetById<TId>(TId id) => throw new NotImplementedException();

    //}
}