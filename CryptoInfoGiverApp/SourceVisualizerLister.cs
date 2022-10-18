using CryptoInvestAnalystAplicationCore;
using IJunior.TypedScenes;


public class SourceVisualizerLister : VisualizerLister<SourceVisualize, CryptoInfoSource>
{
    protected override SourceVisualize GetSellectable(CryptoInfoSource source)
    {
        throw new System.NotImplementedException();
    }

    protected override void OnSellect(CryptoInfoSource sellectGiverType)
    {
       GoToSource(sellectGiverType);
    }

    private void GoToSource(CryptoInfoSource source)
    {
        if (CryptoInfoGiverCoreFactory.CryptoAplicationDataBase.TryGetIndexBySource(source, out int index))
            SourceInfoScene.Load(source);
    }
}
