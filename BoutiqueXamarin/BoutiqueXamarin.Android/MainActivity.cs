﻿using System;
using System.IO;
using System.Net.Http;
using Android.App;
using Android.Content.PM;
using Android.OS;
using BoutiqueCommon.Models.Domain.Implementations.Configuration;
using BoutiqueDTO.Infrastructure.Implementations.Converters.Configuration;
using BoutiqueDTO.Infrastructure.Interfaces.Converters.Configuration;
using BoutiqueXamarin.Droid.Infrastructure.Implementation.Configuration;
using BoutiqueXamarinCommon.Infrastructure.Interfaces.Configuration;
using FFImageLoading.Forms.Platform;
using FFImageLoading.Svg.Forms;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Sharpnado.HorizontalListView.Droid;
using Unity;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace BoutiqueXamarin.Droid
{
    [Activity(Theme = "@style/MainTheme",
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            CachedImageRenderer.Init(true);
            CachedImageRenderer.InitImageViewHandler();
            SharpnadoInitializer.Initialize();
            Sharpnado.Tabs.Initializer.Initialize(false, false);
            Sharpnado.Shades.Initializer.Initialize(false);
            Akavache.Registrations.Start(AppInfo.Name);
            XF.Material.Droid.Material.Init(this, savedInstanceState);
            Forms.Init(this, savedInstanceState);
            LoadApplication(new App(new AndroidInitializer()));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    /// <summary>
    /// Инициализация
    /// </summary>
    public class AndroidInitializer : IPlatformInitializer
    {
        /// <summary>
        /// Регистрация зависимостей
        /// </summary>
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IXamarinConfigurationManager, AndroidConfigurationManager>();
            containerRegistry.Register<HttpClientHandler>(container => AndroidClientHandlerFactory.GetAndroidHttpHandler());
        }
    }
}

