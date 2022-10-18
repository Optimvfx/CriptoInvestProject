using CryptoInvestAnalystAplicationCore;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SourceVisualizer : Sellectable<ReadonlyCryptoInfoSource>
{
    [SerializeField] private TMP_Text _name;

    private ReadonlyCryptoInfoSource _cryptoInfoSource;

    public void Visualize(ReadonlyCryptoInfoSource source)
    {
        _cryptoInfoSource = source;

        _name.text = source.Name;
    }

    protected override ReadonlyCryptoInfoSource GetSellectedObject()
    {
        return _cryptoInfoSource;
    }
}
