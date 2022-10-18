using Extenstions;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

namespace CryptoInvestAnalystAplicationCore
{
    [Serializable]
    public class CryptoInfoSource
    {
        [SerializeField] private string _name;

        [SerializeField] protected List<Forecast> Forecasts;

        public string Name => _name;

        public int ForecastsLength => Forecasts.Count;

        public IEnumerable<Forecast> AllForecasts => Forecasts;

        public CryptoInfoSource(IEnumerable<Forecast> forecasts, string name)
        {
            Forecasts = forecasts.ToList();
            _name = name;
        }

        public CryptoInfoSource()
        {
            Forecasts = new List<Forecast>();
        }

        public ReadonlyCryptoInfoSource ConvertToReadOnlyCryptoInfoSource()
        {
            return new ReadonlyCryptoInfoSource(Forecasts, _name);
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
            if (obj is CryptoInfoSource == false)
                return false;

            var other = obj as CryptoInfoSource;

            if (other.Name != this.Name)
                return false;

            var otherForecast = other.AllForecasts.ToArray();

            if (otherForecast.Length != Forecasts.Count)
                return false;

            for (int i = 0; i < Forecasts.Count; i++)
            {
                if (Forecasts[i].Equals(otherForecast[i]) == false)
                    return false;
            }

            return true;
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