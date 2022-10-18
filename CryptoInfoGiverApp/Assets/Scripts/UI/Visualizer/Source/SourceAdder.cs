using CryptoInvestAnalystAplicationCore;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SourceAdder : MonoBehaviour
{
    [SerializeField] private TMP_InputField _nameInput;
    [SerializeField] private Button _createNewButton;

    [SerializeField] private uint _minimalLength;

    private void OnEnable()
    {
        _createNewButton.onClick.AddListener(AddNewSource);
    }

    private void OnDisable()
    {
        _createNewButton.onClick.RemoveListener(AddNewSource);
    }

    private void AddNewSource()
    {
        if (_nameInput.text.Length < _minimalLength)
            return;
        
        var newSource = new CryptoInfoSource(new Forecast[0], _nameInput.text);

        CryptoInfoGiverCoreFactory.CryptoAplicationDataBase.TryAddCriptoInfoSource(newSource.ConvertToReadOnlyCryptoInfoSource());
    }
}
