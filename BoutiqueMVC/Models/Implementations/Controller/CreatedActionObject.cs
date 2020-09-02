using System.Collections.Generic;

namespace BoutiqueMVC.Models.Implementations.Controller
{
    /// <summary>
    /// Информация о создаваемом объекте
    /// </summary>
    public class CreatedActionObject
    {
        public CreatedActionObject(string actionGetName, string controllerName, IReadOnlyCollection<int> ids, IReadOnlyCollection<int> values)
        {
            ActionGetName = actionGetName;
            ControllerName = controllerName;
            Ids = ids;
            Values = values;
        }

        /// <summary>
        /// Наименование пути для получения данных
        /// </summary>
        public string ActionGetName { get; }

        /// <summary>
        /// Наименование контроллера
        /// </summary>
        public string ControllerName { get; }

        /// <summary>
        /// Идентификаторы элементов
        /// </summary>
        public IReadOnlyCollection<int> Ids { get; }

        /// <summary>
        /// Объекты
        /// </summary>
        public IReadOnlyCollection<int> Values { get; }
    }
}