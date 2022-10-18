using UnityEngine;

public abstract class VisualizerLister<SellectType, SellectGiverType> : MonoBehaviour
    where SellectType : Sellectable<SellectGiverType>
{
    [SerializeField] private Transform _container;

    protected Transform Container => _container;

    protected void Visualize(SellectGiverType source)
    {
        var newSellectabe = GetSellectable(source);

        newSellectabe.Sellected += OnSellect;

        newSellectabe.gameObject.transform.parent = _container;
    }

    protected abstract SellectType GetSellectable(SellectGiverType source);

    protected abstract void OnSellect(SellectGiverType sellectGiverType);
}
