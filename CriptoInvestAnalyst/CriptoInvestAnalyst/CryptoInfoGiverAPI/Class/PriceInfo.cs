namespace CryptoInfoGiverSpace
{
    public struct PriceInfo
    {
        public readonly Crypto Crypto;
        public readonly double Price;

        public PriceInfo(Crypto crypto, double price)
        {
            Crypto = crypto;
            Price = price;
        }

        public override string ToString()
        {
            return $"{Crypto} - {Price}";
        }
    }
}
