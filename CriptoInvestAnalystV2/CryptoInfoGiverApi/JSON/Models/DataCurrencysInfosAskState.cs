using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace CryptoInfoGiverApi.JSON.Models
{
    public class DataCurrencysInfosAskState
    {
        [JsonProperty("Response")]
        public string Response { get; private set; }


        [JsonProperty("Message")]
        public string Message { get; private set; }


        [JsonProperty("HasWarning")]
        public bool HasWarning { get; private set; }


        [JsonProperty("Type")]
        public int Type { get; private set;  }


        [JsonProperty("Data")]
        public DataCurrencyInfoRange DataCurrencyInfoRange { get; private set; }
    }
}
