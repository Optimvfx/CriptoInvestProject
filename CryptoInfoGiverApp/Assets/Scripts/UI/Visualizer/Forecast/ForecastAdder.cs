using CryptoInfoGiverSpace;
using CryptoInvestAnalystAplicationCore;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ForecastAdder : MonoBehaviour
{
    [SerializeField] private SourceMenu _sourceMenu;
    [Header("Fiealds")]
    [SerializeField] private CryptoDropDown _crypto;
    [SerializeField] private CryptoDropDown _comperer;
    [SerializeField] private UIntInputField _length;
    [SerializeField] private DataTimeInputField _creationData;
    [SerializeField] private UIntInputField _price;
    [SerializeField] private Button _createNewButton;

    private void OnEnable()
    {
        _createNewButton.onClick.AddListener(TryAddNewSource);
    }

    private void OnDisable()
    {
        _createNewButton.onClick.RemoveListener(TryAddNewSource);
    }

    private void TryAddNewSource()
    {
        if (_crypto.Sellected == _comperer.Sellected)
            return;

        if (_creationData.TryParse(out DateTime creationDate) == false)
            return;

        if (_length.TryParse(out uint length) == false)
            return;

        if (_price.TryParse(out uint price) == false)
            return;

        var newForecast = new Forecast(_crypto.Sellected, new TimeSpan((int)length, 0, 0, 0), creationDate, 2000, _comperer.Sellected);

        _sourceMenu.AddNewForecast(newForecast);
    }
}
