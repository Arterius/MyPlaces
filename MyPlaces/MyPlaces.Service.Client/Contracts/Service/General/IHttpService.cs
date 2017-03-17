using System;
using System.Threading.Tasks;

namespace MyPlaces.Service.Client.Contracts.Service.General
{
    public interface IHttpService
    {
        Task<string> GetStringAsync(Uri requestUri);
    }
}
