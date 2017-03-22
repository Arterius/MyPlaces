using MyPlaces.ViewModel;
using MyPlaces.ViewModel.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace MyPlaces
{
    public partial class App : Application
    {
        private static readonly ViewModelLocator _locator = new ViewModelLocator();
        public static ViewModelLocator Locator
        {
            get { return _locator; }
        }

        public App()
        {
            Locator.NavigationService.Configure(ViewModelLocator.MainPage, typeof(MainPage));
            Locator.NavigationService.Configure(ViewModelLocator.SettingsPage, typeof(SettingsPage));

            NavigationPage firstPage = new NavigationPage(new MainPage());

            Locator.NavigationService.Initialize(firstPage);

            InitializeComponent();
            MainPage = firstPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
