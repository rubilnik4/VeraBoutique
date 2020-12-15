using System.Collections.Generic;
using System.Threading.Tasks;
using BoutiqueCommon.Models.Enums.Clothes;
using BoutiqueDTO.Infrastructure.Implementations.Services.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Refit.Clothes;
using BoutiqueDTO.Models.Implementations.Clothes;
using Functional.Models.Interfaces.Result;

namespace BoutiqueDTO.Infrastructure.Implementations.Services.Clothes
{
    public class GenderApiService : ApiServiceBase<GenderType, GenderTransfer, IGenderApi>
    {
        public GenderApiService(IGenderApi genderApi)
        {
            ApiBase = genderApi;
        }

        /// <summary>
        /// Интерфейс для Api методов
        /// </summary>
        protected override IGenderApi ApiBase { get; }

        /// <summary>
        /// Получение данных
        /// </summary>
        protected override async Task<IReadOnlyCollection<GenderTransfer>> GetApi() =>
            await ApiBase.Get();

        /// <summary>
        /// Получение данных по идентификатору
        /// </summary>
        protected override async Task<GenderTransfer> GetApi(GenderType id) =>
            await ApiBase.Get(id);

        /// <summary>
        /// Добавление данных
        /// </summary>
        protected override async Task<GenderType> PostApi(GenderTransfer transfer) =>
            await ApiBase.Post(transfer);

        /// <summary>
        /// Добавление коллекции данных
        /// </summary>
        protected override async Task<IReadOnlyCollection<GenderType>> PostApi(IReadOnlyCollection<GenderTransfer> transfers) =>
           await ApiBase.PostCollection(transfers);

        /// <summary>
        /// Добавление данных
        /// </summary>
        protected override async Task PutApi(GenderTransfer transfer) =>
            await ApiBase.Put(transfer);

        /// <summary>
        /// Удаление данных
        /// </summary>
        protected override async Task<GenderTransfer> DeleteApi(GenderType id) =>
            await ApiBase.Delete(id);
    }
}