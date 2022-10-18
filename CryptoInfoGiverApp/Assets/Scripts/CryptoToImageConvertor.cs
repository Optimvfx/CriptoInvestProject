using CryptoInfoGiverSpace;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "CryptoToImageConvertor", menuName = "ScriptableObjects/CryptoToImageConvertor", order = 1)]
public class CryptoToImageConvertor : ScriptableObject
{
    [SerializeField] private CryptoImagePair[] _cryptoImagePairs;

    public bool TryGetImageFromCrypto(Crypto convertiong, out Sprite sprite)
    {
        sprite = null;

        if (_cryptoImagePairs.Select(pair => pair.Crypto).Contains(convertiong) == false)
            return false;

        sprite = _cryptoImagePairs.First(pair => pair.Crypto == convertiong).Image;

        return true;
    }

    [System.Serializable]
    private class CryptoImagePair
    {
        [SerializeField] private Crypto _crypto;
        [SerializeField] private Sprite _image;

        public Crypto Crypto => _crypto;
        public Sprite Image => _image;
    }
}
