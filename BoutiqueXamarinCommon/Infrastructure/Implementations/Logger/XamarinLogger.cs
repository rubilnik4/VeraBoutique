using System.Collections.Generic;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using Functional.Models.Interfaces.Results;

namespace BoutiqueXamarinCommon.Infrastructure.Implementations.Logger
{
    /// <summary>
    /// Логгер для клиента
    /// </summary>
    public class BoutiqueXamarinLogger: IBoutiqueLogger
    {
        public void ShowMessage(string message)
        { }

        public void ShowError(IResultError resultError)
        { }

        public void ShowErrors(IEnumerable<IErrorResult> errors)
        { }
    }
}