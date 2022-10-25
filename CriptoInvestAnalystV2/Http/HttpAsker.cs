using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Http
{
    public class HttpAsker
    {
        private readonly HttpClient _httpClient;

        public HttpAsker(HttpClient httpClient)
        {
            if (httpClient == null)
                throw new System.NullReferenceException();

            _httpClient = httpClient;
        }

        public HttpAsker()
        {
            _httpClient = new HttpClient();
        }

        public async void PushAsk(string url, Dictionary<string, string> query = null)
        {
            if (query == null)
                query = new Dictionary<string, string>();

            url = QueryHelpers.AddQueryString(url, query);

            var result = await _httpClient.GetAsync(url);

            if (result.IsSuccessStatusCode == false)
                throw new HttpRequestException(result.StatusCode.ToString());
        }

        public async Task<T> GetAsk<T>(string url, Dictionary<string, string> query = null)
        {
            if (query == null)
                query = new Dictionary<string, string>();

            url = QueryHelpers.AddQueryString(url, query);

            var result = await _httpClient.GetAsync(url);

            if (result != null && result.IsSuccessStatusCode)
            {
                try
                {
                    var jsonString = result.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<T>(jsonString.Result);
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }

            throw new HttpRequestException(result.StatusCode.ToString());
        }
    }
}