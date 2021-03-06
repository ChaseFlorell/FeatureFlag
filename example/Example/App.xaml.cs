﻿using System;
using Example.Features.Main;
using FutureFlag;
using NodaTime;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Example
{
    public partial class App
    {
        public App()
        {
            InitializeComponent();
            
            FutureFlagConfiguration.Configure(config =>
            {
                // here you can override how your application will acquire "Now"
                // using NodaTime to acquire "Now"
                config.NowProvider(GetNodaNow);
                
                // here you can override how your application will acquire "UtcNow"
                // using NodaTime to acquire "UtcNow"
                config.UtcNowProvider(GetNodaUtcNow);

                // here you can override whether your application will
                // look at "Now" or "UtcNow" by default
                config.UseUtcByDefault(true);

#if DEBUG
                // here you can override an exact version to override all version checks
                // this is particularly useful for development. Just set this to your predefined dev version. 
                config.SetIsEnabledForExactVersion("0.0.0.1");
#endif
            });
            
            MainPage = new MainPage();
        }

        private DateTime GetNodaUtcNow() 
            => SystemClock.Instance.GetCurrentInstant().ToDateTimeUtc();

        private DateTime GetNodaNow() 
            => SystemClock.Instance.GetCurrentInstant().ToDateTimeUtc().ToLocalTime();
    }
}