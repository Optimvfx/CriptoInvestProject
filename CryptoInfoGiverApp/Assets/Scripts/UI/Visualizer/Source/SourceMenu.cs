using UnityEngine;
using IJunior.TypedScenes;
using CryptoInvestAnalystAplicationCore;
using System.Linq;
using System;

public class SourceMenu : MonoBehaviour, ISceneLoadHandler<int>
{
    [SerializeField] private ForecastVisualizerLister _forecastVisualizerLister;

    private int _sourceIndex = 0;

    public void OnSceneLoaded(int index)
    {
        _sourceIndex = index;

        if (CryptoInfoGiverCoreFactory.CryptoAplicationDataBase.TryGetSourceByIndex(out ReadonlyCryptoInfoSource source, _sourceIndex) == false)
            throw new ArgumentException();

        _forecastVisualizerLister.Visualize(source.AllForecasts);
    }

    public void AddNewForecast(Forecast newForecast)
    {
        CryptoInfoGiverCoreFactory.CryptoAplicationDataBase.TryAddForecast(_sourceIndex, newForecast);
    }
}
