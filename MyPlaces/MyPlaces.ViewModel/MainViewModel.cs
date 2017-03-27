using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using MyPlaces.Model;
using MyPlaces.Service.Client.Contracts.Service.Data;
using MyPlaces.Service.Client.Exceptions;
using MyPlaces.ViewModel.Common;
using MyPlaces.ViewModel.Helpers;

namespace MyPlaces.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private const int _searchDelay = 500;
        private readonly IPlacesDataServiceFactory _placesDataServiceFactory;
        private readonly INavigationService _navigationService;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        private bool _pagingAvailable = true;
        private string _searchTerm;
        private bool _isBusy;

        public string SearchTerm
        {
            get { return _searchTerm.Trim(); }
            set
            {
                if (_searchTerm == value) return;
                _searchTerm = value;
            }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy == value) return;
                _isBusy = value;
                RaisePropertyChanged();
            }
        }

        public RangeObservableCollection<Place> Places { get; set; }

        public RelayCommand SearchCommand { get; private set; }
        public RelayCommand LoadMoreCommand { get; private set; }
        public RelayCommand NavigateToSettingsCommand { get; private set; }
        public RelayCommand ReloadDataCommand { get; private set; }

        public MainViewModel(IPlacesDataServiceFactory placesDataServiceFactory, INavigationService navigationService)
        {
            if (placesDataServiceFactory == null) throw new ArgumentException(nameof(placesDataServiceFactory));
            if (navigationService == null) throw new ArgumentException(nameof(navigationService));

            _placesDataServiceFactory = placesDataServiceFactory;
            _navigationService = navigationService;

            Places = new RangeObservableCollection<Place>();

            SearchCommand = new RelayCommand(DelayedSearch, () => !string.IsNullOrWhiteSpace(SearchTerm));
            LoadMoreCommand = new RelayCommand(LoadMore, () => !string.IsNullOrWhiteSpace(SearchTerm));
            NavigateToSettingsCommand = new RelayCommand(() => _navigationService.NavigateTo(ViewModelLocator.SettingsPage));
            ReloadDataCommand = new RelayCommand(ReloadData, () => !string.IsNullOrWhiteSpace(SearchTerm));

            Messenger.Default.Register<PlaceDataProvider>(this, (_) => ReloadDataCommand.Execute(null));
        }

        private async void DelayedSearch() => await DelayedSearchAsync();

        private async Task DelayedSearchAsync()
        {
            if (string.IsNullOrWhiteSpace(_searchTerm)) return;

            string originalSearchTerm = SearchTerm;

            Interlocked.Exchange(ref _cancellationTokenSource, new CancellationTokenSource()).Cancel();

            try
            {
                _pagingAvailable = true;
                await Task.Delay(_searchDelay, _cancellationTokenSource.Token).ContinueWith(async (_) =>
                {
                    if (originalSearchTerm == SearchTerm)
                    {
                        Places.AddRange(await RetrieveDataAsync(), clear: true);
                    }
                }, CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch(TaskCanceledException) { }
        }

        private async void LoadMore() => await LoadMoreAsync();

        private async Task LoadMoreAsync()
        {
            if (_pagingAvailable)
            {
                Places.AddRange(await RetrieveDataAsync(loadMore: true), clear: false);
            }
        }

        private async void ReloadData() => await ReloadDataAsync();

        private async Task ReloadDataAsync()
        {
            _pagingAvailable = true;
            List<Place> places = await RetrieveDataAsync();
            Places.AddRange(places, true);
        }

        private async Task<List<Place>> RetrieveDataAsync(bool loadMore = false)
        {
            IsBusy = true;

            List<Place> places = null;
            try
            {
                IPlacesDataService dataService = _placesDataServiceFactory.GetDataService(PlacesDataServiceProviders.Instance.Default.Id);
                places = loadMore ? await dataService.GetNext() : await dataService.Search(SearchTerm);
            }
            catch(DataPaginationException)
            {
                _pagingAvailable = false;
            }
            catch (BaseException)
            {
                //
            }
            finally
            {
                IsBusy = false;
            }

            return places;
        }
    }
}
