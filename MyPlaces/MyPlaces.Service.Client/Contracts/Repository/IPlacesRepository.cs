using System.Collections.Generic;
using System.Threading.Tasks;
using MyPlaces.Model;

namespace MyPlaces.Service.Client.Contracts.Repository
{
    public interface IPlacesRepository
    {
        Task<List<Place>> GetPlaces();
    }
}
