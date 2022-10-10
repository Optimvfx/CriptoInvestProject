using AngleSharp.Html.Dom;

namespace CriptoInvestAnalyst
{
    public interface IParser<T>
        where T : class
    {
        T Parse(IHtmlDocument document);
    }
}
