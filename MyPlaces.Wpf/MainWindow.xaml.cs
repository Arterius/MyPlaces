using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows;
using MyPlaces.Service.Client.Contracts.Service.General;
using MyPlaces.Service.Client.Service;
using MyPlaces.Service.Client.Repository;

namespace MyPlaces.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //private async void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    //GooglePlacesRepository repository = new GooglePlacesRepository(new HttpService());
        //    //GooglePlacesDataService service = new GooglePlacesDataService(repository, "AIzaSyDI3Q6N_PIKL3yW_HR2OApUhFFR-BbIzxs");
        //    //var result = await service.Search("restaurants+in+Yerevan");

        //    //FoursquareVenuesRepository repository = new FoursquareVenuesRepository(new HttpService());
        //    //FoursquareVenuesDataService service = new FoursquareVenuesDataService(repository, "UZH4KD340XSEKTU1WPUJT0EKNSF1QCOH00EUTQGOASKWIRUB", "ZL1NJN13P53C4OSHHMTPSHZSCDPBDOWWBMAISERPRUGAVVP4");
        //    //var result = await service.Search("shop");
        //}
    }
}
