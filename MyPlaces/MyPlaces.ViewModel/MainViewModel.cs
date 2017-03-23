using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using MyPlaces.Service.Client.Contracts.Service.Data;
using MyPlaces.ViewModel.Helpers;
using MyPlaces.Model;
using GalaSoft.MvvmLight.Command;
using System.Threading;
using GalaSoft.MvvmLight.Views;
using MyPlaces.ViewModel.Common;

namespace MyPlaces.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private const int _searchDelay = 500;
        private readonly IPlacesDataServiceFactory _placesDataServiceFactory;
        private readonly INavigationService _navigationService;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        private string _defaultDataProvider;

        private string _searchTerm;
        public string SearchTerm
        {
            get { return _searchTerm.Trim(); }
            set
            {
                if (_searchTerm == value) return;
                _searchTerm = value;

                //Uncomment to run from WPF project
                //DelayedSearch();
            }
        }

        public RangeObservableCollection<Place> Places { get; set; }

        public RelayCommand SearchCommand { get; private set; }
        public RelayCommand LoadMoreCommand { get; private set; }
        public RelayCommand NavigateToSettingsCommand { get; private set; }

        public MainViewModel(IPlacesDataServiceFactory placesDataServiceFactory, INavigationService navigationService)
        {
            if (placesDataServiceFactory == null) throw new ArgumentException(nameof(placesDataServiceFactory));
            if (navigationService == null) throw new ArgumentException(nameof(navigationService));

            _placesDataServiceFactory = placesDataServiceFactory;
            _navigationService = navigationService;

            _defaultDataProvider = PlacesDataServiceProviders.Foursquare;

            Places = new RangeObservableCollection<Place>();

            SearchCommand = new RelayCommand(DelayedSearch, () => !string.IsNullOrWhiteSpace(SearchTerm));
            LoadMoreCommand = new RelayCommand(LoadMore, () => !string.IsNullOrWhiteSpace(SearchTerm));
            NavigateToSettingsCommand = new RelayCommand(() => _navigationService.NavigateTo(ViewModelLocator.SettingsPage));
        }

        private async void DelayedSearch() => await DelayedSearchAsync();

        private async Task DelayedSearchAsync()
        {
            if (string.IsNullOrWhiteSpace(_searchTerm)) return;

            string originalSearchTerm = SearchTerm;

            Interlocked.Exchange(ref _cancellationTokenSource, new CancellationTokenSource()).Cancel();

            try
            {
                await Task.Delay(_searchDelay, _cancellationTokenSource.Token).ContinueWith(async (_) =>
                {
                    if (originalSearchTerm == SearchTerm)
                    {
                        IPlacesDataService dataService = _placesDataServiceFactory.GetDataService(_defaultDataProvider);
                        Places.AddRange(await dataService.Search(SearchTerm), true);
                    }
                }, CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch { }
        }

        private async void LoadMore() => await LoadMoreAsync();

        private async Task LoadMoreAsync()
        {
            IPlacesDataService dataService = _placesDataServiceFactory.GetDataService(_defaultDataProvider);
            Places.AddRange(await dataService.GetNext());
        }
    }
}
