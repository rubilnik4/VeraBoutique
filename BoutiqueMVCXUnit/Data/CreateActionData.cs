using System.Linq;
using BoutiqueCommonXUnit.Data.Models.Implementations;
using BoutiqueDTOXUnit.Data.Models.Interfaces;
using BoutiqueMVC.Models.Implementations.Controller;
using ResultFunctional.FunctionalExtensions.Sync;

namespace BoutiqueMVCXUnit.Data
{
    /// <summary>
    /// Информация о создаваемом объекте
    /// </summary>
    public static class CreateActionData
    {
        /// <summary>
        /// Информация о создаваемом объекте со значением
        /// </summary>
        public static CreatedActionValue<TestEnum, ITestTransfer> CreatedActionValue =>
            TransferData.GetTestTransfers().First().
            Map(transfer => (transfer.Id, transfer)).
            Map(idValue => new CreatedActionValue<TestEnum, ITestTransfer>("action", "controller", idValue));

        /// <summary>
        /// Информация о создаваемом объекте с коллекцией
        /// </summary>
        public static CreatedActionCollection<TestEnum, ITestTransfer> CreatedActionCollection =>
            TransferData.GetTestTransfers().
            Select(transfer => (transfer.Id, transfer)).ToList().
            Map(idValues => new CreatedActionCollection<TestEnum, ITestTransfer>("action", "controller", idValues));
    }
}