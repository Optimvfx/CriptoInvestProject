using CryptoInvestAnalystAplicationCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForecastVisualizerLister : VisualizerLister<ForecastVisualizer, Forecast>
{
    [SerializeField] private ForecastVisualizer _forecastVisualizer;

    private void OnEnable()
    {
        CryptoInfoGiverCoreFactory.CryptoAplicationDataBase.AddedNewForecast += Visualize;
    }

    private void OnDisable()
    {
        CryptoInfoGiverCoreFactory.CryptoAplicationDataBase.AddedNewForecast -= Visualize;
    }

    public void Visualize(IEnumerable<Forecast> forecasts)
    {
        foreach(var forecast in forecasts)
        {
            Visualize(forecast);
        }
    }

    protected override ForecastVisualizer GetSellectable(Forecast source)
    {
        var newForecast = Instantiate(_forecastVisualizer, Container);
        newForecast.Visualize(source);

        return newForecast;
    }

    protected override void OnSellect(Forecast sellectGiverType)
    {
       
    }
}
