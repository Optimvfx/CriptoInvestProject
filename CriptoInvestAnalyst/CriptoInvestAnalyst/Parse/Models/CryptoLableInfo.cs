namespace CryptoInvestAnalyst
{
    public struct CryptoLableInfo
    {
        [Newtonsoft.Json.JsonProperty("id")]
        public int Id { get; private set; }
        [Newtonsoft.Json.JsonProperty("symbol")]
        public string Symbol { get; private set; }
        [Newtonsoft.Json.JsonProperty("partner_symbol")]
        public string PartnerSymbol { get; private set; }
        [Newtonsoft.Json.JsonProperty("data_available_from")]
        public int DataAvaibleFrom { get; private set; }

        public override string ToString()
        {
            return $"ID {Id}, Symbol {Symbol}, Partner Symbol {PartnerSymbol}, Data Avaible From {DataAvaibleFrom}";
        }
    }

}
