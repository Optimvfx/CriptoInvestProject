using Newtonsoft.Json;

namespace CryptoInfoGiverApi.JSON.Models
{
    public class SymbolFullData
    {
        [JsonProperty("TYPE")]
        public string TYPE { get; private set; }


        [JsonProperty("MARKET")]
        public string MARKET { get; private set; }


        [JsonProperty("FROMSYMBOL")]
        public string FROMSYMBOL { get; private set; }


        [JsonProperty("TOSYMBOL")]
        public string TOSYMBOL { get; private set; }


        [JsonProperty("FLAGS")]
        public string FLAGS { get; private set; }


        [JsonProperty("PRICE")]
        public float PRICE { get; private set; }


        [JsonProperty("LASTUPDATE")]
        public int LASTUPDATE { get; private set; }


        [JsonProperty("MEDIAN")]
        public float MEDIAN { get; private set; }


        [JsonProperty("LASTVOLUME")]
        public float LASTVOLUME { get; private set; }


        [JsonProperty("LASTVOLUMETO")]
        public float LASTVOLUMETO { get; private set; }


        [JsonProperty("LASTTRADEID")]
        public string LASTTRADEID { get; private set; }


        [JsonProperty("VOLUMEDAY")]
        public float VOLUMEDAY { get; private set; }


        [JsonProperty("VOLUMEDAYTO")]
        public float VOLUMEDAYTO { get; private set; }


        [JsonProperty("VOLUME24HOUR")]
        public float VOLUME24HOUR { get; private set; }


        [JsonProperty("VOLUME24HOURTO")]
        public float VOLUME24HOURTO { get; private set; }


        [JsonProperty("OPENDAY")]
        public float OPENDAY { get; private set; }


        [JsonProperty("HIGHDAY")]
        public float HIGHDAY { get; private set; }


        [JsonProperty("LOWDAY")]
        public float LOWDAY { get; private set; }


        [JsonProperty("OPEN24HOUR")]
        public float OPEN24HOUR { get; private set; }


        [JsonProperty("HIGH24HOUR")]
        public float HIGH24HOUR { get; private set; }


        [JsonProperty("LOW24HOUR")]
        public float LOW24HOUR { get; private set; }


        [JsonProperty("LASTMARKET")]
        public string LASTMARKET { get; private set; }


        [JsonProperty("VOLUMEHOUR")]
        public float VOLUMEHOUR { get; private set; }


        [JsonProperty("VOLUMEHOURTO")]
        public float VOLUMEHOURTO { get; private set; }


        [JsonProperty("OPENHOUR")]
        public float OPENHOUR { get; private set; }


        [JsonProperty("HIGHHOUR")]
        public float HIGHHOUR { get; private set; }


        [JsonProperty("LOWHOUR")]
        public float LOWHOUR { get; private set; }


        [JsonProperty("TOPTIERVOLUME24HOUR")]
        public float TOPTIERVOLUME24HOUR { get; private set; }


        [JsonProperty("TOPTIERVOLUME24HOURTO")]
        public float TOPTIERVOLUME24HOURTO { get; private set; }


        [JsonProperty("CHANGE24HOUR")]
        public float CHANGE24HOUR { get; private set; }


        [JsonProperty("CHANGEPCT24HOUR")]
        public float CHANGEPCT24HOUR { get; private set; }


        [JsonProperty("CHANGEDAY")]
        public float CHANGEDAY { get; private set; }


        [JsonProperty("CHANGEPCTDAY")]
        public float CHANGEPCTDAY { get; private set; }


        [JsonProperty("CHANGEHOUR")]
        public float CHANGEHOUR { get; private set; }


        [JsonProperty("CHANGEPCTHOUR")]
        public float CHANGEPCTHOUR { get; private set; }


        [JsonProperty("CONVERSIONTYPE")]
        public string CONVERSIONTYPE { get; private set; }


        [JsonProperty("CONVERSIONSYMBOL")]
        public string CONVERSIONSYMBOL { get; private set; }


        [JsonProperty("SUPPLY")]
        public float SUPPLY { get; private set; }


        [JsonProperty("MKTCAP")]
        public float MKTCAP { get; private set; }


        [JsonProperty("MKTCAPPENALTY")]
        public int MKTCAPPENALTY { get; private set; }


        [JsonProperty("CIRCULATINGSUPPLY")]
        public float CIRCULATINGSUPPLY { get; private set; }


        [JsonProperty("CIRCULATINGSUPPLYMKTCAP")]
        public float CIRCULATINGSUPPLYMKTCAP { get; private set; }


        [JsonProperty("TOTALVOLUME24H")]
        public float TOTALVOLUME24H { get; private set; }


        [JsonProperty("TOTALVOLUME24HTO")]
        public float TOTALVOLUME24HTO { get; private set; }


        [JsonProperty("TOTALTOPTIERVOLUME24H")]
        public float TOTALTOPTIERVOLUME24H { get; private set; }


        [JsonProperty("TOTALTOPTIERVOLUME24HTO")]
        public float TOTALTOPTIERVOLUME24HTO { get; private set; }


        [JsonProperty("IMAGEURL")]
        public string IMAGEURL { get; private set; }
    }

}

