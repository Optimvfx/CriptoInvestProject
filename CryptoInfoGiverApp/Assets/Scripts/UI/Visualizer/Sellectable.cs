using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public abstract class Sellectable<SellectObject> : MonoBehaviour
{
    [SerializeField] private Button _sellectButton;

    public event UnityAction<SellectObject> Sellected;

    private void OnEnable()
    {
        _sellectButton.onClick.AddListener(Sellect);
    }

    private void OnDisable()
    {
        _sellectButton.onClick.RemoveListener(Sellect);
    }

    private void Sellect()
    {
        Sellected?.Invoke(GetSellectedObject());
    }

    protected abstract SellectObject GetSellectedObject();
}
