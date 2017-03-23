using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using MyPlaces.ViewModel.Common;

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
                PlacesDataServiceProviders.Instance.Default = value;
            }
        }
    }
}
