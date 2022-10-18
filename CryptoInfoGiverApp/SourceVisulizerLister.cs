using CryptoInvestAnalystAplicationCore;
using IJunior.TypedScenes;
using UnityEngine;

public class SourceVisulizerLister : VisualizerLister<SourceVisualizer, CryptoInfoSource>
{
    [SerializeField] private Transform _container;
    [SerializeField] private SourceVisualizer _visualizerPrefab;

    private void Start()
    {
        VisualizeAll();

        CryptoInfoGiverCoreFactory.CryptoAplicationDataBase.AddedNewSource += Visualize;
    }

    private void VisualizeAll()
    {
        foreach(var source in CryptoInfoGiverCoreFactory.CryptoAplicationDataBase.GetTopSourceByForcasts())
        {
            Visualize(source);
        }
    }

    private void GoToSource(ReadonlyCryptoInfoSource source)
    {
        if (CryptoInfoGiverCoreFactory.CryptoAplicationDataBase.TryGetIndexBySource(source, out int index))
        {
            CryptoInfoGiverCoreFactory.TrySave();
            SourceInfoScene.Load(source);
        }
    }

    protected override SourceVisualizer GetSellectable(CryptoInfoSource source)
    {
        var newVisualize = Instantiate(_visualizerPrefab, _container);
        newVisualize.Visualize(source);

        return newVisualize;
    }

    protected override void OnSellect(CryptoInfoSource sellectGiverType)
    {
      GoToSource(sellectGiverType);
    }
}
