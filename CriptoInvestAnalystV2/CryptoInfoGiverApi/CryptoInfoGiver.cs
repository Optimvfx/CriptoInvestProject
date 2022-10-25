using CryptoInfoGiverApi.BaseClass;
using CryptoInfoGiverApi.Interface;
using CryptoInfoGiverApi.JSON.Models;
using Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CryptoInfoGiverApi
{
    public class CryptoInfoGiver : ICryptoInfoGiver
    {
        private const string _url = "https://min-api.cryptocompare.com";

        private const string _apiKey = "505986c3d636b5f282102aab2d210bb525e95814b57d50bce32a5a05de7c48bf";

        public async Task<Dictionary<string, Dictionary<string, float>>> GetDayPairOHLCVbyTS(Currency comparerer, IEnumerable<Currency> comparables, DateTime ts)
        {
            return await GetDayPairOHLCVbyTS(comparerer, comparables, ((DateTimeOffset)ts).ToUnixTimeSeconds());
        }

        public async Task<Dictionary<string, Dictionary<string, float>>> GetDayPairOHLCVbyTS(Currency comparerer, IEnumerable<Currency> comparables, long ts)
        {
            const string extraUrl = "data/pricehistorical";

            var httpClient = GenerateHttpClient();

            var query = new Dictionary<string, string>()
            {
                ["fsym"] = CurrencyToApiName(comparerer),
                ["tsyms"] = CurrencysToApiNames(comparables),
                ["ts"] = ts.ToString()
            };

            return await new HttpAsker(httpClient).GetAsk<Dictionary<string, Dictionary<string, float>>>(GetUrl(extraUrl), query);
        }

        public async Task<DataCurrencysInfosAskState> GetDailyPairOHLCV(Currency comparerer, Currency comparable, uint limit)
        {
            const string extraUrl = "data/v2/histoday";

            return await GetTimePairOHLCV(extraUrl, comparerer, comparable, limit);
        }
        
        public async Task<DataCurrencysInfosAskState> GetHourlyPairOHLCV(Currency comparerer, Currency comparable, uint limit)
        {
            const string extraUrl = "data/v2/histohour";

            return await GetTimePairOHLCV(extraUrl, comparerer, comparable, limit);
        }

        public async Task<DataCurrencysInfosAskState> GetMinutePairOHLCV(Currency comparerer, Currency comparable, uint limit)
        {
            const string extraUrl = "data/v2/histominute";

            return await GetTimePairOHLCV(extraUrl, comparerer, comparable, limit);
        }


        public async Task<MultipleSymbolsFullData> GetMultipleSymbolsFullData(IEnumerable<Currency> comparerers, IEnumerable<Currency> comparables)
        {
            const string extraUrl = "data/pricemultifull";

            var httpClient = GenerateHttpClient();

            var query = new Dictionary<string, string>()
            {
                ["fsyms"] = CurrencysToApiNames(comparerers),
                ["tsyms"] = CurrencysToApiNames(comparables)
            };

            return await new HttpAsker(httpClient).GetAsk<MultipleSymbolsFullData>(GetUrl(extraUrl), query);
        }

        public async Task<Dictionary<string, Dictionary<string, float>>> GetMultySymbolPrice(IEnumerable<Currency> comparerers, IEnumerable<Currency> comparables)
        {
            const string extraUrl = "data/price";

            var httpClient = GenerateHttpClient();

            var query = new Dictionary<string, string>()
            {
                ["fsyms"] = CurrencysToApiNames(comparerers),
                ["tsyms"] = CurrencysToApiNames(comparables)
            };

            return await  new HttpAsker(httpClient).GetAsk<Dictionary<string, Dictionary<string, float>>>(GetUrl(extraUrl), query);
        }

        public async Task<Dictionary<string, float>> GetSyngleSymbolPrice(Currency comparerer, IEnumerable<Currency> comparables)
        {
            const string extraUrl = "data/price";

            var httpClient = GenerateHttpClient();

            var query = new Dictionary<string, string>()
            {
                ["fsym"] = CurrencyToApiName(comparerer),
                ["tsyms"] = CurrencysToApiNames(comparables)
            };
        
            return await new HttpAsker(httpClient).GetAsk<Dictionary<string, float>>(GetUrl(extraUrl), query);
        }

        public async Task<CustomAverage> GenerateCustomAverage(Currency comparerer, Currency comparable)
        {
            const string extraUrl = "/data/generateAvg";

            var httpClient = GenerateHttpClient();

            var query = new Dictionary<string, string>()
            {
                ["fsym"] = CurrencyToApiName(comparerer),
                ["tsym"] = CurrencyToApiName(comparable)
            };

            return await new HttpAsker(httpClient).GetAsk<CustomAverage>(GetUrl(extraUrl), query);
        }

        private async Task<DataCurrencysInfosAskState> GetTimePairOHLCV(string timeExtraUrl, Currency comparerer, Currency comparable, uint limit)
        {
            var httpClient = GenerateHttpClient();

            var query = new Dictionary<string, string>()
            {
                ["fsym"] = CurrencyToApiName(comparerer),
                ["tsym"] = CurrencyToApiName(comparable),
                ["limit"] = limit.ToString()
            };

            return await new HttpAsker(httpClient).GetAsk<DataCurrencysInfosAskState>(GetUrl(timeExtraUrl), query);
        }

        private HttpClient GenerateHttpClient()
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Apikey", $"{_apiKey}");

            return httpClient;
        }

        private string GetUrl(string extraUrl)
        {
            return _url + "/" + extraUrl;
        }

        private string CurrencyToApiName(Currency currency)
        {
            return currency.ToString();
        }

        private string CurrencysToApiNames(IEnumerable<Currency> currencys)
        {
            const string splitChar = ",";

            var sb = new StringBuilder();

            foreach(var currency in currencys)
            {
                sb.Append(CurrencyToApiName(currency));
                sb.Append(splitChar);
            }

            sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }

    }
}
