using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace MVC.Common
{
    public class DefaultConnector: IApiConnector
    {
        private readonly HttpClient _client;

        public DefaultConnector(HttpClient client)
        {
            _client = client;
        }

        public async Task<HttpResponseMessage> SendGetAsync(
            string request, bool useAuthorization = true)
        {
            // ConfigureAuthorization(useAuthorization);
            return await _client.GetAsync(request);
        }

        public async Task<HttpResponseMessage> SendGetAsync(
            string request, Dictionary<string, string> parameters, bool useAuthorization = true)
        {
            if (parameters != null)
            {
                var content = new FormUrlEncodedContent(parameters);
                var urlString = await content.ReadAsStringAsync();
                request = request + "?" + urlString;
            }
            // ConfigureAuthorization(useAuthorization);
             return await _client.GetAsync(request);
        }


        public async Task<HttpResponseMessage> SendPostAsync<T>(
            string request, T content, bool useAuthorization = true)
        {
           // ConfigureAuthorization(useAuthorization);
            return await _client.PostAsJsonAsync(request, content); ;
        }


        public async Task<HttpResponseMessage> SendPutAsync<T>(
            string request, T content, bool useAuthorization = true)
        {
            //ConfigureAuthorization(useAuthorization);
            return await _client.PutAsJsonAsync(request, content);
        }


        public async Task<HttpResponseMessage> SendDeleteAsync(
            string request, bool useAuthorization = true)
        {
            //ConfigureAuthorization(useAuthorization);
            return await _client.DeleteAsync(request);
        }


        //private void ConfigureAuthorization(bool useAuth)
        //{
        //    _client.DefaultRequestHeaders.Authorization =
        //        useAuth ? new AuthenticationHeaderValue("Bearer", App.Token) : null;
        //}
    }
}