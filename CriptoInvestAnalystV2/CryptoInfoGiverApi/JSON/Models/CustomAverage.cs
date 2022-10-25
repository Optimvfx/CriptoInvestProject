using Newtonsoft.Json;

namespace CryptoInfoGiverApi.JSON.Models
{
    public class CustomAverage
    {
        [JsonProperty("RAW")]
        public SymbolFullData Raw { private set; get; }
    }
}
