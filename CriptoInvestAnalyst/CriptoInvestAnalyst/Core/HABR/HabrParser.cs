using AngleSharp.Html.Dom;
using System;
using System.Linq;

namespace CryptoInvestAnalyst
{
    public class HabrParser : IParser<string[]>
    {
        private static string _tag => "a";
        private static string _searchingClassName => "tm-article-snippet__title-link";

        public string[] Parse(IHtmlDocument document)
        {
            var items = document.QuerySelectorAll(_tag)
                                .Where(item => item.ClassName != null)
                                .Where(item => item.ClassName.Contains(_searchingClassName));

            return items.Select(item => item.TextContent).ToArray();
        }
    }
}
