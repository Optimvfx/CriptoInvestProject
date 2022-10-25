using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace CryptoInfoGiverApi.JSON.Models
{
    public class DataCurrencyInfoRange
    {
        [JsonProperty("Aggregated")]
        public bool Aggregated { get; private set; }


        [JsonProperty("TimeFrom")]
        public int TimeFrom { get; private set; }


        [JsonProperty("TimeTo")]
        public int TimeTo { get; private set; }


        [JsonProperty("Data")]
        private List<DataCurrencyInfo> _dataCurrencyInfos;

        public IEnumerable<DataCurrencyInfo> DataCurrencyInfos => _dataCurrencyInfos;
    }
}
