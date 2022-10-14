using System;

namespace CryptoInfoGiverSpace
{
    public class HabrParserSettings : IParserSettings
    {
        private readonly int _startPoint;
        private readonly int _endPoint;

        public string BaseUrl => "https://habr.com/ru";

        public string Prefix => $"page";

        public int StartPoint => _startPoint;

        public int EndPoint => _endPoint;

        public HabrParserSettings(int startPoint, int endPoint)
        {
            _startPoint = Math.Max(startPoint, 1);
            _endPoint = Math.Max(endPoint, startPoint);
        }
    }
}
