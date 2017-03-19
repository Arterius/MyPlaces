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

namespace MyPlaces.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IPlacesDataService _placesDataService;

        private string _searchTerm;
        public string SearchTerm
        {
            get { return _searchTerm; }
            set
            {
                if (_searchTerm == value) return;
                _searchTerm = value;
                SearchCommand.RaiseCanExecuteChanged();
            }
        }
        //public string SearchTerm { get; set; }

        public RangeObservableCollection<Place> Places { get; set; }

        public RelayCommand SearchCommand { get; private set; }

        public MainViewModel(IPlacesDataService placesDataService)
        {
            _placesDataService = placesDataService ?? throw new ArgumentNullException(nameof(placesDataService));
            Places = new RangeObservableCollection<Place>();
            SearchCommand = new RelayCommand(Search, () => !string.IsNullOrWhiteSpace(SearchTerm));
        }

        private async void Search()
        {
            Places.AddRange(await _placesDataService.Search(SearchTerm), true);
        }
    }
}
