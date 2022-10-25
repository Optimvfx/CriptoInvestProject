using Newtonsoft.Json;

namespace CryptoInfoGiverApi.JSON.Models
{
    public class DataCurrencyInfo
    {
        [JsonProperty("time")]
        public int Time { get; private set; }


        [JsonProperty("high")]
        public float High { get; private set; }


        [JsonProperty("low")]
        public float Low { get; private set; }


        [JsonProperty("open")]
        public float Open { get; private set; }


        [JsonProperty("volumefrom")]
        public float Volumefrom { get; private set; }


        [JsonProperty("volumeto")]
        public float Volumeto { get; private set; }


        [JsonProperty("close")]
        public float Close { get; private set; }


        [JsonProperty("conversionType")]
        public string ConversionType { get; private set; }


        [JsonProperty("conversionSymbol")]
        public string ConversionSymbol { get; private set; }
    }
}
