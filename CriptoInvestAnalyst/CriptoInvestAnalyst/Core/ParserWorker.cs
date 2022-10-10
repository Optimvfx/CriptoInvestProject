
using AngleSharp.Html.Parser;
using System;
using System.Net.Http;

namespace CriptoInvestAnalyst
{
    public class ParserWorker<T>
        where T : class
    {
        private readonly IParser<T> _parser;
        private readonly IParserSettings _parserSetings;

        private readonly HtmlLoader _loader;
        private readonly HtmlParser _htmlParser;

        public bool IsActive { get; private set; }

        public event Action<object, T> OnNewData;
        public event Action<object> OnComlited;

        public ParserWorker(IParser<T> parser, IParserSettings parserSettings, HtmlLoaderExtraUrlArguments extraUrlArguments, HtmlParser htmlParser = null)
        {
            _parser = parser;
            _parserSetings = parserSettings;

            _loader = new HtmlLoader(_parserSetings, extraUrlArguments);

            _htmlParser =  htmlParser ?? new HtmlParser();
        }

        public void Start()
        {
            if (IsActive)
                throw new ArgumentException();

            IsActive = true;
            StartWork();
        }

        public void Abort()
        {
            IsActive = false;
        }

        private async void StartWork()
        {
            for(int i = _parserSetings.StartPoint; i <= _parserSetings.EndPoint; i++)
            {
                if(IsActive == false)
                {
                    break;
                }

                var source = await _loader.GetSourceByPageId(i);

                var document = await _htmlParser.ParseDocumentAsync(source);

                var result = _parser.Parse(document);

                OnNewData?.Invoke(this, result);
            }

            OnComlited?.Invoke(this);
        }
    }
}
