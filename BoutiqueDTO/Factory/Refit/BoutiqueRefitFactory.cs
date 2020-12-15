using BoutiqueDTO.Infrastructure.Interfaces.Refit.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Refit.Clothes;
using Functional.FunctionalExtensions.Sync;
using Refit;

namespace BoutiqueDTO.Factory.Refit
{
    public static class BoutiqueRefitFactory
    {
        public static void GetBoutiqueRefit<TRefitApi>()
            where TRefitApi: IApiBase =>
            RestService.For<TRefitApi>("https://api.github.com").Map(tt => tt);
    }
}