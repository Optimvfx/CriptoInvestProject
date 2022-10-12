namespace CryptoInvestAnalyst
{
    public struct CryptoLableInfo
    {
        [Newtonsoft.Json.JsonProperty("id")]
        public int Id { get; set; }
        [Newtonsoft.Json.JsonProperty("symbol")]
        public string Symbol { get; set; }
        [Newtonsoft.Json.JsonProperty("partner_symbol")]
        public string PartnerSymbol { get; set; }
        [Newtonsoft.Json.JsonProperty("data_available_from")]
        public int DataAvaibleFrom { get; set; }

        public override string ToString()
        {
            return $"ID {Id}, Symbol {Symbol}, Partner Symbol {PartnerSymbol}, Data Avaible From {DataAvaibleFrom}";
        }
    }

}
