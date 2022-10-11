namespace CryptoInvestAnalyst
{
    public interface IParserSettings
    {
        string BaseUrl {get;}

        string Prefix { get;}

        int StartPoint { get;}

        int EndPoint { get;}
    }
}
