using CryptoInvestAnalystAplicationCore;
using CryptoInfoGiverSpace;
using UnityEngine;
using System.Linq;

public class BtcDraw : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Texture2D _texture;

    [Range(0, 2000)]
    [SerializeField] private uint _historyLength;

    [Header("Color")]
    [SerializeField] private Color _maxPriceColor;
    [SerializeField] private Color _minPriceColor;
    [SerializeField] private Color _noPriceColor;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();

        var daysHistory = CryptoInfoGiverCoreFactory.CryptoAplicationDataBase.GetDaysPrice(Crypto.USD, Crypto.BTC, _historyLength).ToArray();

        var maxPrice = daysHistory.Max(cryptoCurseInfo => cryptoCurseInfo.MaximalPrice);

        _texture = new Texture2D((int)_historyLength, (int)_historyLength);

        for (int i = 0; i < _historyLength; i++)
        {
            int dayMaxPrice = (int)(daysHistory[i].MaximalPrice / maxPrice * _historyLength);
            int dayMinPrice = (int)(daysHistory[i].MinimalPrice / maxPrice * _historyLength);

            for (int j = 0; j < _historyLength; j++)
            {
                if(j < dayMinPrice)
                {
                    _texture.SetPixel(i, j, _minPriceColor);
                }
                else if (j < dayMaxPrice)
                {
                    _texture.SetPixel(i, j, _maxPriceColor);
                }
                else
                {
                    _texture.SetPixel(i, j, _noPriceColor);
                }
            }
        }

        _texture.Apply();

        MaterialPropertyBlock block = new MaterialPropertyBlock();
        block.SetTexture("_MainTex", _texture);

        _renderer.SetPropertyBlock(block);
    }
}
