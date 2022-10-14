using Extenstions;
using System.Collections.Generic;

namespace CryptoInvestAnalystAplicationCore
{
    public class CryptoInfoSource : ReadonlyCryptoInfoSource
    {
        public CryptoInfoSource(IEnumerable<Forecast> forecasts) : base(forecasts)
        {
          
        }

        public bool OutOfBounds(int index)
        {
            return Forecasts.OutOfBounds(index);
        }

        public bool TryAddForecast(Forecast forecast)
        {
            if (Forecasts.Contains(forecast))
                return false;

            Forecasts.Add(forecast);

            return true;
        }

        public bool TryRemoveForecast(Forecast forecast)
        {
            if(Forecasts.Contains(forecast) == false)
                return false;

            Forecasts.Remove(forecast);

            return true;
        }

        public bool TryRemoveForecastByIndex(in int index)
        {
            if (Forecasts.OutOfBounds(index))
                return false;

            Forecasts.RemoveAt(index);

            return true;
        }
    }
}