using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MyPlaces.Service.Client.Contracts.Service.General;

namespace MyPlaces.Service.Client.Repository
{
    public abstract class BaseRepository
    {
        private IHttpService _httpService;

        public BaseRepository(IHttpService httpService)
        {
            _httpService = httpService;
        }

        protected async Task<T> GetAsync<T>(Uri requestUri) where T : new()
        {
            if (requestUri == null)
                throw new ArgumentNullException(nameof(requestUri));

            T result;
            try
            {
                string response = await _httpService.GetStringAsync(requestUri);
                result = await Task.Run(() => JsonConvert.DeserializeObject<T>(response));

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
