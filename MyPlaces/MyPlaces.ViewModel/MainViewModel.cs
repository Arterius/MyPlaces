using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using MyPlaces.Service.Client.Contracts.Service.Data;
using MyPlaces.ViewModel.Common;
using MyPlaces.Model;
using GalaSoft.MvvmLight.Command;
using System.Threading;

namespace MyPlaces.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private const int _searchDelay = 500;
        private readonly IPlacesDataService _placesDataService;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        private string _searchTerm;
        public string SearchTerm
        {
            get { return _searchTerm; }
            set
            {
                if (_searchTerm == value) return;
                _searchTerm = value;

                if (!string.IsNullOrWhiteSpace(_searchTerm))
                    ExecuteDelayedSearch();
            }
        }

        public RangeObservableCollection<Place> Places { get; set; }

        public RelayCommand SearchCommand { get; private set; }

        public MainViewModel(IPlacesDataService placesDataService)
        {
            _placesDataService = placesDataService ?? throw new ArgumentNullException(nameof(placesDataService));
            Places = new RangeObservableCollection<Place>();
            SearchCommand = new RelayCommand(Search, () => !string.IsNullOrWhiteSpace(SearchTerm));
        }

        private async void ExecuteDelayedSearch()
        {
            string originalSearchTerm = SearchTerm;

            Interlocked.Exchange(ref _cancellationTokenSource, new CancellationTokenSource()).Cancel();

            try
            {
                await Task.Delay(_searchDelay, _cancellationTokenSource.Token).ContinueWith(async (_) =>
                {
                    if (originalSearchTerm == SearchTerm)
                    {
                        Places.AddRange(await _placesDataService.Search(SearchTerm), true);
                    }
                }, CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch { }
        }

        private async void Search()
        {
            Places.AddRange(await _placesDataService.Search(SearchTerm), true);
        }
    }
}
