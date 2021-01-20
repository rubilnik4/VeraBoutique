using System.Collections.Generic;
using BoutiqueCommon.Infrastructure.Interfaces.Logger;
using Functional.Models.Interfaces.Result;

namespace BoutiqueXamarin.Infrastructure.Implementations
{
    /// <summary>
    /// Логгер для клиента
    /// </summary>
    public class BoutiqueXamarinLogger: IBoutiqueLogger
    {
        public void ShowMessage(string message)
        {
            
        }

        public void ShowError(IResultError resultError)
        {
           
        }

        public void ShowErrors(IEnumerable<IErrorResult> errors)
        {
           
        }
    }
}