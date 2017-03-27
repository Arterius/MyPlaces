using System.Collections.Generic;
using GalaSoft.MvvmLight;
using MyPlaces.ViewModel.Common;
using GalaSoft.MvvmLight.Messaging;

namespace MyPlaces.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        public List<PlaceDataProvider> Providers
        {
            get
            {
                return PlacesDataServiceProviders.Instance.Providers;
            }
        }

        public PlaceDataProvider Default
        {
            get
            {
                return PlacesDataServiceProviders.Instance.Default;
            }
            set
            {
                if (PlacesDataServiceProviders.Instance.Default != value)
                {
                    PlacesDataServiceProviders.Instance.Default = value;
                    try
                    {
                        Messenger.Default.Send(value);
                    }
                    catch { }
                }
            }
        }
    }
}
