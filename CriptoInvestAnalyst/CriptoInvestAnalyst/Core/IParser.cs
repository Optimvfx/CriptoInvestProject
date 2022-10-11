using AngleSharp.Html.Dom;

namespace CryptoInvestAnalyst
{
    public interface IParser<T>
        where T : class
    {
        T Parse(IHtmlDocument document);
    }
}
