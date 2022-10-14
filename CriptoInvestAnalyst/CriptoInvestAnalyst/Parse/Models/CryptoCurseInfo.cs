namespace CryptoInfoGiverSpace
{
    public class CryptoCurseInfo
    {
        [Newtonsoft.Json.JsonProperty("time")]
        public uint UnitTime { get; private set; }

        [Newtonsoft.Json.JsonProperty("high")]
        public float MaximalPrice { get; private set; }

        [Newtonsoft.Json.JsonProperty("low")]
        public float MinimalPrice { get; private set; }

        [Newtonsoft.Json.JsonProperty("open")]
        public float OpenPrice { get; private set; }

        [Newtonsoft.Json.JsonProperty("volumefrom")]
        public float VolumeFrom { get; private set; }

        [Newtonsoft.Json.JsonProperty("volumeto")]
        public float VplumeTo { get; private set; }

        [Newtonsoft.Json.JsonProperty("close")]
        public float ClosePrice { get; private set; }

        [Newtonsoft.Json.JsonProperty("conversionType")]
        public string ConversionType { get; private set; }

        [Newtonsoft.Json.JsonProperty("conversionSymbol")]
        public string ConversionSymbol { get; private set; }

        public override string ToString()
        { 
            return $"Time = {UnitTime}, Max {MaximalPrice}, Min {MinimalPrice}, Open {OpenPrice}, VolFrom {VolumeFrom}, VolTo {VplumeTo}, Close {ClosePrice}, ConvertType {ConversionType}, ConvertChar {ConversionSymbol}.";
        }
    }
}
