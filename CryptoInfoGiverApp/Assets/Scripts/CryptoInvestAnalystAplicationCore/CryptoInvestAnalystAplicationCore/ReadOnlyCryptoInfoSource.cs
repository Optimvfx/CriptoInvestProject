using System.Collections.Generic;
using System.Linq;
using Extenstions;
using UnityEngine;

namespace CryptoInvestAnalystAplicationCore
{
    [System.Serializable]
    public class ReadonlyCryptoInfoSource
    {
        [SerializeField] private string _name;

        [SerializeField] protected List<Forecast> Forecasts;

        public string Name => _name;

        public int ForecastsLength => Forecasts.Count;

        public IEnumerable<Forecast> AllForecasts => Forecasts;

        public ReadonlyCryptoInfoSource(IEnumerable<Forecast> forecasts, string name)
        {
            Forecasts = forecasts.ToList();
            _name = name;
        }

        public ReadonlyCryptoInfoSource()
        {
            Forecasts = new List<Forecast>();
        }

        public CryptoInfoSource ConvertToCryptoInfoSource()
        {
            return new CryptoInfoSource(Forecasts, _name);
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

        public override bool Equals(object obj)
        {
            if (obj is ReadonlyCryptoInfoSource == false)
                return false;

            var other = obj as ReadonlyCryptoInfoSource;

            if (other.Name != this.Name)
                return false;

            var otherForecast = other.AllForecasts.ToArray();

            if (otherForecast.Length != Forecasts.Count)
                return false;

            for(int i = 0; i < Forecasts.Count; i++)
            {
                if (Forecasts[i].Equals(otherForecast[i]) == false)
                    return false;
            }

            return true;
        }
    }
}