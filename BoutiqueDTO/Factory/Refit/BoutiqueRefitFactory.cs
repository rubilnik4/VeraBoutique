using BoutiqueDTO.Infrastructure.Interfaces.Refit.Base;
using BoutiqueDTO.Infrastructure.Interfaces.Refit.Clothes;
using Refit;

namespace BoutiqueDTO.Factory.Refit
{
    public static class BoutiqueRefitFactory
    {
        public static void GetBoutiqueRefit<TRefitApi>()
            where TRefitApi: IRefitApiBase =>
            RestService.For<TRefitApi>("https://api.github.com");
    }
}