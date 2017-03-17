using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPlaces.Model;

namespace MyPlaces.Service.Client.Contracts.Service.Data
{
    public interface IPlacesDataService
    {
        Task<List<Place>> Search(string keyWord);
    }
}
