using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace CryptoInfoGiverSpace
{
    public class HtmlLoader
    {
        private readonly HttpClient _client;
        private readonly string _url;
        private readonly HtmlLoaderExtraUrlArguments _extraUrlArguments;

        public HtmlLoader(IParserSettings settings, HtmlLoaderExtraUrlArguments extraUrlArguments, HttpClient client = null)
        {
            _client = client ?? new HttpClient();
            _url = GeterateUrl(settings);
            _extraUrlArguments = extraUrlArguments;
        }

        public async Task<string> GetSourceByPageId(int id)
        {
            var currentUrl = _url + _extraUrlArguments.GetUrlExtraArguments(id);

            var response = await _client.GetAsync(currentUrl);

            string source = null;

            if(ResponseInValid(response))
            {
                source = await response.Content.ReadAsStringAsync();
            }

            return source;
        }

        private string GeterateUrl(IParserSettings settings)
        {
            return $"{settings.BaseUrl}/{settings.Prefix}";
        }

        private bool ResponseInValid(HttpResponseMessage response)
        {
            return response != null && response.StatusCode == HttpStatusCode.OK;
        }
    }
}
