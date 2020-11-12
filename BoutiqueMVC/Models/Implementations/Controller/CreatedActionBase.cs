using System.Collections.Generic;

namespace BoutiqueMVC.Models.Implementations.Controller
{
    /// <summary>
    /// Информация о создаваемом объекте. Базовый класс
    /// </summary>
    public abstract class CreatedActionBase
    {
        protected CreatedActionBase(string actionGetName, string controllerName)
        {
            ActionGetName = actionGetName;
            ControllerName = controllerName;
        }

        /// <summary>
        /// Наименование пути для получения данных
        /// </summary>
        public string ActionGetName { get; }

        /// <summary>
        /// Наименование контроллера
        /// </summary>
        public string ControllerName { get; }
    }
}