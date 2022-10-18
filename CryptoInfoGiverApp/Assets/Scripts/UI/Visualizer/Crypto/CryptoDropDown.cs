using CryptoInfoGiverSpace;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TMP_Dropdown))]
public class CryptoDropDown : MonoBehaviour
{
    private TMP_Dropdown _dropdown;

    public Crypto Sellected => (Crypto)_dropdown.value;

    private void Awake()
    {
        _dropdown = GetComponent<TMP_Dropdown>();

        AddParameters();
    }

    private void AddParameters()
    {
        var cryptos = Enum.GetNames(typeof(Crypto));

        _dropdown.ClearOptions();
        _dropdown.AddOptions(cryptos.ToList());
    }
}
