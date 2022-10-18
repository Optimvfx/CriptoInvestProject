using UnityEngine;

public class DataBaseOnExitSaver : MonoBehaviour
{
    private void OnApplicationQuit()
    {
        CryptoInfoGiverCoreFactory.TrySave();
    }
}
