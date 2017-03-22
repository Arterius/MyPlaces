using System;

namespace MyPlaces.Service.Client.Service.Helper
{
    public interface IUriBuilder
    {
        Uri ConstructSearch(string keyword);
        Uri ConstructGetNext(string param);
    }
}
