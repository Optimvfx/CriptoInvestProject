using AngleSharp.Html.Dom;

namespace CryptoInfoGiverSpace
{
    public interface IParser<T>
        where T : class
    {
        T Parse(IHtmlDocument document);
    }
}
