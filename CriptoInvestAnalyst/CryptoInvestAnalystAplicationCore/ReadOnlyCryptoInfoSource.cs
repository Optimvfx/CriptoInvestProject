using System.Collections.Generic;
using System.Linq;
using Extenstions;

namespace CryptoInvestAnalystAplicationCore
{
    public class ReadonlyCryptoInfoSource
    {
        private protected readonly List<Forecast> Forecasts;

        public int ForecastsLength => Forecasts.Count;

        public IEnumerable<Forecast> AllForecasts => Forecasts;

        public ReadonlyCryptoInfoSource(IEnumerable<Forecast> forecasts)
        {
            Forecasts = forecasts.ToList();
        }

        public ReadonlyCryptoInfoSource()
        {
            Forecasts = new List<Forecast>();
        }

        public bool TryGetIndexOfForecast(Forecast forecast, out int index)
        {
            index = -1;

            if (Forecasts.Contains(forecast) == false)
                return false;

            index = Forecasts.IndexOf(forecast);

            return true;
        }

        public bool TryGetForecastByIndex(out Forecast forecast, in int index)
        {
            forecast = default(Forecast);

            if (Forecasts.OutOfBounds(index))
                return false;

            forecast = Forecasts[index];

            return true;
        }
    }
}