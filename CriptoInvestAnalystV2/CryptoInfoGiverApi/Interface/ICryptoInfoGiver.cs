using CryptoInfoGiverApi.BaseClass;
using System.Collections.Generic;
using CryptoInfoGiverApi.JSON.Models;
using System.Threading.Tasks;

namespace CryptoInfoGiverApi.Interface
{
    public interface ICryptoInfoGiver
    {
        Task<Dictionary<string, float>> GetSyngleSymbolPrice(Currency comparerer, IEnumerable<Currency> comparables);

        Task<Dictionary<string, Dictionary<string, float>>> GetMultySymbolPrice(IEnumerable<Currency> comparerers, IEnumerable<Currency> comparables);

        Task<MultipleSymbolsFullData> GetMultipleSymbolsFullData(IEnumerable<Currency> comparerers, IEnumerable<Currency> comparables);

        Task<CustomAverage> GenerateCustomAverage(Currency comparerer, Currency comparable);


        Task<DataCurrencysInfosAskState> GetDailyPairOHLCV(Currency comparerer, Currency comparable, uint limit);

        Task<DataCurrencysInfosAskState> GetHourlyPairOHLCV(Currency comparerer, Currency comparable, uint limit);

        Task<DataCurrencysInfosAskState> GetMinutePairOHLCV(Currency comparerer, Currency comparable, uint limit);

        Task<Dictionary<string, Dictionary<string, float>>> GetDayPairOHLCVbyTS(Currency comparerer, IEnumerable<Currency> comparable, long ts);
    }
}
