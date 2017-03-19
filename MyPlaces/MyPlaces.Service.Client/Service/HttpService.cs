using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MyPlaces.Service.Client.Contracts.Service.General;

namespace MyPlaces.Service.Client.Service
{
    public class HttpService : IHttpService
    {
        public async Task<string> GetStringAsync(Uri requestUri)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string response = await httpClient.GetStringAsync(requestUri);

                return response;
                //var response = httpClient.GetAsync(requestUri);
                //var parsed = await response.Result.Content.ReadAsStringAsync();
                //return parsed;
            }
        }
    }
}
