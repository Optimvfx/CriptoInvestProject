using CryptoInvestAnalystAplicationCore;
using UnityEngine;
using IJunior.TypedScenes;

public class SourceVisualizerLister : VisualizerLister<SourceVisualizer, ReadonlyCryptoInfoSource>
{
    [SerializeField] private SourceVisualizer _sourceVisualizer;

    private void Start()
    {
        foreach(var source in CryptoInfoGiverCoreFactory.CryptoAplicationDataBase.GetTopSourceByForcasts())
            Visualize(source.ConvertToReadOnlyCryptoInfoSource());

        CryptoInfoGiverCoreFactory.CryptoAplicationDataBase.AddedNewSource += Visualize;
    }

    protected override SourceVisualizer GetSellectable(ReadonlyCryptoInfoSource source)
    {
        var newSource = Instantiate(_sourceVisualizer, Container);

        newSource.Visualize(source);

        return newSource;
    }

    protected override void OnSellect(ReadonlyCryptoInfoSource sellectGiverType)
    {
        GoToSource(sellectGiverType);
    }

    private void GoToSource(ReadonlyCryptoInfoSource source)
    {
        CryptoInfoGiverCoreFactory.TrySave();

        if(CryptoInfoGiverCoreFactory.CryptoAplicationDataBase.TryGetIndexBySource(source, out int index))
            SourceInfoScene.Load(index);
    }
}
