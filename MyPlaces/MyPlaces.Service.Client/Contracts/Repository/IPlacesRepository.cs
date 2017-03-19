using System;
using System.Threading.Tasks;

namespace MyPlaces.Service.Client.Contracts.Repository
{
    public interface IPlacesRepository<T>
    {
        Task<T> GetPlaces(Uri requestUri);
    }
}
