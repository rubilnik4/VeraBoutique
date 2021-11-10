using Xamarin.Android.Net;

namespace BoutiqueXamarin.Droid.Infrastructure.Implementation.Configuration
{
    public static class AndroidClientHandlerFactory
    {
        public static AndroidClientHandler GetAndroidHttpHandler() =>
            new AndroidClientHandler();
    }
}