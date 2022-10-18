using CryptoInvestAnalystAplicationCore;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ForecastVisualizer : Sellectable<Forecast>
{
    [SerializeField] private CryptoToImageConvertor _cryptoToImageConvertor;

    [Header("Visualize")]
    [SerializeField] private Image _cryptoImage;
    [SerializeField] private Image _compererImage;
    [SerializeField] private TMP_Text _lengthText;
    [SerializeField] private TMP_Text _creationDataText;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private TMP_Text _resultText;

    private Forecast _forecast;

    public void Visualize(Forecast forecast)
    {
        _forecast = forecast;

        if (_cryptoToImageConvertor.TryGetImageFromCrypto(_forecast.Crypto, out Sprite sprite))
            _cryptoImage.sprite = sprite;

        if (_cryptoToImageConvertor.TryGetImageFromCrypto(_forecast.Comparer, out sprite))
            _compererImage.sprite = sprite;

        _lengthText.text = forecast.Length.TotalDays.ToString();
        _creationDataText.text = forecast.CreationData.ToString();
        _priceText.text = forecast.Price.ToString();

        if (CryptoInfoGiverCoreFactory.CryptoAplicationDataBase.TryGetForecastResult(forecast, out double resultProcent, out double exceptedResult, out double result))
            _resultText.text = resultProcent.ToString() + "%";
    }

    protected override Forecast GetSellectedObject()
    {
        return _forecast;
    }
}
