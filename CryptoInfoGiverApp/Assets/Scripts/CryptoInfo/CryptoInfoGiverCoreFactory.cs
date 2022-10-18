using CryptoInvestAnalystAplicationCore;
using CryptoInfoGiverSpace;
using UnityEngine;
using UnityEditor;

public class CryptoInfoGiverCoreFactory : MonoBehaviour
{
    private static readonly string _savePrefabName = "CrpytoBase";

    private static CryptoAplicationDataBase _cryptoAplicationDataBase;

    public static CryptoAplicationDataBase CryptoAplicationDataBase 
    { 
        get
        {
            if (_cryptoAplicationDataBase == null)
            {
                if (TryLoadDatabase(out _cryptoAplicationDataBase) == false)
                {
                    _cryptoAplicationDataBase = GetStandartDatabase();
                }
            }

            return _cryptoAplicationDataBase;
        } 
    }

    public static bool TrySave()
    {
        try
        {
            PlayerPrefs.SetString(_savePrefabName, JsonUtility.ToJson(CryptoAplicationDataBase));

            return true;
        }
        catch
        {
            return false;
        }
    }

    public static bool TryClearSave()
    {
        try
        {
            PlayerPrefs.SetString(_savePrefabName, null);

            return true;
        }
        catch
        {
            return false;
        }
    }

    private static bool TryLoadDatabase(out CryptoAplicationDataBase dataBase)
    {
        dataBase = null;

        if (PlayerPrefs.HasKey(_savePrefabName) == false)
            return false;

        try
        {
            dataBase = JsonUtility.FromJson<CryptoAplicationDataBase>(PlayerPrefs.GetString(_savePrefabName));

            if (dataBase == null)
                return false;

            return true;
        }
        catch
        {
            return false;
        }
    }

    [MenuItem("CryptoInfoGiverFactory/Save/Clear")]
    public static void ClearSave()
    {
        if (TryClearSave() == false)
            throw new System.NullReferenceException("No Save founded!");
    }

    private static CryptoAplicationDataBase GetStandartDatabase()
    {
        var cryptoInfoGiver = new CryptoInfoGiver();
        var cryptoInfoHistoryGiver = new CryptoInfoHistorialGiver(cryptoInfoGiver);
        var cryptoInfoPriceGiver = new CryptoInfoPriceGiver(cryptoInfoGiver);
        var cryptoAllInfoGiver = new CryptoInfoAllGiver(cryptoInfoPriceGiver, cryptoInfoHistoryGiver);

        return new CryptoAplicationDataBase(cryptoAllInfoGiver, cryptoInfoGiver);
    }
}