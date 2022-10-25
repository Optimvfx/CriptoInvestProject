using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace CryptoInfoGiverApi.JSON.Models
{
    public class MultipleSymbolsFullData
    {
        [JsonProperty("RAW")]
        private Dictionary<string, Dictionary<string, SymbolFullData>> _raw;

        public Dictionary<string, Dictionary<string, SymbolFullData>> Raw => _raw;
    }
}
