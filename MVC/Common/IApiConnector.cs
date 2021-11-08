using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MVC.Common
{
    public interface IApiConnector
    {
        Task<HttpResponseMessage> SendGetAsync(string request, bool useAuthorization = true);
        Task<HttpResponseMessage> SendGetAsync(string request, 
            Dictionary<string,string> parameters, bool useAuthorization = true);
        Task<HttpResponseMessage> SendPostAsync<T>(string request, T content, bool useAuthorization = true);
        Task<HttpResponseMessage> SendPutAsync<T>(string request, T content, bool useAuthorization = true);
        Task<HttpResponseMessage> SendDeleteAsync(string request, bool useAuthorization = true);
    }
}